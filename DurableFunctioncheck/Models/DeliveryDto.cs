using System;
using System.Collections.Generic;
using System.Text;

namespace DurableFunctioncheck.Models
{
    public class DeliveryDto
    {

        public Guid DeliveryId { get; set; }
        public Guid PaymentId { get; set; }
        public Guid GroceryId { get; set; }
        public bool Status { get; set; }
    }
}
