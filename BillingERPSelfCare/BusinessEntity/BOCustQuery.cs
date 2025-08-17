using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingERPSelfCare.BusinessEntity
{
    public class BOCustQuery
    {

        public string FullName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }

        public int District { get; set; }

        public int Upazila { get; set; }

        public string Address { get; set; }

        public string Package { get; set; }

        
    }
}