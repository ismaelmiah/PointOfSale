using System;
using System.Collections.Generic;
using System.Linq;
using PointOfSale.Foundation.UnitOfWorks;

namespace PointOfSale.Foundation.Services
{

    public interface IMonthDetailService
    {
        void AddMonthDetail(MonthDetail monthDetail);
        bool DeleteMonthDetail(Guid id);
        IList<MonthDetail> MonthDetails();
        (int total, int totalDisplay, IList<MonthDetail> records) GetMonthDetailList(int pageIndex,
            int pageSize, string searchText, string orderBy);
        void UpdateMonthDetail(MonthDetail monthDetail);
        MonthDetail GetMonthDetail(Guid id);
    }

    public class MonthDetailService : IMonthDetailService
    {
        private readonly IManagementUnitOfWork _management;

        public MonthDetailService(IManagementUnitOfWork management)
        {
            _management = management;
        }
        public void AddMonthDetail(MonthDetail monthDetail)
        {
            _management.MonthDetailRepository.Add(monthDetail);
            _management.Save();
        }

        public IList<MonthDetail> MonthDetails()
        {
            return _management.MonthDetailRepository.GetAll();
        }

        public bool DeleteMonthDetail(Guid id)
        {
            try
            {
                _management.MonthDetailRepository.Remove(id);
                _management.Save();
                return true;
            }
            catch (System.Exception)
            {
                return false;
                throw;
            }
        }

        public (int total, int totalDisplay, IList<MonthDetail> records) GetMonthDetailList(int pageIndex, int pageSize, string searchText, string orderBy)
        {
            (IList<MonthDetail> data, int total, int totalDisplay) result;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                result = _management.MonthDetailRepository.GetDynamic(null,
                    orderBy, "Category", pageIndex, pageSize);

            }
            else
            {
                result = _management.MonthDetailRepository.GetDynamic(x => x.Invest.ToString() == searchText,
                    orderBy, "Category", pageIndex, pageSize);
            }

            var data = (from x in result.data
                select new MonthDetail
                {
                    Id = x.Id,
                    Profit = x.Profit,
                    Balance = x.Balance,
                    Loss = x.Loss,
                    DateOfDetails = x.DateOfDetails,
                    Invest = x.Invest,
                    CategoryId = x.CategoryId,
                    Category = x.Category
                }).ToList();

            return (result.total, result.totalDisplay, data);
        }

        public void UpdateMonthDetail(MonthDetail monthDetail)
        {
            _management.MonthDetailRepository.Edit(monthDetail);
            _management.Save();
        }

        public MonthDetail GetMonthDetail(Guid id) => _management.MonthDetailRepository.GetById(id);
    }
}