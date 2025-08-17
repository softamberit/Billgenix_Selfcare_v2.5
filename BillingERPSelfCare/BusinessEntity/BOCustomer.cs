using System;

namespace BillingERPSelfCare.BusinessEntity
{
    public class BOCustomer
    {
        public int SNID { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Attention { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string EntryNotes { get; set; }
        public bool IsApproved { get; set; }
        public int Approveby { get; set; }
        public DateTime ApproveDate { get; set; }
        public string ApprovalComments { get; set; }
        public int StatusID { get; set; }
        public int StatusEntryBy { get; set; }
        public DateTime StatusEntryTime { get; set; }
        public string SecondaryStatus { get; set; }
        public string EntryID { get; set; }
        public DateTime EntryDate { get; set; }
        public string UpdateID { get; set; }
        public DateTime UpdateDate { get; set; }
        public int Serial { get; set; }
        public int CustomerTypeId { get; set; }
        public bool IsDeviceCollected { get; set; }
        public int OccupationId { get; set; }
        public string NationalityId { get; set; }
        public string NID { get; set; }
        public DateTime SalesDate { get; set; }
        public string Passport { get; set; }
        public bool IsPhysicalInstallDone { get; set; }
        public DateTime PhysicalInstallDate { get; set; }
        public int PhysicalInstallBy { get; set; }
        public int PhysicalInstallEntryBy { get; set; }
        public DateTime PhysicalInstallEntryTime { get; set; }
        public int POPId { get; set; }
        public int RouterId { get; set; }
        public string PhysicalInstallInfo { get; set; }
        public bool IsLogicalInstallDone { get; set; }
        public DateTime LogicalInstallDate { get; set; }
        public int LogicalInstallBy { get; set; }
        public int LogicalInstallEntryBy { get; set; }
        public DateTime LogicalInstallEntryTime { get; set; }
        public string LogicalInstallInfo { get; set; }
        public string IPAddress { get; set; }
        public string VLAN { get; set; }
        public string Password { get; set; }
        public bool IsHandoverDone { get; set; }
        public DateTime HandoverDate { get; set; }
        public int HandoverBy { get; set; }
        public int HandoverEntryBy { get; set; }
        public DateTime HandoverEntryTime { get; set; }
        public int BID { get; set; }
        public decimal InternetMRC { get; set; }
        public decimal InternetOTC { get; set; }
        public decimal IpTvMRC { get; set; }
        public decimal IpTvOTC { get; set; }
        public decimal TotalMRC { get; set; }
        public decimal TotalOTC { get; set; }
        //public bool IsIpTvIncluded { get; set; }
        public decimal Discount { get; set; }
        public decimal NetMRC { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal MRAmount { get; set; }
        public string MRNO { get; set; }
        public int TranModeID { get; set; }



      
        public int LogicalPOPID { get; set; }          
        public string RemarksLogical { get; set; }       
        public int PhysicalPOPID { get; set; }
        public string RemarksPhysical { get; set; }
        public int Installby { get; set; }
        public string PinNo { get; set; }
        public int RouterID { get; set; }


        public string RequestRefNo { get; set; }


    }
}