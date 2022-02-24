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
    public class DeliveryActivityFunction
    {
        [FunctionName(nameof(DeliveryActivityFunction))]
        public async Task<DeliveryDto> Delivery([ActivityTrigger] DeliveryDto deliveryDto, ILogger log)
        {
            deliveryDto.DeliveryId = Guid.NewGuid();

            //storage logic can be added --

            deliveryDto.Status = true;
            log.LogInformation($"Delivery In Process {deliveryDto.PaymentId}");

            return deliveryDto;
        }
    }
}
