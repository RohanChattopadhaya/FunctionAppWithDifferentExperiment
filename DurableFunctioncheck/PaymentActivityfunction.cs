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
    public class PaymentActivityfunction
    {
        [FunctionName(nameof(PaymentActivityfunction))]
        public async Task<PaymentDto> Payment([ActivityTrigger] PaymentDto paymentDto, ILogger log)
        {
            paymentDto.PaymentId = Guid.NewGuid();

            //storage logic can be added --

            paymentDto.Status = true;
            log.LogInformation($"Payment done {paymentDto.PaymentId}");

            return paymentDto;
        }
    }
}
