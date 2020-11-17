using System;
using System.Collections.Generic;
using System.IO;
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
            return _uow.Category.GetAll(includeProperties: "Products");
        }
        public void CreateCategoryPost(CategoryViewModel categoryViewModel)
        {
            _uow.Category.Add(categoryViewModel.category);
            _uow.Save();
        }
        public CategoryViewModel EditCategoryGet(Guid id)
        {
            var model = _uow.Category.GetFirstOrDefault(x => x.Id == id);

            var categoryViewModel = new CategoryViewModel()
            {
                category = model
            };
            return categoryViewModel;
        }
        public void EditCategoryPost(CategoryViewModel categoryViewModel)
        {
            _uow.Category.Update(categoryViewModel.category);
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
            if (deleteData.ImageUrl != null)
            {
                var webRootPath = _hostEnvironment.WebRootPath;
                var imagePath = Path.Combine(webRootPath, deleteData.ImageUrl.TrimStart('\\'));

                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }
            _uow.Category.Remove(deleteData);
            _uow.Save();
            return true;
        }
    }
}