using System.Collections.Generic;

namespace DataLayer
{
    public class Contract
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ContractTask> Tasks { get; set; } = new List<ContractTask>();
        public Client Owner { get; set; }
    }
}