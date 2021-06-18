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

        public SaleDetailModel(ISaleDetailService saleDetailService, IProductService productService)
        {
            _saleDetailService = saleDetailService;
            _productService = productService;
        }

        public SaleDetailModel()
        {
            _saleDetailService = Startup.AutofacContainer.Resolve<ISaleDetailService>();
            _productService = Startup.AutofacContainer.Resolve<IProductService>();
        }

        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public DateTime SaleDate { get; set; }
        public Guid ProductId { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        public SelectList ProductList { get; set; }        

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
            return new SaleDetailModel
            {
                Price = saleDetail.Price,
                Quantity = saleDetail.Quantity,
                ProductList = BuildProductList(saleDetail.ProductId),
                ProductId = saleDetail.ProductId,
                Id = saleDetail.Id
            };
        }

        private SelectList BuildProductList(object productId = null)
        {
            var products = _productService.Products();
            return new SelectList(products, "Id", "Name", productId);
        }

        internal void SaveSaleDetail(SaleDetailModel model)
        {
            var saleDetail = new SaleDetail()
            {
                Price = model.Price,
                Quantity = model.Quantity,
                SaleDate = DateTime.UtcNow,
                ProductId = model.ProductId
            };

            _saleDetailService.AddSaleDetail(saleDetail);
        }

        internal void UpdateSaleDetail(Guid id, SaleDetailModel model)
        {
            var saleDetail = _saleDetailService.GetSaleDetails(id);

            saleDetail.ProductId = model.ProductId;
            saleDetail.Quantity = model.Quantity;
            saleDetail.SaleDate = model.SaleDate;
            saleDetail.Price = model.Price;

            _saleDetailService.UpdateSaleDetail(saleDetail);
        }

        internal bool DeleteSaleDetail(Guid id)
        {
            return _saleDetailService.DeleteSaleDetail(id);
        }
    }
}