using System;
using System.Collections.Generic;
using System.Text;

namespace DurableFunctioncheck.Models
{
    public class InputDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string CardNumber { get; set; }
        public string RandomStringId { get; set; }
    }
}
