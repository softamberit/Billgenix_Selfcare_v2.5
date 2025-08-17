using BillingERPSelfCare.Session;
using BillingERPSelfCare.Utility;
using BillingERPConn;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BillingERPSelfCare.Sales
{
    public partial class SupportTicket : BasePageClass
    {
        DBUtility objDBUitility = new DBUtility();

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGridBody();
            Literal pageLtl = ((Literal)PageUtility.FindControlIterative(this.Master, "ltlPageName"));
            if (!IsPostBack)
            {
                ShowGridBody();
            }

            if (pageLtl != null)
            {
                pageLtl.Text = "Support Ticket";
            }
        }
        #endregion


        #region DropDownValueLoadEvent
        //protected void ddlTicketType_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        //{
        //    try
        //    {
        //        DataTable DT = objDBUitility.GetDataByProc("Tk_GetTicketType");
        //        foreach (DataRow datarow in DT.Rows)
        //        {
        //            RadComboBoxItem item = new RadComboBoxItem();
        //            item.Text = (string)datarow["TicketTypeName"];
        //            item.Value = datarow["TicketTypeID"].ToString();
        //            item.Attributes.Add("TicketTypeName", datarow["TicketTypeName"].ToString());
        //            ddlTicketType.Items.Add(item);
        //            item.DataBind();
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        Message.Show(ex.Message.ToString());
        //    }
        //}
        #endregion


        #region Grid
        private void LoadGridBody()
        {
            try
            {
                Hashtable ht = new Hashtable();
                string CustID = Session[SessionInfo.loginid].ToString();
                ht.Add("CustomerID", CustID);

                DataTable dt = objDBUitility.GetDataByProc(ht, "sp_GetTicketByCustomerId");
                grdShowList.DataSource = dt;
                grdShowList.DataBind();
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message);
            }
        }


        protected void btnShowList_Click(object sender, EventArgs e)
        {
            try
            {
                LoadGridBody();
                ShowGridBody();
                Clear();

            }
            catch (Exception ex)
            {
                Message.Show(ex.Message);
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {


                DivRadio.Visible = false;
                divMessage.Visible = true;
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message);
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ShowEntryBodyForCreate();

            }
            catch (Exception ex)
            {
                Message.Show(ex.Message);
            }
        }
        #endregion



        #region ShowHide

        private void ShowEntryBodyForCreate()
        {
            New.Visible = false;
            EntryBody.Visible = true;
            btnSave.Visible = true;
            GridBody.Visible = false;
            DivRadio.Visible = true;
            divMessage.Visible = false;
        }

        private void ShowGridBody()
        {
            GridBody.Visible = true;
            New.Visible = true;
            EntryBody.Visible = false;
        }
        #endregion


        #region Save
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int TicketType = 0;

                if (RadioButton.Checked == true)
                {
                    TicketType = 5;
                }
                else if (RadioButton1.Checked == true)
                {
                    TicketType = 8;
                }
                else if (RadioButton2.Checked == true)
                {
                    TicketType = 25;
                }
                else if (RadioButton3.Checked == true)
                {
                    TicketType = 47;
                }
                else if (RadioButton4.Checked == true)
                {
                    TicketType = 48;
                }
                else if (RadioButton5.Checked == true)
                {
                    TicketType = 19;
                }
                else if (RadioButton6.Checked == true)
                {
                    TicketType = 49;
                }
                else if (RadioButton7.Checked == true)
                {
                    TicketType = 50;
                }
                else if (RadioButton8.Checked == true)
                {
                    TicketType = 32;
                }
                else if (RadioButton9.Checked == true)
                {
                    TicketType = 51;
                }
                else if (RadioButton10.Checked == true)
                {
                    TicketType = 52;
                }
                else if (RadioButton11.Checked == true)
                {
                    TicketType = 53;
                }


                string CustID = Session[SessionInfo.loginid].ToString();

                Hashtable ht = new Hashtable
                        {
                            {"CustomerID", CustID},
                            {"TicketType",TicketType},
                            {"Message", txtMessage.Text.Trim()}
                        };

                DataTable dt = objDBUitility.GetDataByProc(ht, "sp_InsertSelfCareTicket");

                foreach (DataRow dataRow in dt.Rows)
                {
                    Message.Show(dataRow["Feedback"].ToString());

                    if (!dataRow["Feedback"].ToString().Contains("Exists"))
                    {
                        Clear();
                        LoadGridBody();
                        ShowGridBody();
                    }
                }



            }
            catch (Exception ex)
            {
                Message.Show(ex.Message.ToString());
            }
        }
        #endregion




        #region Clear
        private void Clear()
        {
            //ddlTicketType.ClearSelection();
            //ddlTicketType.Text = string.Empty;
            txtMessage.Text = string.Empty;

        }
        #endregion

        protected void grdShowList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {

                Hashtable ht = new Hashtable();
                string CustID = Session[SessionInfo.loginid].ToString();
                ht.Add("CustomerID", CustID);

                DataTable dt = objDBUitility.GetDataByProc(ht, "sp_GetTicketByCustomerId");
                grdShowList.DataSource = dt;
                //grdShowList.DataBind();
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message);
            }
        }
    }
}