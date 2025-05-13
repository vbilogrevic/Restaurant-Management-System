using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoran.Model
{
    public class Ingredient : Entity
    {
        public string Name { get; set; }
        public Category Category { get; set; }
        public decimal Kcal { get; set; }
        public string PreparationMethod { get; set; }

        public Ingredient(long id, string name, Category category, decimal kcal, string preparationMethod) : base(id)
        {
            Name = name;
            Category = category;
            Kcal = kcal;
            PreparationMethod = preparationMethod;
        }
    }
}
