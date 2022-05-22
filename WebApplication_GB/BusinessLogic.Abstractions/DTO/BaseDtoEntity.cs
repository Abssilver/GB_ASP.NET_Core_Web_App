namespace BusinessLogic.Abstractions.DTO
{
    public abstract class BaseDtoEntity
    {
        public long? Id { get; set; }
        public string Name { get; set; }
    }
}