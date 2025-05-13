using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoran.Model
{
    public sealed class MeatMeal : Meal, IMeat
    {

        public string MeatType { get; set; }

        public MeatMeal(long id, string name, Category category, List<Ingredient> ingredients, decimal price, string meatType) : base(id, name, category, ingredients, price)
        {
            MeatType = meatType;
        }

        public void RecommendedSideDish()
        {
            Console.WriteLine("Pomfri");
        }
    }
}
