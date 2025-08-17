using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingERPSelfCare.Bkash.Models
{
    public class TransactionStatusResponse
    {
        public string trxID { get; set; }
        public string initiationTime { get; set; }
        public string completedTime { get; set; }
        public string transactionType { get; set; }
        public string customerMsisdn { get; set; }
        public string payerAccount { get; set; }
        public string transactionStatus { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string organizationShortCode { get; set; }
        public string serviceFee { get; set; }
        public string payerType { get; set; }
        public string creditedAmount { get; set; }
        public string statusCode { get; set; }
        public string statusMessage { get; set; }
    }
}