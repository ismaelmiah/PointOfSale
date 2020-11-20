using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataSets.Entity;
using DataSets.Interfaces;
using Microsoft.AspNetCore.Hosting;
using PointOfSale.Models;

namespace PointOfSale.Services
{
    public class CategoryServices
    {
        private readonly IUnitOfWork _uow;
        private readonly IWebHostEnvironment _hostEnvironment;
        public CategoryServices(IUnitOfWork uow, IWebHostEnvironment hostEnvironment)
        {
            _uow = uow;
            _hostEnvironment = hostEnvironment;
        }
        public IEnumerable<Category> GetAllCategory()
        {
            var categories = _uow.Category.GetAll(includeProperties: "Products").ToList();
            foreach (var category in categories)
            {
                //var sum = category.Products.Sum(x => x.Price);
                //var count= category.Products.Select(y=> y.Quantity).Sum();
                //category.StockProduct = count;
                //category.Invest = sum;
            }
            return categories;
        }
        public CategoryViewModel CreateCategoryGet()
        {
            var model = new CategoryViewModel(){Category = new Category()};
            return model;
        }
        public void CreateCategoryPost(CategoryViewModel categoryViewModel)
        {
            _uow.Category.Add(categoryViewModel.Category);
            _uow.Save();
        }
        public CategoryViewModel EditCategoryGet(Guid? id)
        {
            var model = _uow.Category.GetFirstOrDefault(x => x.Id == id);

            var categoryViewModel = new CategoryViewModel()
            {
                Category = model
            };
            return categoryViewModel;
        }
        public void EditCategoryPost(CategoryViewModel categoryViewModel)
        {
            _uow.Category.Update(categoryViewModel.Category);
            _uow.Save();
        }
        public Category DetailsCategory(Guid id)
        {
           return _uow.Category.GetFirstOrDefault(x => x.Id == id, includeProperties: "Products");
        }
        public bool DeleteCategory(Guid id)
        {
            var deleteData = _uow.Category.GetFirstOrDefault(x => x.Id == id);
            if (deleteData == null) return false;
            _uow.Category.Remove(deleteData);
            _uow.Save();
            return true;
        }
    }
}