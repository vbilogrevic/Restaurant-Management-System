using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoran.Model
{
    public sealed class VeganMeal : Meal, IVegan
    {

        public int Calories { get; set; }

        public VeganMeal(long id, string name, Category category, List<Ingredient> ingredients, decimal price, int calories) : base(id, name, category, ingredients, price)
        {
            Calories = calories;
        }

        public bool IsLowCalorie()
        {
            return Calories < 450;
        }
    }
}
