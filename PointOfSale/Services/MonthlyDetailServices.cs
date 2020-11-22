using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DataSets.Entity;
using DataSets.Interfaces;
using PointOfSale.Models;

namespace PointOfSale.Services
{
    public class MonthlyDetailServices
    {
        private readonly IUnitOfWork _uow;

        public MonthlyDetailServices(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public static int MonthNumber(Month moth)
        {
            var month = 0;
            switch (moth)
            {
                case Month.January:
                    month = 1;
                    break;
                case Month.February:
                    month = 2;
                    break;
                case Month.March:
                    month = 3;
                    break;
                case Month.April:
                    month = 4;
                    break;
                case Month.May:
                    month = 5;
                    break;
                case Month.June:
                    month = 6;
                    break;
                case Month.July:
                    month = 7;
                    break;
                case Month.August:
                    month = 8;
                    break;
                case Month.September:
                    month = 9;
                    break;
                case Month.October:
                    month = 10;
                    break;
                case Month.November:
                    month = 11;
                    break;
                case Month.December:
                    month = 12;
                    break;
            }
            return month;
        }
        public IEnumerable<MonthDetails> GetSpecificDetails(MonthDetailViewModel monthDetailViewModel)
        {
            var month = MonthNumber(monthDetailViewModel.Month);
            var date = new DateTime(monthDetailViewModel.Year, month, 01);
            var model = _uow.MonthDetails.GetAll().Where(x=> x.DateOfDetails.ToShortDateString() == date.ToShortDateString());
            return model;
        }

        public IEnumerable<MonthDetails> GetAllMonthDetails()
        {
            var model = _uow.MonthDetails.GetAll(includeProperties: "Category");
            return model;
        }
    }
}