using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Razorpay.Api;
using System.Security.Cryptography;

namespace Company.Function
{
  public static class HttpTriggerCSharp2
  {
    [FunctionName("HttpTriggerCSharp2")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
        ILogger log)
    {
      log.LogInformation("C# HTTP trigger function processed a request.");

      string razorpay_payment_id = req.Query["razorpay_payment_id"];
      string razorpay_order_id = req.Query["razorpay_order_id"];
      string razorpay_signature = req.Query["razorpay_signature"];
      Dictionary<string, string> options = new Dictionary<string, string>();
      options.Add("razorpay_payment_id", razorpay_payment_id);
      options.Add("razorpay_order_id", razorpay_order_id);
      options.Add("razorpay_signature", razorpay_signature);
      try
      {
        Utils.verifyPaymentSignature(options);
        return new RedirectResult("http://localhost:3000", true); //Mention the url you want to redirect to.
      }
      catch (Exception e)
      {
        return new ObjectResult(e.Message);
      }
    }
  }
}
