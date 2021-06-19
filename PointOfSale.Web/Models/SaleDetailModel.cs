using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Autofac;
using Microsoft.AspNetCore.Mvc.Rendering;
using PointOfSale.Foundation;
using PointOfSale.Foundation.Services;

namespace PointOfSale.Web.Models
{
    public class SaleDetailModel
    {
        private readonly ISaleDetailService _saleDetailService;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public SaleDetailModel(ISaleDetailService saleDetailService, IProductService productService, ICategoryService categoryService)
        {
            _saleDetailService = saleDetailService;
            _productService = productService;
            _categoryService = categoryService;
        }

        public SaleDetailModel()
        {
            _saleDetailService = Startup.AutofacContainer.Resolve<ISaleDetailService>();
            _productService = Startup.AutofacContainer.Resolve<IProductService>();
            _categoryService = Startup.AutofacContainer.Resolve<ICategoryService>();
        }

        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public DateTime SaleDate { get; set; }
        public Guid ProductId { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        internal object GetAllSaleDetails(DataTablesAjaxRequestModel tableModel)
        {
            var (total, totalDisplay, records) = _saleDetailService.GetSaleDetailList(tableModel.PageIndex, tableModel.PageSize, tableModel.SearchText,
            tableModel.GetSortText(
                new[]{
                    "SaleDate",
                    "Price",
                    "Quantity",
                }
            ));
            return new
            {
                recordsTotal = total,
                recordsFiltered = totalDisplay,
                data = (from record in records
                        select new object[]
                        {
                            record.SaleDate.ToShortDateString(),
                            record.Product == null ? "" : record.Product.Name,
                            record.Quantity,
                            record.Product == null ? 0 : record.Product.Price,
                            record.Price,
                            record.Id.ToString(),
                        }
                    ).ToArray()
            };
        }

        internal SaleDetailModel BuildEditSaleDetailModel(Guid id)
        {
            var saleDetail = _saleDetailService.GetSaleDetails(id);
            if(saleDetail == null)
            {
                var product = _productService.GetProduct(id);
            
                return new SaleDetailModel
                {
                    Price = 0,
                    Quantity = 1,
                    ProductName = product.Name,
                    ProductId = id
                };
            }
            else{
                return new SaleDetailModel
                {
                    Id = id,
                    Price = saleDetail.Price,
                    Quantity = saleDetail.Quantity,
                    ProductName = saleDetail.Product.Name,
                    ProductId = saleDetail.ProductId
                };
            }
        }

        internal void SaveSaleDetail(SaleDetailModel model)
        {
            try
            {
                var product = _productService.GetProduct(model.ProductId);
                var saleDetail = new SaleDetail()
                {
                    Quantity = model.Quantity,
                    Price = model.Price * model.Quantity,
                    SaleDate = DateTime.UtcNow,
                    ProductId = model.ProductId
                };

                _saleDetailService.AddSaleDetail(saleDetail);

                product.Quantity -= model.Quantity;
                _productService.UpdateProduct(product);

                var categories = _categoryService.GetCategory(product.CategoryId);
                categories.StockProduct -= model.Quantity;
                categories.Sales += saleDetail.Price;

                _categoryService.UpdateCategory(categories);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        internal void UpdateSaleDetail(Guid id, SaleDetailModel model)
        {
            try
            {
                var saleDetail = _saleDetailService.GetSaleDetails(id);
                var product = _productService.GetProduct(saleDetail.ProductId);
                saleDetail.ProductId = model.ProductId;
                saleDetail.Quantity = model.Quantity;
                saleDetail.SaleDate = model.SaleDate;
                saleDetail.Price = model.Price;

                _saleDetailService.UpdateSaleDetail(saleDetail);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        internal bool DeleteSaleDetail(Guid id)
        {
            return _saleDetailService.DeleteSaleDetail(id);
        }
    }
}