using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoran.Model
{
    public class Restaurant : Entity
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public List<Meal> Meals { get; set; }
        public List<Chef> Chefs { get; set; }
        public List<Waiter> Waiters { get; set; }
        public List<Deliverer> Deliverers { get; set; }

        public Restaurant(long id, string name, Address address, List<Meal> meals, List<Chef> chefs, List<Waiter> waiters, List<Deliverer> deliverters) : base(id)
        {
            Name = name;
            Address = address;
            Meals = meals;
            Chefs = chefs;
            Waiters = waiters;
            Deliverers = deliverters;
        }
    }
}
