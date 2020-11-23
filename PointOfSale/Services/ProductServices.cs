using System;
using System.Collections.Generic;
using System.Linq;
using DataSets.Entity;
using DataSets.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using PointOfSale.Models;

namespace PointOfSale.Services
{
    public class ProductServices
    {

        private readonly IUnitOfWork _uow;
        private readonly CategoryServices _categoryServices;

        public ProductServices(IUnitOfWork uow, CategoryServices categoryServices)
        {
            _uow = uow;
            _categoryServices = categoryServices;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _uow.Product.GetAll(includeProperties: "Category");
        }

        public ProductViewModel CreateGet()
        {
            var categories = _uow.Category.GetAll().ToList();
            var product = new ProductViewModel()
            {
                Product = new Product(),
                CategoryList = from c in categories select new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }
            };

            return product;
        }

        public void CreatePost(ProductViewModel productViewModel)
        {
            productViewModel.Product.DateOfEntry = DateTime.Today;
            _uow.Product.Add(productViewModel.Product);
            _uow.Save();

            var model = _categoryServices.EditCategoryGet(productViewModel.Product.CategoryId);
            model.Category.Invest += ((productViewModel.Product.Quantity) * (productViewModel.Product.Price));
            model.Category.NoOfProduct += productViewModel.Product.Quantity;
            model.Category.StockProduct += productViewModel.Product.Quantity;

            _uow.Category.Update(model.Category);
            _uow.Save();



            var monthDetails = _uow.MonthDetails.GetFirstOrDefault(x => x.CategoryId == model.Category.Id);
            monthDetails.Invest += ((productViewModel.Product.Quantity) * (productViewModel.Product.Price));
            monthDetails.Loss += ((productViewModel.Product.Quantity) * (productViewModel.Product.Price));

            _uow.MonthDetails.Update(monthDetails);
            _uow.Save();

        }

        public ProductViewModel EditProductGet(Guid? id)
        {
            var model = _uow.Product.GetFirstOrDefault(x => x.Id == id);
            if (model == null) return null;
            var categories = _uow.Category.GetAll().ToList();

            var product = new ProductViewModel()
            {
                Product = model,
                CategoryList = from c in categories
                    select new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    }
            };
            return product;
        }

        public void EditProductPost(ProductViewModel productVm)
        {
            _uow.Product.Update(productVm.Product);
            _uow.Save();
        }

        public ProductViewModel DetailsProduct(Guid id)
        {
            var product = _uow.Product.GetFirstOrDefault(x => x.Id == id, includeProperties: "Category");
            var sales = _uow.SaleDetails.GetFirstOrDefault(x => x.ProductId == product.Id);
            var model = new ProductViewModel()
            {
                Product = product,
                SalesDetails = sales
            };

            return model;
        }

        public bool DeleteProduct(Guid id)
        {
            var deleteData = _uow.Product.GetFirstOrDefault(x => x.Id == id);
            if (deleteData == null) return false;
            var categoryModel = _uow.Category.GetFirstOrDefault(x => x.Id == deleteData.CategoryId);
            categoryModel.Invest -= ((deleteData.Quantity) * (deleteData.Price));
            categoryModel.NoOfProduct -= deleteData.Quantity;
            categoryModel.StockProduct -= deleteData.Quantity;
            categoryModel.DateOfEntry = DateTime.Now;
            //TODO: Product Delete and Change on Category Too.
            _uow.Product.Remove(deleteData);
            _uow.Save();
            return true;
        }
    }
}