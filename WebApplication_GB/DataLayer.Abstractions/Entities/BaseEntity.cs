namespace DataLayer.Abstractions.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}