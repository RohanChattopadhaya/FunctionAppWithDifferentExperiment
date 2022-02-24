using System;
using System.Collections.Generic;
using System.Text;

namespace DurableFunctioncheck.Models
{
    public class AddGroceryDto
    {
        public Guid GroceryId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
