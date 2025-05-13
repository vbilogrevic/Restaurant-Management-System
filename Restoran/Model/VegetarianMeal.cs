using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoran.Model
{
    public sealed class VegetarianMeal : Meal, IVegetarian
    {
        public bool ContainsNuts { get; set; }

        public VegetarianMeal(long id, string name, Category category, List<Ingredient> ingredients, decimal price, bool containsNuts) : base(id, name, category, ingredients, price)
        {
            ContainsNuts = containsNuts;
        }

        public void RecommendedDrink()
        {
            Console.WriteLine("Uz ovo jelo preporučujemo bijelo vino.");
        }
    }
}
