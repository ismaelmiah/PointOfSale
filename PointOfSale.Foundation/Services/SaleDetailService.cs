using System;
using System.Collections.Generic;
using System.Linq;
using PointOfSale.Foundation.UnitOfWorks;

namespace PointOfSale.Foundation.Services
{

    public interface ISaleDetailService
    {
        void AddSaleDetail(SaleDetail saleDetail);
        bool DeleteSaleDetail(Guid id);
        IList<SaleDetail> SaleDetails();
        (int total, int totalDisplay, IList<SaleDetail> records) GetSaleDetailList(int pageIndex,
            int pageSize, string searchText, string orderBy);
        void UpdateSaleDetail(SaleDetail saleDetail);
        SaleDetail GetSaleDetails(Guid id);
    }

    public class SaleDetailService : ISaleDetailService
    {
        private readonly IManagementUnitOfWork _management;

        public SaleDetailService(IManagementUnitOfWork management)
        {
            _management = management;
        }
        public void AddSaleDetail(SaleDetail SaleDetail)
        {
            _management.SaleDetailRepository.Add(SaleDetail);
            _management.Save();
        }

        public IList<SaleDetail> SaleDetails()
        {
            return _management.SaleDetailRepository.GetAll();
        }

        public bool DeleteSaleDetail(Guid id)
        {
            try
            {
                _management.SaleDetailRepository.Remove(id);
                _management.Save();
                return true;
            }
            catch (System.Exception)
            {
                return false;                
                throw;
            }
        }

        public (int total, int totalDisplay, IList<SaleDetail> records) GetSaleDetailList(int pageIndex, int pageSize, string searchText, string orderBy)
        {
            (IList<SaleDetail> data, int total, int totalDisplay) result;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                result = _management.SaleDetailRepository.GetDynamic(null,
                    orderBy, "Product", pageIndex, pageSize);

            }
            else
            {
                result = _management.SaleDetailRepository.GetDynamic(x => x.Price.ToString() == searchText,
                    orderBy, "Product", pageIndex, pageSize);
            }

            var data = (from x in result.data
                select new SaleDetail
                {
                    Id = x.Id,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    Product = x.Product,
                    ProductId = x.ProductId
                }).ToList();

            return (result.total, result.totalDisplay, data);
        }

        public void UpdateSaleDetail(SaleDetail SaleDetail)
        {
            _management.SaleDetailRepository.Edit(SaleDetail);
            _management.Save();
        }

        public SaleDetail GetSaleDetails(Guid id) => _management.SaleDetailRepository.GetById(id);
    }
}