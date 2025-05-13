using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoran.Model
{
    public class Contract
    {
        public decimal Salary { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
        public ContractType ContractType { get; set; }

        public Contract(decimal salary, DateTime startDate, DateTime endDate, ContractType contractType)
        {
            Salary = salary;
            StartDate = startDate;
            EndDate = endDate;
            ContractType = contractType;
        }
    }

    public enum ContractType{
        FullTime, PartTime
    }
}
