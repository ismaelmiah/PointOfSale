using System;
using System.Collections.Generic;

namespace PointOfSale.Foundation.Services
{
    public interface ICategoryService
    {
        void AddCategory(Category category);
        bool DeleteCategory(Guid id);
        IList<Category> Categories();
        (int total, int totalDisplay, IList<Category> records) GetCategoryList(int pageIndex,
            int pageSize, string searchText, string orderBy);
        void UpdateCategory(Category category);
        Category GetCategory(Guid id);
    }
}