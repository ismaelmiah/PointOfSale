using System;
using System.Collections.Generic;
using System.Globalization;
using DataSets.Entity;
using DataSets.Interfaces;

namespace PointOfSale.Services
{
    public class OrderServices
    {
        private IUnitOfWork _uow;

        public OrderServices(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public bool SaleProduct(Guid id, int quantity)
        {
            var product = _uow.Product.GetFirstOrDefault(x => x.Id == id & x.Quantity >= quantity);
            if (product == null) return false;
            var sale = new SalesDetails()
            {
                Quantity = quantity,
                Price = quantity * product.Price,
                Product = product,
                //SaleDate = DateTime.Now.ToString(CultureInfo.InvariantCulture)
            };
            _uow.OrderDetails.Add(sale);
            _uow.Save();

            product.Quantity -= quantity;
            _uow.Product.Update(product);
            _uow.Save();
            return true;
        }

        public bool DeleteRecord(Guid id)
        {
            var record = _uow.OrderDetails.Get(id);
            if (record == null) return false;
            _uow.OrderDetails.Remove(id);
            var modifiedProduct = _uow.Product.Get(record.ProductId);
            modifiedProduct.Quantity += record.Quantity;
            _uow.Save();
            _uow.Product.Update(modifiedProduct);
            _uow.Save();
            return true;
        }

        public IEnumerable<SalesDetails> GetAllRecords()
        {
            return _uow.OrderDetails.GetAll(includeProperties: "Product");
        }
    }
}
