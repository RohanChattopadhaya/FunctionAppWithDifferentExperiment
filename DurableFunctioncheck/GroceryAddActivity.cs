using DurableFunctioncheck.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DurableFunctioncheck
{
    public class GroceryAddActivity
    {
        [FunctionName(nameof(GroceryAddActivity))]
        public async Task<Guid> GroceryAdd([ActivityTrigger] AddGroceryDto addGroceryDto, ILogger log)
        {
            addGroceryDto.GroceryId = Guid.NewGuid();

           //storage logic can be added --
           
            log.LogInformation($"Grocery added {addGroceryDto.Name}");

            return addGroceryDto.GroceryId;
        }
    }
}
