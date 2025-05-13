using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restoran.Model
{
    public abstract class Entity
    {
        public long Id { get; set; }

        protected Entity(long id)
        {
            Id = id;
        }
    }
}
