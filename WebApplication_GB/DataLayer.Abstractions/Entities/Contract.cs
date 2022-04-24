using System.Collections.Generic;
using DataLayer.Abstractions.Entities;

namespace DataLayer
{
    public class Contract: BaseEntity
    {
        public string Description { get; set; }
        public List<ContractTask> Tasks { get; set; } = new List<ContractTask>();
        public Client Owner { get; set; }
    }
}