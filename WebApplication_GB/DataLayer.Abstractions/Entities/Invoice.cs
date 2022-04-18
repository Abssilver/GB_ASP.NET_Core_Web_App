namespace DataLayer
{
    public class Invoice
    {
        public string Id { get; set; }
        public string Price { get; set; }
        public Client Payer { get; set; }
    }
}