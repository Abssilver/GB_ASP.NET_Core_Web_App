using BusinessLogic.Abstractions.DTO;

namespace DataLayer
{
    public class EmployeeDto : BaseDtoEntity
    {
        public decimal? Salary { get; set; }
        public long? Time { get; set; }
    }
}