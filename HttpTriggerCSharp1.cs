using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Razorpay.Api;

using System.Collections.Generic;

namespace Company.Function
{
  public static class HttpTriggerCSharp1
  {
    [FunctionName("HttpTriggerCSharp1")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
        ILogger log)
    {
      RazorpayClient client = new RazorpayClient(
      System.Environment.GetEnvironmentVariable("key_id"),
      System.Environment.GetEnvironmentVariable("key_secrete"));

      log.LogInformation("C# HTTP trigger function processed a request.");

      string name = req.Query["name"];

      string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      dynamic data = JsonConvert.DeserializeObject(requestBody);
      name = name ?? data?.name;
      Dictionary<string, object> options = new Dictionary<string, object>();
      options.Add("amount", data.amount); // amount in the smallest currency unit
      options.Add("receipt", data.receipt);
      options.Add("currency", data.currency);
      options.Add("payment_capture", data.paymentcapture);
      Console.WriteLine(options);
      try
      {
        Order order = client.Order.Create(options);
        Console.Write("->", order);
        return new OkObjectResult(order);
      }
      catch (Exception e)
      {
        Console.WriteLine("error", e);
      }

      string responseMessage = string.IsNullOrEmpty(name)
          ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
          : $"Hello, {name}. This HTTP triggered function executed successfully.";

      return new OkObjectResult(responseMessage);
    }
  }
}
