using System;
using System.Linq;
using Autofac;
using PointOfSale.Foundation;
using PointOfSale.Foundation.Services;

namespace PointOfSale.Web.Models
{
    public class MonthDetailModel
    {
        private readonly IMonthDetailService _monthDetailService;

        public Guid Id { get; set; }
        public double Profit { get; set; }
        public double Loss { get; set; }
        public double Invest { get; set; }
        public double Balance { get; set; }
        public DateTime DateOfDetails { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public int Year { get; set; }

        public MonthDetailModel(IMonthDetailService monthDetailService)
        {
            _monthDetailService = monthDetailService;
        }

        public MonthDetailModel()
        {
            _monthDetailService = Startup.AutofacContainer.Resolve<IMonthDetailService>();
        }

        internal object GetAllMonthDetailModels(DataTablesAjaxRequestModel tableModel)
        {
            var (total, totalDisplay, records) = _monthDetailService.GetMonthDetailList(tableModel.PageIndex, tableModel.PageSize, tableModel.SearchText,
            tableModel.GetSortText(
                new[]{
                    "SaleDate",
                    "Price",
                    "Quantity",
                }
            ));
            return new
            {
                recordsTotal = total,
                recordsFiltered = totalDisplay,
                data = (from record in records
                        select new object[]
                        {
                            record.Balance,
                            record.Id.ToString(),
                        }
                    ).ToArray()
            };
        }


        internal MonthDetailModel BuildEditMonthDetailModel(Guid id)
        {
            var monthDetail = _monthDetailService.GetMonthDetail(id);

            return new MonthDetailModel
            {
                Id = id,
            };
        }

        internal void SaveMonthDetailModel(MonthDetailModel model)
        {
            
        }

        internal void UpdateMonthDetailModel(Guid id, MonthDetailModel model)
        {
            throw new NotImplementedException();
        }

        internal bool DeleteMonthDetailModel(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}