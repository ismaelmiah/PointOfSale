using System;
using PointOfSale.DataAccessLayer;

namespace PointOfSale.Foundation
{
    public class MonthDetail : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public double Profit { get; set; }
        public double Loss { get; set; }
        public double Invest { get; set; }
        public double Balance { get; set; }
        public DateTime DateOfDetails { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}