using System.Linq;
using DataSets.Entity;
using DataSets.Interfaces;
using PointOfSale.Data;

namespace PointOfSale.RepositoryPattern.Repository
{
    public class MonthDetailsRepo : Repository<MonthDetails>, IMonthDetails
    {
        private readonly ApplicationDbContext _dbContext;

        public MonthDetailsRepo(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(MonthDetails month)
        {
            var data = _dbContext.MonthDetails.FirstOrDefault(x=> x.CategoryId == month.CategoryId);
            if (data == null) return;
            data.Invest = month.Invest;
            data.Profit = month.Profit;
            data.Loss = month.Loss;
            data.Balance = month.Balance;
        }
    }
}