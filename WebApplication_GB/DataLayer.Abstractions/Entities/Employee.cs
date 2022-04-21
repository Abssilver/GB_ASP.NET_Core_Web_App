using DataLayer.Abstractions.Entities;

namespace DataLayer
{
    public class Employee : BaseEntity
    {
        public decimal Salary { get; set; }
        public long Time { get; set; }
    }
}