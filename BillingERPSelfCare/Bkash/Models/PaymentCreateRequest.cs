using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingERPSelfCare.Bkash.Models
{
    public class PaymentCreateRequest
    {
        public string paymentID { get; set; }
        public string bkashURL { get; set; }
        public string callbackURL { get; set; }
        public string successCallbackURL { get; set; }
        public string failureCallbackURL { get; set; }
        public string cancelledCallbackURL { get; set; }
        public decimal amount { get; set; } // Assuming amount is numeric
        public string intent { get; set; }
        public string currency { get; set; }
        public string paymentCreateTime { get; set; } // DateTime for time representation
        public string transactionStatus { get; set; }
        public string merchantInvoiceNumber { get; set; }
        public string statusCode { get; set; }
        public string statusMessage { get; set; }
    }
}