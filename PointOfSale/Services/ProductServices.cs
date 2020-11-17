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

        public ProductVm CreateGet()
        {
            var product = new ProductVm()
            {
                Product = new Product(),
                CategoryList = _uow.Category.GetAll().Select(i => new SelectListItem()
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            return product;
        }

        public void CreatePost(ProductVm productVm)
        {
            _uow.Product.Add(productVm.Product);
            _uow.Save();
        }

        public ProductVm EditProductGet(Guid id)
        {
            var model = _uow.Product.GetFirstOrDefault(x => x.Id == id);
            if (model == null) return null;
            var product = new ProductVm()
            {
                Product = model,
                CategoryList = _uow.Category.GetAll().Select(i => new SelectListItem()
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            return product;
        }

        public void EditProductPost(ProductVm productVm)
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
            if (deleteData.ImageUrl != null)
            {
                var webRootPath = _hostEnvironment.WebRootPath;
                var imagePath = Path.Combine(webRootPath, deleteData.ImageUrl.TrimStart('\\'));

                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }
            _uow.Product.Remove(deleteData);
            _uow.Save();
            return true;
        }
    }
}