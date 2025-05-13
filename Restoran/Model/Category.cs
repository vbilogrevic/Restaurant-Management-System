using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoran.Model
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Category(long id, string name, string description) : base(id)
        {
            Name = name;
            Description = description;
        }
    }
}
