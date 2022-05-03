using DataLayer.Abstractions.Entities;

namespace DataLayer
{
    public class Invoice : BaseEntity
    {
        public string Price { get; set; }
        public Client Payer { get; set; }
    }
}