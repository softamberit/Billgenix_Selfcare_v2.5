using BillingERPConn;
using BillingERPSelfCare.Session;
using BillingERPSelfCare.Utility;
using ReportBilling;
using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Reporting.Processing;
using Telerik.Web.UI;

namespace BillingERPSelfCare.Accounts
{
    public partial class InvoiceDetails : BasePageClass
    {
        DBUtility db = new DBUtility();

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[SessionInfo.loginid] == null)
            {
                Response.Redirect("~/LogOut.aspx");
            }
            Literal pageLtl = ((Literal)PageUtility.FindControlIterative(this.Master, "ltlPageName"));
            if (pageLtl != null)
            {
                pageLtl.Text = "Invoice Details";
            }

            if (!IsPostBack)
            {

                LoadRequestList();
                LoadAmount();
            }

        }
        #endregion

        #region List
        private void LoadRequestList()
        {
            try
            {

                Hashtable ht = new Hashtable();
                ht.Add("CustomerID", (Session[SessionInfo.loginid].ToString()));
                DataTable dataTableDetails = db.GetDataByProc(ht, "sp_getInvoiceDetails");
                grdRequestList.DataSource = dataTableDetails;
                grdRequestList.DataBind();
                grdRequestList.MasterTableView.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                grdRequestList.MasterTableView.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                grdRequestList.MasterTableView.GetColumn("Amount").ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message);
            }

        }

        private void LoadAmount()
        {
            Hashtable ht = new Hashtable();
            ht.Add("CustomerID", Session[SessionInfo.loginid].ToString());
            DataTable dataTableDetails = db.GetDataByProc(ht, "sp_getInvoiceDetailsAmount");

            foreach (DataRow dataRow in dataTableDetails.Rows)
            {
                txtamount.Text = dataRow["Amount"].ToString();
            }

        }
        #endregion

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {

                RadButton Button = (RadButton)sender;
                GridDataItem item = (GridDataItem)Button.NamingContainer;

                string RefNo = (item["RefNo"].Text);


                DataTable dt = db.GetDataBySQLString("Select SNID,ISNULL(IsVatOption,0) as IsVatOption from BillingMaster Where RefNo = '" + RefNo + "'");
                
                int IsVatOption = Convert.ToInt32(dt.Rows[0]["IsVatOption"]);

                if (dt.Rows.Count == 0)
                {
                    var rpt = new REPORT_TEMP_INVOICE();

                    rpt.CustomerID = Session[SessionInfo.loginid].ToString();
                    ExportReport(rpt, "InvoicReport");
                }
                else
                {
                    if(IsVatOption == 1)
                    {
                        var rpt = new REPORT_INVOICEVAT();
                        rpt.RefNo = RefNo;
                        ExportReport(rpt, "InvoicReport");
                    }
                    else
                    {
                        var rpt = new REPORT_INVOICE();
                        rpt.RefNo = RefNo;
                        ExportReport(rpt, "InvoicReport");
                    }
                    
                }

            }
            catch (Exception ex)
            {

                Message.Show(ex.Message.ToString());
            }
        }



        private void ExportReport(Telerik.Reporting.Report rpt, string fileName)
        {
            ReportProcessor reportProcessor = new ReportProcessor();
            Telerik.Reporting.InstanceReportSource instanceReportSource = new Telerik.Reporting.InstanceReportSource();
            instanceReportSource.ReportDocument = rpt;
            RenderingResult result = reportProcessor.RenderReport("PDF", instanceReportSource, null);

            fileName = fileName + "." + result.Extension;

            Response.Clear();
            Response.ContentType = result.MimeType;
            Response.Cache.SetCacheability(HttpCacheability.Private);
            Response.Expires = -1;
            Response.Buffer = true;

            Response.AddHeader("Content-Disposition",
                               string.Format("{0};FileName=\"{1}\"",
                                             "attachment",
                                             fileName));

            Response.BinaryWrite(result.DocumentBytes);
            Response.End();

          


        }

        protected void grdRequestList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            Hashtable ht = new Hashtable();
            ht.Add("CustomerID", (Session[SessionInfo.loginid].ToString()));
            DataTable dataTableDetails = db.GetDataByProc(ht, "sp_getInvoiceDetails");
            grdRequestList.DataSource = dataTableDetails;
        }
    }
}