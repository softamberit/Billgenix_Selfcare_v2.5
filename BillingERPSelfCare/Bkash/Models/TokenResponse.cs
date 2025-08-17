using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingERPSelfCare.Bkash.Models
{
    public class TokenResponse
    {
        public string statusCode { get; set; }
        public string statusMessage { get; set; }
        public string id_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
    }
}