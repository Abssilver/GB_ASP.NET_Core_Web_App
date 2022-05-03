using BusinessLogic.Abstractions.DTO;

namespace DataLayer
{
    public class InvoiceDto : BaseDtoEntity
    {
        public string Price { get; set; }
        public ClientDto Payer { get; set; }
    }
}