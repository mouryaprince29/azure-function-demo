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
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];
            // Key Vault se secrets automatically fetch honge!
            string whatsappkey = Environment.GetEnvironmentVariable("WHATSAPP_TOKEN");
            // Validation
            bool whatsappOk = !string.IsNullOrEmpty(whatsappkey);
            _logger.LogInformation($"Whatsapp Key loaded: {whatsappOk}");
            // Timestamp add karo response mein
            var response = new
            {
                message = "Key Vault Integration Test",
                secretsLoaded = new
                {
                    whatsappApiKey = whatsappOk ? "Loaded (first 10 chars: " + whatsappkey?.Substring(0, 10) + "...)" : "Failed"
                },
                executedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                executionId = Guid.NewGuid().ToString()
            };

            return new OkObjectResult(response);
        }
    }
}
