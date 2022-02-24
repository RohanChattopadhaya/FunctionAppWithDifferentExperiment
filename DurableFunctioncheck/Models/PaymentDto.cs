using System;
using System.Collections.Generic;
using System.Text;

namespace DurableFunctioncheck.Models
{
    public class PaymentDto
    {
        public Guid PaymentId { get; set; }
        public Guid GroceryId { get; set; }
        public string CardNumber { get; set; }
        public bool Status { get; set; }
    }
}
