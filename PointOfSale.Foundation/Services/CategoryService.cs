using System;
using System.Collections.Generic;
using System.Linq;
using PointOfSale.Foundation.UnitOfWorks;

namespace PointOfSale.Foundation.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IManagementUnitOfWork _management;

        public CategoryService(IManagementUnitOfWork management)
        {
            _management = management;
        }
        public void AddCategory(Category category)
        {
            _management.CategoryRepository.Add(category);
            _management.Save();
        }

        public IList<Category> Categories()
        {
            return _management.CategoryRepository.GetAll();
        }

        public bool DeleteCategory(Guid id)
        {
            try
            {
                _management.CategoryRepository.Remove(id);
                _management.Save();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public Category GetCategory(Guid id)
        {
            return _management.CategoryRepository.GetById(id);
        }

        public (int total, int totalDisplay, IList<Category> records) GetCategoryList(int pageIndex, int pageSize, string searchText, string orderBy)
        {
            (IList<Category> data, int total, int totalDisplay) result;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                result = _management.CategoryRepository.GetDynamic(null,
                    orderBy, "", pageIndex, pageSize);

            }
            else
            {
                result = _management.CategoryRepository.GetDynamic(x => x.Name == searchText,
                    orderBy, "", pageIndex, pageSize);
            }

            var data = (from x in result.data
                select new Category
                {
                    Id = x.Id,
                    Name = x.Name,
                    NoOfProduct = x.NoOfProduct,
                    Products = x.Products,
                    StockProduct = x.StockProduct,
                    Sales = x.Sales,
                    Invest = x.Invest,
                    MonthDetail = x.MonthDetail
                }).ToList();

            return (result.total, result.totalDisplay, data);
        }

        public void UpdateCategory(Category category)
        {
            _management.CategoryRepository.Edit(category);
            _management.Save();
        }
    }
}