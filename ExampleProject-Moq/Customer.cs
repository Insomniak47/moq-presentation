using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleProjectMoq
{
    public class Customer
    {
        public Guid Id { get; }

        public Customer(Guid id)
        {
            Id = id;
        }
    }
}
