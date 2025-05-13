using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoran.Model
{
    public class Order : Entity
    {
        public Restaurant Restaurant { get; set; }
        public List<Meal> Meals { get; set; }
        public Deliverer Deliverer { get; set; }
        public DateTime DeliveryDateAndTime { get; set; }

        public Order(long id, Restaurant restaurant, List<Meal> meals, Deliverer deliverer, DateTime deliveryDateAndTime) : base(id)
        {
            Restaurant = restaurant;
            Meals = meals;
            Deliverer = deliverer;
            DeliveryDateAndTime = deliveryDateAndTime;
        }

        public decimal GetTotalPrice()
        {
            decimal price = decimal.Zero;

            foreach (Meal meal in Meals)
            {
                price += meal.Price;
            }
            return price;
        }
    }
}
