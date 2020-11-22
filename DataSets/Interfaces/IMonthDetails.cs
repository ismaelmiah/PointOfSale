using DataSets.Entity;

namespace DataSets.Interfaces
{
    public interface IMonthDetails : IRepository<MonthDetails>
    {
        void Update(MonthDetails month);
    }
}