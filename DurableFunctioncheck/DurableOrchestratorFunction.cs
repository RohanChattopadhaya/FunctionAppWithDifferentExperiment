using DurableFunctioncheck.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DurableFunctioncheck
{
    public class DurableOrchestratorFunction
    {
        [FunctionName("OrchestratorGrocery")]
        public static async Task<DeliveryDto> RunOrchestrator(
           [OrchestrationTrigger] IDurableOrchestrationContext context)
        {

            var req = context.GetInput<InputDto>();

            var addGrocery = new AddGroceryDto()
            {
                Name = req.Name,
                Quantity = req.Quantity
            };

            //var groceryResult = await context.CallActivityAsync<Guid>(
            //   nameof(GroceryAddActivity),
            //   addGrocery);

            var groceryResult = await context.CallActivityWithRetryAsync<Guid>(
               nameof(GroceryAddActivity),
                new RetryOptions(
                    firstRetryInterval: TimeSpan.FromSeconds(5),
                    maxNumberOfAttempts: 3),
               addGrocery);

            context.SetCustomStatus("30%");

            var payment = new PaymentDto()
            {
                GroceryId = groceryResult,
                CardNumber = req.CardNumber
            };

            var paymentResult = await context.CallActivityAsync<PaymentDto>(
               nameof(PaymentActivityfunction),
               payment);

            context.SetCustomStatus("70%");

            var delivery = new DeliveryDto()
            {
                GroceryId = paymentResult.GroceryId,
                PaymentId = paymentResult.PaymentId,
            };

            var deliveryResult = await context.CallActivityWithRetryAsync<DeliveryDto>(
               nameof(DeliveryActivityFunction),
               new RetryOptions(
                    firstRetryInterval: TimeSpan.FromSeconds(5),
                    maxNumberOfAttempts: 3),
               delivery);

            context.SetCustomStatus("100%");

            return deliveryResult;


            //rollback can be done // assuming all will return boolean response

            //Task<bool> orderResponse = context.CallActivityAsync<bool>("GroceryAddActivity", req);
            //Task<bool> paymentResponse = context.CallActivityAsync<bool>("PaymentActivityfunction", req);
            //Task<bool> deliveryResponse = context.CallActivityAsync<bool>("DeliveryActivityFunction", req);

            //await Task.WhenAll(orderResponse, paymentResponse, deliveryResponse);

            //if (orderResponse.Result == false || paymentResponse.Result == false || deliveryResponse.Result == false)
            //{
            //    await context.CallActivityAsync("RollbackGroceryAddActivity", req);
            //    await context.CallActivityAsync("RollbackPaymentActivity", req);
            //    await context.CallActivityAsync("RollbackDeliveryActivity", req);

            //    //return false;
            //}

            //rollback can be done // assuming all will return boolean response
        }
    }
}
