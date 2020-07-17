[![GitHub license](https://img.shields.io/github/license/officialsanjaysharma/3rd-party-payment-azure-functions-Csharp-razorpay?style=flat-square)](https://github.com/officialsanjaysharma/3rd-party-payment-azure-functions-Csharp-razorpay/blob/master/License)
![npm](https://img.shields.io/npm/v/ng-offline?color=blue&style=flat-square)

# 3rd party payment with Razorpay

A simple dotnet project which will show end to end payment system with razorpay using <b>Microsoft Azure functions</b>.

### Installing

```bash
$ dotnet restore
```

Generate <b>key_secret</b> and <b>key_id</b> from razorpay and put both in <b>local.settings.json</b>

```
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "key_id":"",
    "key_secret":""
  }
}
```

###Steps

1. Create an Order 
  Request
    - Make a POST call to <b>http://localhost:7071/api/HttpTriggerCSharp1</b> with parameters in body.
        ```
        {
        "amount":5000,
        "currency":"INR",
        "receipt":"order_rcptid_12",
        "paymentcapture":"0"
        }
        ```

   Response
    - This is the type of request you should expect if everything goes right.
      ```
      "attributes": {
          "id": "order_FFQNPUJmGInOwb",
          "entity": "order",
          "amount": 5000,
          "amount_paid": 0,
          "amount_due": 5000,
          "currency": "INR",
          "receipt": "order_rcptid_12",
          "offer_id": null,
          "status": "created",
          "attempts": 0,
          "notes": [],
          "created_at": 1594958691
      }
      ```
2. The id received in response will be used as a order_id.
   Fill all necessary details
    ```
    <form action="http://localhost:7071/api/HttpTriggerCSharp2" method="GET">  // Replace this with your website's success callback URL
    <script    src="https://checkout.razorpay.com/v1/checkout.js"    
    data-key="Key" // Enter the Key ID generated from the Dashboard    
    data-amount="50000" // Amount is in currency subunits. Default currency is INR. Hence, 50000 refers to 50000 paise    
    data-currency="INR"    
    data-order_id="orderId received after creating order"//This is a sample Order ID. Pass the `id` obtained in the response of the previous step.    
    data-buttontext="Pay with Razorpay"    
    data-name="Acme Corp"    
    data-description="Test transaction"    
    data-image="https://example.com/your_logo.jpg"    
    data-prefill.name="Gaurav Kumar"    
    data-prefill.email="gaurav.kumar@example.com"    
    data-prefill.contact="7017152581"    
    data-theme.color="#F37254">
    </script>
    <input type="hidden" custom="Hidden Element" name="hidden"></form>
    ```
3. Open html page in browser and click on payment and complete the dummy payment.
4. After successful completion of payment you will redirect to the mention url. In our case it is http://localhost:7071/api/HttpTriggerCSharp2.
5. After successful verification you will be redirected to the url defined in <b>HttpTriggerCSharp2.cs</b> file.
  ```
  return new RedirectResult("http://localhost:3000", true);
  ```
###License
Published under the [MIT License](https://github.com/officialsanjaysharma/3rd-party-payment-azure-functions-Csharp-razorpay/blob/master/License)..
