﻿using DataSets.Entity;
using DataSets.Interfaces;
using PointOfSale.Data;
using System.Linq;

namespace PointOfSale.RepositoryPattern.Repository
{
    public class CategoryRepo : Repository<Category>, ICategory
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepo(ApplicationDbContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }

        public void Update(Category category)
        {
            var data = _db.Categories.FirstOrDefault(x => x.Id == category.Id);
            if (data == null) return;
            data.Name = category.Name;
            data.NoOfProduct = category.NoOfProduct;
            data.StockProduct = category.StockProduct;
            data.Invest = category.Invest;
            data.Sales = category.Sales;
            data.Products = category.Products;
        }
    }
}
