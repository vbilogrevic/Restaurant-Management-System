using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoran.Model
{
    public class Deliverer : Person
    {
        public Contract Contract { get; set; }
        public int DeliveryCount { get; set; }

        public Deliverer(string firstName, string lastName, Contract contract) : base(firstName, lastName)
        {
           Contract = contract;
            DeliveryCount = 0;
        }

        public int getDeliveryCount()
        {
            return DeliveryCount;
        }

        public void incrementDeliveryCount()
        {
            DeliveryCount++;
        }
    }
}
