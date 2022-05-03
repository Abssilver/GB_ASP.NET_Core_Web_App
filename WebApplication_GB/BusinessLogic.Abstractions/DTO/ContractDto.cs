using System.Collections.Generic;
using BusinessLogic.Abstractions.DTO;

namespace DataLayer
{
    public class ContractDto : BaseDtoEntity
    {
        public string Description { get; set; }
        public List<ContractTaskDto> Tasks { get; set; } = new List<ContractTaskDto>();
        public ClientDto Owner { get; set; }
    }
}