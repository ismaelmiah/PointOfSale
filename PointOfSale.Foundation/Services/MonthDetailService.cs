using System;
using System.Collections.Generic;
using System.Linq;
using PointOfSale.Foundation.Repositories;

namespace PointOfSale.Foundation.Services
{

    public interface ICMonthDetailService
    {
        void AddMonthDetail(MonthDetail monthDetail);
        void DeleteMonthDetail(Guid id);
        IList<MonthDetail> Categories();
        (int total, int totalDisplay, IList<MonthDetail> records) GetMonthDetailList(int pageIndex,
            int pageSize, string searchText, string orderBy);
        void UpdateMonthDetail(MonthDetail monthDetail);
    }

    public class MonthDetailService : ICMonthDetailService
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

        public IList<MonthDetail> Categories()
        {
            return _management.MonthDetailRepository.GetAll();
        }

        public void DeleteMonthDetail(Guid id)
        {
            _management.MonthDetailRepository.Remove(id);
            _management.Save();
        }

        public (int total, int totalDisplay, IList<MonthDetail> records) GetMonthDetailList(int pageIndex, int pageSize, string searchText, string orderBy)
        {
            (IList<MonthDetail> data, int total, int totalDisplay) result;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                result = _management.MonthDetailRepository.GetDynamic(null,
                    orderBy, "MonthDetail", pageIndex, pageSize);

            }
            else
            {
                result = _management.MonthDetailRepository.GetDynamic(x => x.Invest.ToString() == searchText,
                    orderBy, "MonthDetail", pageIndex, pageSize);
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
    }
}