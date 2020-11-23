﻿using DataSets.Interfaces;
using PointOfSale.Data;

namespace PointOfSale.RepositoryPattern.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Product = new ProductRepo(_db);
            Category = new CategoryRepo(_db);
            SaleDetails = new SaleDetailsRepo(_db);
            MonthDetails = new MonthDetailsRepo(_db);
        }
        public ICategory Category { get; }
        public IProduct Product { get; }
        public ISaleDetails SaleDetails { get; }
        public IMonthDetails MonthDetails { get; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}