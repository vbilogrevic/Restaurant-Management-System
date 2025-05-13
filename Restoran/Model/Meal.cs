using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoran.Model
{
    public class Meal : Entity
    {
        public string Name { get; set; }
        public Category Category { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public decimal Price { get; set; }

        public Meal(long id, string name, Category category, List<Ingredient> ingredients, decimal price) : base(id)
        {
            Name = name;
            Category = category;
            Ingredients = ingredients;
            Price = price;
        }
    }
}
