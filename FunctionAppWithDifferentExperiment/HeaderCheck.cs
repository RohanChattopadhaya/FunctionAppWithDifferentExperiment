using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionAppWithDifferentExperiment
{
    public static class HeaderCheck
    {
        [Function("HeaderCheck")]
        public static async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("Function1");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            string stringdata =  await new StreamReader(req.Body).ReadToEndAsync();  // request body data

            dynamic data = JsonConvert.DeserializeObject(stringdata);
            var dt = data["Name"]; // dynamic data

            var headerdata = req.Headers.GetValues("Auth").First(); //Header value check
            //var headerdata = headers.FirstOrDefault(x => x.Key == "Auth");

            var query = HttpUtility.ParseQueryString(req.Url.Query); // query string parameter check
            var queryData = query["Code"];

            var response = req.CreateResponse(HttpStatusCode.OK);
            //response.Headers.Add("Content-Type", "application/json");

            response.WriteString("Welcome to Azure Functions!");


            return response;

        }
    }
}
