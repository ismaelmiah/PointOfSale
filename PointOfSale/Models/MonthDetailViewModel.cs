using System.Collections.Generic;
using DataSets.Entity;

namespace PointOfSale.Models
{
    public class MonthDetailViewModel
    {
        public Month Month { get; set; }
        public int Year { get; set; }
        public IEnumerable<MonthDetails> MonthDetails { get; set; }
    }
}