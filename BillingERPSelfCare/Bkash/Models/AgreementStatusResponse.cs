using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingERPSelfCare.Bkash.Models
{
    public class AgreementStatusResponse
    {
        public string agreementID { get; set; }
        public string paymentID { get; set; }
        public string agreementStatus { get; set; }
        public string agreementCreateTime { get; set; }
        public string agreementExecuteTime { get; set; }
        public string payerReference { get; set; }
        public string customerMsisdn { get; set; }
        public string payerAccount { get; set; }
        public string payerType { get; set; }
        public string statusCode { get; set; }
        public string statusMessage { get; set; }
    }
}