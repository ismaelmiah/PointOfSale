using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataSets.Entity;
using DataSets.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using PointOfSale.Models;

namespace PointOfSale.Services
{
    public class ProductServices
    {

        private readonly IUnitOfWork _uow;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductServices(IUnitOfWork uow, IWebHostEnvironment hostEnvironment)
        {
            _uow = uow;
            _hostEnvironment = hostEnvironment;
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
            _uow.Product.Add(productViewModel.Product);
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

        public Product DetailsProduct(Guid id)
        {
            var model = _uow.Product.GetFirstOrDefault(x => x.Id == id, includeProperties: "Category");
            return model;
        }

        public bool DeleteProduct(Guid id)
        {
            var deleteData = _uow.Product.GetFirstOrDefault(x => x.Id == id);
            if (deleteData == null) return false;
            _uow.Product.Remove(deleteData);
            _uow.Save();
            return true;
        }
    }
}