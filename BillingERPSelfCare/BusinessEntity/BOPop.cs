using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingERPSelfCare.BusinessEntity
{
    public class BOPop
    {
        public int PinNo { get; set; }
        public int POPID { get; set; }
        public int CreatedBy { get; set; }
        public int Id { get; set; }
        public string PopName { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsActive { get; set; }

        public string CreatedDateStr { get; set; }

        public string UpdatedDateStr { get; set; }

        public string RouterName { get; set; }

        public int RouterId { get; set; }

        public int PopId { get; set; }

        public string OltName { get; set; }

        public int Capacity { get; set; }

        public int NoOfPon { get; set; }

        public string Splitter { get; set; }

        public int OltId { get; set; }

        public string ChkBox { get; set; }

        public string JsonString { get; set; }

        public string viewButton { get; set; }

        public int SplitterId { get; set; }

        public int SplitterL1Id { get; set; }

        public int SplitterL2Id { get; set; }

        public string ONTName { get; set; }

        public int OntId { get; set; }

        public string Name { get; set; }

        public int ServiceID { get; set; }

        public string ServiceName { get; set; }

        public int BID { get; set; }

        public string Bandwidth { get; set; }
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Edit { get; set; }
        public int ProcessDay { get; set; }


    }
    public class DropDownLoad
    {
        public int ValId { get; set; }
        public string ValName { get; set; }
    }
    public class DropDownLoad2
    {
        public string ValId { get; set; }
        public string ValName { get; set; }
    }


}