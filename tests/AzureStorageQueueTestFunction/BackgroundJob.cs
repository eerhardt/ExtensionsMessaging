using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureStorageQueueTestFunction
{
    public static class BackgroundJob
    {
        [FunctionName("Function1")]
        public static void Run([QueueTrigger("eerhardt-test", Connection = "test-queue")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
