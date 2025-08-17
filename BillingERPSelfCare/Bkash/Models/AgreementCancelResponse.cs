using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingERPSelfCare.Bkash.Models
{
    public class AgreementCancelResponse
    {
        public string paymentID { get; set; }
        public string agreementID { get; set; }
        public string payerReference { get; set; }
        public string agreementVoidTime { get; set; }
        public string agreementStatus { get; set; }
        public string statusCode { get; set; }
        public string statusMessage { get; set; }
    }
}