using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MyFirstAzureFunction
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function("Function1")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("Function triggered!");
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            return new OkObjectResult($"Hello, {name ?? "Azure Function User"}! Welcome to serverless!");
        }
    }
}
