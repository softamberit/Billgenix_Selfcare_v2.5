using BillingERPSelfCare.BusinessEntity;
using BillingERPConn;
using System;
using System.Collections;
using System.Data;
using BillingERPSelfCare.Utility;
namespace BillingERPSelfCare.DataAccess
{
    public class DACustomer
    {
        DBUtility objDBUitility = new DBUtility();


        public string CustomerEntry(BOCustomer obj)
        {
            Hashtable ht = new Hashtable();

            // ProcessID 1 For Customer Insert
            ht.Add("ProcessID", 1);

            ht.Add("CustomerTypeId", obj.CustomerTypeId);
            ht.Add("CustomerName", obj.CustomerName);
            ht.Add("Attention", obj.Attention);
            ht.Add("Address", obj.Address);
            ht.Add("Mobile", obj.Mobile);
            ht.Add("Email", obj.Email);
            ht.Add("NationalityId", obj.NationalityId);
            ht.Add("NID", obj.NID);
            ht.Add("OccupationId", obj.OccupationId);
            ht.Add("POPId", obj.POPId);
            ht.Add("SalesDate", obj.SalesDate);
            ht.Add("EntryNotes", obj.EntryNotes);
            ht.Add("BID", obj.BID);
            ht.Add("InternetMRC", obj.InternetMRC);
            ht.Add("InternetOTC", obj.InternetOTC);
            ht.Add("IpTvMRC", obj.IpTvMRC);
            ht.Add("IpTvOTC", obj.IpTvOTC);
            ht.Add("TotalMRC", obj.TotalMRC);
            ht.Add("TotalOTC", obj.TotalOTC);
            //ht.Add("IsIpTvIncluded", obj.IsIpTvIncluded);
            ht.Add("TranModeID", obj.TranModeID);
            ht.Add("MRNO", obj.MRNO);
            ht.Add("MRAmount", obj.MRAmount);

            ht.Add("StatusEntryBy", obj.StatusEntryBy);
            ht.Add("EntryID", obj.EntryID);

            DataTable dt = objDBUitility.GetDataByProc(ht, "CustomerInsertUpdate");

            string CustomerId = string.Empty;

            foreach (DataRow dataRow in dt.Rows)
            {
                CustomerId = dataRow["CustomerId"].ToString();
            }

            return CustomerId;
        }

      

       

        public DataTable ApproveCustomer(BOCustomer boItem)
        {

            try
            {

                Hashtable hashTable1 = new Hashtable();
                hashTable1.Add("ProcessID", 3);
                hashTable1.Add("CustomerID", boItem.CustomerID);
                hashTable1.Add("Discount", boItem.Discount);
                hashTable1.Add("CreditLimit", boItem.CreditLimit);
                hashTable1.Add("NetMRC", boItem.NetMRC);
                hashTable1.Add("ApprovalComments", boItem.ApprovalComments);
                hashTable1.Add("ApproveBy", boItem.Approveby);



                DataTable Dtab = objDBUitility.GetDataByProc(hashTable1, "customerinsertupdate");

                return Dtab;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CustomerUpdate(BOCustomer obj)
        {
            string feedback = string.Empty;

            Hashtable ht = new Hashtable();

            // ProcessID 2 For Customer Update
            ht.Add("ProcessID", 2);

            ht.Add("CustomerID", obj.CustomerID);
            ht.Add("CustomerTypeId", obj.CustomerTypeId);
            ht.Add("CustomerName", obj.CustomerName);
            ht.Add("Attention", obj.Attention);
            ht.Add("Address", obj.Address);
            ht.Add("Mobile", obj.Mobile);
            ht.Add("Email", obj.Email);
            ht.Add("NationalityId", obj.NationalityId);
            ht.Add("NID", obj.NID);
            ht.Add("OccupationId", obj.OccupationId);
            ht.Add("POPId", obj.POPId);
            ht.Add("EntryNotes", obj.EntryNotes);
            ht.Add("BID", obj.BID);
            ht.Add("InternetMRC", obj.InternetMRC);
            ht.Add("InternetOTC", obj.InternetOTC);
            ht.Add("IpTvMRC", obj.IpTvMRC);
            ht.Add("IpTvOTC", obj.IpTvOTC);
            ht.Add("TotalMRC", obj.TotalMRC);
            ht.Add("TotalOTC", obj.TotalOTC);
            //ht.Add("IsIpTvIncluded", obj.IsIpTvIncluded);
            ht.Add("TranModeID", obj.TranModeID);
            ht.Add("MRNO", obj.MRNO);
            ht.Add("MRAmount", obj.MRAmount);

            ht.Add("UpdateID", obj.UpdateID);

            DataTable dt = objDBUitility.GetDataByProc(ht, "CustomerInsertUpdate");

            foreach (DataRow dataRow in dt.Rows)
            {
                feedback = dataRow["Feedback"].ToString();
            }

            return feedback;
        }


        #region Installation

        public long InsertLogicalInfo(BOCustomer boItem)
        {
            try
            {

                Hashtable ht = new Hashtable();
                ht.Add("ProcessID", 4);
                ht.Add("CustomerID", boItem.CustomerID);
                ht.Add("LogicalInstallDate", boItem.LogicalInstallDate);
                ht.Add("LogicalPOPID", boItem.LogicalPOPID);
                ht.Add("VLAN", boItem.VLAN);
                ht.Add("IPAddress", boItem.IPAddress);
                ht.Add("RemarksLogical", boItem.RemarksLogical);
                ht.Add("PinNo", boItem.PinNo);
                long res = objDBUitility.InsertData(ht, "customerinsertupdate");

                return res;

            }
            catch (Exception ex)
            {
                Message.Show(ex.Message.ToString());
                return 0;
            }
        }


        public long InsertPhysicalInfo(BOCustomer boItem)
        {

            try
            {

                Hashtable ht = new Hashtable();
                ht.Add("ProcessID", 5);
                ht.Add("CustomerID", boItem.CustomerID);
                ht.Add("PhysicalInstallDate", boItem.PhysicalInstallDate);
                ht.Add("POPId", boItem.POPId);
                ht.Add("RouterID", boItem.RouterID);
                ht.Add("Installby", boItem.Installby);
                ht.Add("RemarksPhysical", boItem.RemarksPhysical);
                ht.Add("PinNo", boItem.PinNo);
                long res = objDBUitility.InsertData(ht, "customerinsertupdate");

                return res;

            }
            catch (Exception ex)
            {
                Message.Show(ex.Message.ToString());
                return 0;
            }
        }


        #endregion



        public string HandoverIsDone(BOCustomer obj)
        {
            Hashtable ht = new Hashtable();

            // ProcessID 6 For Customer Handover
            ht.Add("ProcessID", 6);

            ht.Add("CustomerID", obj.CustomerID);
            ht.Add("HandoverDate", obj.HandoverDate);
            ht.Add("HandoverEntryBy", obj.HandoverEntryBy);

            DataTable dt = objDBUitility.GetDataByProc(ht, "CustomerInsertUpdate");

            string feedback = string.Empty;

            foreach (DataRow dataRow in dt.Rows)
            {
                feedback = dataRow["Feedback"].ToString();
            }

            return feedback;
        }




        #region Installation CR

        public long InsertLogicalInfoCR(BOCustomer boItem)
        {
            try
            {

                Hashtable ht = new Hashtable();
                ht.Add("ProcessID", 1);
                ht.Add("RequestRefNo", boItem.RequestRefNo);
                ht.Add("LogicalInstallDate", boItem.LogicalInstallDate);
                ht.Add("LogicalPOPID", boItem.LogicalPOPID);
                ht.Add("VLAN", boItem.VLAN);
                ht.Add("IPAddress", boItem.IPAddress);
                ht.Add("RemarksLogical", boItem.RemarksLogical);
                ht.Add("PinNo", boItem.PinNo);
                long res = objDBUitility.InsertData(ht, "sp_insertinstallationinfoCR");

                return res;

            }
            catch (Exception ex)
            {
                Message.Show(ex.Message.ToString());
                return 0;
            }
        }


        public long InsertPhysicalInfoCR(BOCustomer boItem)
        {

            try
            {

                Hashtable ht = new Hashtable();
                ht.Add("ProcessID", 2);
                ht.Add("RequestRefNo", boItem.RequestRefNo);
                ht.Add("PhysicalInstallDate", boItem.PhysicalInstallDate);
                ht.Add("POPId", boItem.POPId);
                ht.Add("RouterID", boItem.RouterID);
                ht.Add("Installby", boItem.Installby);
                ht.Add("RemarksPhysical", boItem.RemarksPhysical);
                ht.Add("PinNo", boItem.PinNo);
                long res = objDBUitility.InsertData(ht, "sp_insertinstallationinfoCR");

                return res;

            }
            catch (Exception ex)
            {
                Message.Show(ex.Message.ToString());
                return 0;
            }
        }


        #endregion


        public bool IsAlreadyRequested(string CustomerID, int statusID)
        {
            bool res = false;

            try
            {
                DataTable dt = objDBUitility.GetDataBySQLString("select * from RequestMaster where CustomerID = '" + CustomerID + "' AND StatusID = " + statusID + "  AND ISNULL(IsCompleted,0)=0");
                if (dt.Rows.Count > 0)
                    res = true;

            }
            catch (Exception)
            {

                res =false;
            }

            return res;
        }


    }
}