using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingERPSelfCare.BusinessEntity
{
    public class BOUser
    {

        public string UserType { get; set; }
        public int PinNo { get; set; }
        public string LoginId { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string UserName { get; set; }

        public bool IsERPUser { get; set; }

        public object ProcessID { get; set; }

        public object UserID { get; set; }
    }
}