namespace DataLayer
{
    public class InvoiceDto
    {
        public string Id { get; set; }
        public string Price { get; set; }
        public ClientDto Payer { get; set; }
    }
}