using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingERPSelfCare.Bkash.Models
{
    public class AgreementCreateRequest
    {
        public string mode { get; set; }
        public string callbackURL { get; set; }
        public string payerReference { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string intent { get; set; }
        public string merchantInvoiceNumber { get; set; }
    }
}