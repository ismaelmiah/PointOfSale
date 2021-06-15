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

        internal CategoryModel GetCategories(DataTablesAjaxRequestModel tableModel)
        {
            //return _categoryService.GetCategoryList()
            throw new NotImplementedException();
        }

        internal CategoryModel BuildEditCategoryModel(Guid guid)
        {
            throw new NotImplementedException();
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
                Invest = model.Invest,
                NoOfProduct = model.NoOfProduct,
                StockProduct = model.StockProduct,
                Sales = model.Sales,
                Products = listOfProducts(model.Products)
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
            category.Invest = model.Invest;
            category.Name = model.Name;
            category.NoOfProduct = model.NoOfProduct;
            category.Sales = model.Sales;
            category.StockProduct = model.StockProduct;
            category.Products = listOfProducts(model.Products);

            _categoryService.UpdateCategory(category);
        }

        internal CategoryModel DetailsCategory(Guid id)
        {
            var category = _categoryService.GetCategory(id);
            return new CategoryModel
            {
                Name = category.Name,
                Invest = category.Invest,
                StockProduct = category.StockProduct,
                Sales = category.Sales,
                Products = listOfProductModels(category.Products),
                NoOfProduct = category.NoOfProduct,                
            };
        }

        internal bool DeleteCategory(Guid id)
        {
            return _categoryService.DeleteCategory(id);
        }
    }
}