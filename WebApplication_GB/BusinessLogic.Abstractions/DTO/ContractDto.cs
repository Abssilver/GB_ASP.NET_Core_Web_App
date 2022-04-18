using System.Collections.Generic;

namespace DataLayer
{
    public class ContractDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ContractTaskDto> Tasks { get; set; } = new List<ContractTaskDto>();
        public ClientDto Owner { get; set; }
    }
}