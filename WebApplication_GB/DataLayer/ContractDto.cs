using System.Collections.Generic;

namespace DataLayer
{
    public class ContractDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TaskDto> Tasks { get; set; } = new List<TaskDto>();
        public ClientDto Owner { get; set; }
    }
}