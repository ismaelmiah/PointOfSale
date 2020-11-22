using System;
using System.Collections.Generic;
using DataSets.Entity;
using DataSets.Interfaces;
using PointOfSale.Models;

namespace PointOfSale.Services
{
    public class OrderServices
    {
        private IUnitOfWork _uow;

        public OrderServices(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void SaleProduct(ProductViewModel productViewModel)
        {
            var product = _uow.Product.GetFirstOrDefault(x => x.Id == productViewModel.Product.Id & x.Quantity >= productViewModel.Product.Quantity);
            if (product == null) return;
            var sale = new SalesDetails()
            {
                Quantity = productViewModel.Product.Quantity,
                Price = productViewModel.Product.Price * productViewModel.Product.Quantity,
                Product = product,
                SaleDate = DateTime.Today
            };
            _uow.OrderDetails.Add(sale);
            _uow.Save();

            product.Quantity -= productViewModel.Product.Quantity;
            _uow.Product.Update(product);
            _uow.Save();

            var categories = _uow.Category.GetFirstOrDefault(x => x.Id == product.CategoryId);
            categories.StockProduct -= productViewModel.Product.Quantity;
            categories.Sales += sale.Price;

            _uow.Category.Update(categories);
            _uow.Save();

            var monthDetails = _uow.MonthDetails.GetFirstOrDefault(x => x.CategoryId == categories.Id);
            var profit = monthDetails.Loss - sale.Price;
            if (profit < 0)
            {
                monthDetails.Loss = 0;
                monthDetails.Profit = Math.Abs(profit);
            }
            else monthDetails.Loss = profit;

            if (monthDetails.Loss == 0)
            {
                monthDetails.Balance = monthDetails.Invest + monthDetails.Profit;
            }
            else
            {
                monthDetails.Balance = monthDetails.Invest - monthDetails.Loss;
            }

            _uow.MonthDetails.Update(monthDetails);
            _uow.Save();
        }

        public bool DeleteRecord(Guid id)
        {
            var record = _uow.OrderDetails.Get(id);
            if (record == null) return false;
            _uow.OrderDetails.Remove(id);
            _uow.Save();
            return true;
        }

        public IEnumerable<SalesDetails> GetAllRecords()
        {
            return _uow.OrderDetails.GetAll(includeProperties: "Product");
        }
    }
}
