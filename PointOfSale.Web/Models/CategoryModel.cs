using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using PointOfSale.Foundation;
using PointOfSale.Foundation.Services;

namespace PointOfSale.Web.Models
{
    public class CategoryModel
    {
        private readonly ICategoryService _categoryService;

        public CategoryModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public CategoryModel()
        {
            _categoryService = Startup.AutofacContainer.Resolve<ICategoryService>();
        }

        internal object GetCategories(DataTablesAjaxRequestModel tableModel)
        {
            var (total, totalDisplay, records) = _categoryService.GetCategoryList(tableModel.PageIndex, tableModel.PageSize, tableModel.SearchText,
            tableModel.GetSortText(
                new[]{
                    "Name",
                    "NoOfProduct",
                    "Invest",
                }
            ));
            return new
            {
                recordsTotal = total,
                recordsFiltered = totalDisplay,
                data = (from record in records
                        select new object[]
                        {
                            record.Name,
                            record.Products != null ? record.Products.Count() : 0,
                            record.StockProduct,
                            record.Invest,
                            record.Sales,
                            record.Id.ToString(),
                        }
                    ).ToArray()
            };
        }

        internal CategoryModel BuildEditCategoryModel(Guid id)
        {
            var category = _categoryService.GetCategory(id);
            return new CategoryModel{
                Name = category.Name,
                Id = category.Id
            };
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Invest { get; set; }
        public int NoOfProduct { get; set; }
        public int StockProduct { get; set; }
        public double Sales { get; set; }
        public List<ProductModel> Products { get; set; }

        internal void SaveCategory(CategoryModel model)
        {
            var category = new Category()
            {
                Name = model.Name
            };

            _categoryService.AddCategory(category);
        }

        private List<Product> listOfProducts(List<ProductModel> products)
        {
            return products.Select(x => new Product{
                Price = x.Price,
                Quantity = x.Quantity,
                CategoryId = x.CategoryId
            }).ToList();
        }

        private List<ProductModel> listOfProductModels(List<Product> products)
        {
            return products.Select(x => new ProductModel{
                Price = x.Price,
                Quantity = x.Quantity,
                CategoryId = x.CategoryId
            }).ToList();
        }

        internal void UpdateCategory(Guid id, CategoryModel model)
        {
            var category = _categoryService.GetCategory(id);
            //category.Invest = model.Invest;
            category.Name = model.Name;
            // category.NoOfProduct = model.NoOfProduct;
            // category.Sales = model.Sales;
            // category.StockProduct = model.StockProduct;
            // category.Products = listOfProducts(model.Products);

            _categoryService.UpdateCategory(category);
        }

        internal bool DeleteCategory(Guid id)
        {
            return _categoryService.DeleteCategory(id);
        }
    }
}