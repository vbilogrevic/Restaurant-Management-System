using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoran.Model
{
    public class Chef : Person
    {
        public Contract Contract { get; set; }

        public Chef(string firstName, string lastName, Contract contract) : base(firstName, lastName)
        {
            Contract = contract;
        }
    }
}
