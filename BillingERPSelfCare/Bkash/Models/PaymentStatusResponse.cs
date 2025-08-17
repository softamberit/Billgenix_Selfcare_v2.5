using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingERPSelfCare.Bkash.Models
{
    public class PaymentStatusResponse
    {
        public string paymentID { get; set; }
        public string trxID { get; set; }
        public string agreementID { get; set; }
        public string mode { get; set; }
        public string paymentCreateTime { get; set; }
        public string paymentExecuteTime { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string intent { get; set; }
        public string merchantInvoice { get; set; }
        public string transactionStatus { get; set; }
        public string serviceFee { get; set; }
        public string creditedAmount { get; set; }
        public string verificationStatus { get; set; }
        public string payerReference { get; set; }
        public string payerType { get; set; }
        public string agreementStatus { get; set; }
        public string agreementCreateTime { get; set; }
        public string agreementExecuteTime { get; set; }
        public string statusCode { get; set; }
        public string statusMessage { get; set; }
    }
}