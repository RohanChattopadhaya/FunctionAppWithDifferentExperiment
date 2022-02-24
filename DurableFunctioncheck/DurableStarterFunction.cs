using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using DurableFunctioncheck.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DurableFunctioncheck
{
    public static class DurableStarterFunction
    {
        [FunctionName("DurableStarterFunction")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "start/{orchestratorName}/{id?}")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            string orchestratorName,
            string id,
            ILogger log)
        {

            var requestBody = await req.Content.ReadAsAsync<InputDto>();
            //var data = JsonConvert.DeserializeObject<InputDto>(requestBody.ToString());

            string instanceId = id;
            // Function input comes from the request content.
            //instanceId = await starter.StartNewAsync("OrchestratorGrocery", req);
            instanceId = await starter.StartNewAsync(orchestratorName, instanceId, requestBody);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}