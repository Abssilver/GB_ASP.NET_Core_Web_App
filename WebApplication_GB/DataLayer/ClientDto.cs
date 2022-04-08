using System.Collections.Generic;

namespace DataLayer
{
    public class ClientDto
    {
        public string Id { get; set; }
        public List<ContractDto> Contracts { get; set; } = new List<ContractDto>();
    }
}