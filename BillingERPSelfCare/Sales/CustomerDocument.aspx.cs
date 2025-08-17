using BillingERPSelfCare.Utility;
using BillingERPConn;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using BillingERPSelfCare.Session;
using MkCommunication;
using System.IO;
using System.Web;
using Telerik.Web.UI;
using System.Net;
using System.Web.Configuration;

namespace BillingERPSelfCare.Sales
{
    public partial class CustomerDocument : BasePageClass
    {
        DBUtility objDBUitility = new DBUtility();
        //MkConnection objMKConnection = new MkConnection();

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            Literal pageLtl = ((Literal)PageUtility.FindControlIterative(this.Master, "ltlPageName"));
            if (!IsPostBack)
            {
                LoadOccupation();
                GetCustomerInfo();
            }
            if (pageLtl != null)
            {
                pageLtl.Text = "Customer Document Upload";
                
            }
        }
        #endregion

        string CustomerDocstatus = "0";

        public static string ImagePath = "", docVirtualPath = "";

        private void LoadOccupation()
        {
            DataTable dataTable = objDBUitility.GetDataBySQLString("select OccupationId, OccupationName from OccupationMaster where IsActive = 1");
            cmbOccupation.DataValueField = "OccupationId";
            cmbOccupation.DataTextField = "OccupationName";

            cmbOccupation.DataSource = dataTable;
            cmbOccupation.DataBind();

            cmbOccupation.Items.Insert(0, new ListItem("Please select", ""));
            cmbOccupation.Items[0].Selected = true;

        }


        #region GetCustomerInfo
        public void GetCustomerInfo()
        {
            try
            {
                Hashtable ht = new Hashtable();
                string CustID = Session[SessionInfo.loginid].ToString();
                ht.Add("CustomerID", CustID);
                DataTable dt = objDBUitility.GetDataByProc(ht, "sp_getAllDetailForCustomer");

                foreach (DataRow dataRow in dt.Rows)
                {
                    txtCustomerType.Text = dataRow["CustomerTypeName"].ToString();
                    txtCustomerName.Text = dataRow["CustomerName"].ToString();
                    txtContactName.Text = dataRow["Attention"].ToString();
                    txtAddress.Text = dataRow["Address"].ToString();
                    txtMobile.Text = dataRow["Mobile"].ToString();
                    txtEmail.Text = dataRow["Email"].ToString();
                    CustomerDocstatus = dataRow["CustomerDocStatus"].ToString();
                    cmbOccupation.Text = dataRow["OccupationName"].ToString();
                    cmbOccupation.SelectedValue = dataRow["OccupationId"].ToString();
                    txtNID.Text = dataRow["NID"].ToString();
                }
                if (CustomerDocstatus == "0" || CustomerDocstatus == "15")
                {
                    btnSubmitDoc.Visible = true;
                }
                else
                {
                    btnSubmitDoc.Visible = false;
                }

                Hashtable HT = new Hashtable();
                HT.Add("CustomerID", Session[SessionInfo.loginid].ToString());

                DataTable Data = objDBUitility.GetDataByProc(HT, "sp_GetCustomerDocumentSelfUp");
                grdShowList.DataSource = Data;
                grdShowList.DataBind();
                CustomerListBody.Visible = true;

            }
            catch (Exception ex)
            {
                Message.Show(ex.Message.ToString());
            }
        }

        #endregion

        public void GetPath()
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ID", 2);

                DataTable dataTable = objDBUitility.GetDataByProc(ht, "GetUserManualFileName");

                ImagePath = dataTable.Rows[0]["CustomerDocPhysicalPath"].ToString();
                docVirtualPath = dataTable.Rows[0]["CustomerDocVirtualPath"].ToString();
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message.ToString());
            }
        }

        #region Submit

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                string CustomerPhotoPath = string.Empty;
                string CustomerNIDPath = string.Empty;
                string OfficeIDPath = string.Empty;
                //Start For File Upload  

                string folderPath = WebConfigurationManager.AppSettings["baseUrlmyswift"];
                //Check whether Directory (Folder) exists.
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists Create it.
                    Directory.CreateDirectory(folderPath);
                }
                string TempFolder = Path.Combine(folderPath, "Temp");
                if (!Directory.Exists(TempFolder))
                {
                    //If Directory (Folder) does not exists Create it.
                    Directory.CreateDirectory(TempFolder);
                }

                if (txtNID.Text.Trim() == "")
                {
                    Message.Show("Enter your NID Number");
                    return;
                }

                if (Conversion.TryCastInteger(cmbOccupation.SelectedValue) == 0)
                {
                    Message.Show("Enter your Occupation");
                    return;
                }

                //Start Driver Image Save

                //UploadedFile photoFile = PictureUpload.UploadedFiles != null ? PictureUpload.UploadedFiles[0] : null;
                HttpPostedFile photoFile = PictureUpload.PostedFile;

                string Picturefilename = Path.GetFileName(PictureUpload.FileName);
                string PicturefileExtension = Path.GetExtension(Picturefilename);
                long PicturefileSize = photoFile.ContentLength;

                HttpPostedFile postedFile = NIDUpload.PostedFile;

                string NIDfilename = Path.GetFileName(NIDUpload.FileName);
                string NIDfileExtension = Path.GetExtension(NIDfilename);
                long NIDfileSize = postedFile.ContentLength;

                HttpPostedFile OfficeIDFile = OfficeIDUpload.PostedFile;

                string OfficeIDfilename = Path.GetFileName(OfficeIDUpload.FileName);
                string OfficeIDfileExtension = Path.GetExtension(OfficeIDfilename);
                long OfficeIDfileSize = OfficeIDFile.ContentLength;

                if (PicturefileSize > 4000000)
                {
                    Message.Show("Photo size must be less than 1 MB");
                    return;

                }

                if (NIDfileSize > 4000000)
                {
                    Message.Show("NID size must be less than 1 MB");
                    return;
                }

                if (OfficeIDfileSize > 4000000)
                {
                    Message.Show("Office ID file size must be less than 1 MB");
                    return;

                }

                if (Picturefilename == "")
                {
                    Message.Show("Upload your Photo");
                    return;
                }
                if (NIDfilename == "")
                {
                    Message.Show("Upload your NID");
                    return;
                }

                bool PictureStatus = CheckImageExtenstion(PicturefileExtension);
                bool NIDStatus = CheckImageExtenstion(NIDfileExtension);

                if (!PictureStatus)
                {
                    Message.Show("Photo format is not correct");
                    return;
                }
                else if (!NIDStatus)
                {
                    Message.Show("NID format is not correct");
                    return;
                }
                else
                {
                    CustomerPhotoPath = SaveFilePhoto(PicturefileExtension, TempFolder, "photo");
                    CustomerNIDPath = SaveFileNID(NIDfileExtension, TempFolder, "NID");

                }
                
                
                //if (NIDStatus)
                //{
                //    //CustomerNIDPath = SaveFile(nidFile, folderPath, "NID");
                //    CustomerNIDPath = SaveFileNID(NIDfileExtension, folderPath, "NID");
                //}
                //else
                //{
                //    Message.Show("File format is not correct");
                //    return;
                //}
                
                if (OfficeIDfilename != "")
                {
                    bool OfficeIDStatus = CheckImageExtenstion(OfficeIDfileExtension);
                    if (OfficeIDStatus)
                    {
                        OfficeIDPath = SaveFileOfficeID(OfficeIDfileExtension, TempFolder, "OfficeID");
                    }
                    else
                    {
                        Message.Show("File format is not correct");
                        return;
                    }
                }
                //else
                //{
                //    Message.Show("Upload your Photo");
                //    return;
                //}




                Hashtable hashTable1 = new Hashtable();
                hashTable1.Add("CustomerID", Session[SessionInfo.loginid].ToString());
                hashTable1.Add("NID", txtNID.Text.Trim());
                hashTable1.Add("PhotoPath", CustomerPhotoPath);
                hashTable1.Add("NIDPath", CustomerNIDPath);
                hashTable1.Add("OfficeIDPath", OfficeIDPath);
                hashTable1.Add("UploadTime", DateTime.Now);
                hashTable1.Add("StatusId", 13);
                hashTable1.Add("OccupationId", Conversion.TryCastInteger(cmbOccupation.SelectedValue));
                //hashTable1.Add("OccupationId", 0);


                DataTable Dtab = objDBUitility.GetDataByProc(hashTable1, "sp_customerDocumentSelfUp");
                string feedback = Dtab.Rows[0]["Feedback"].ToString();

                if (feedback == "Document uploaded successfully")
                {
                    if (File.Exists(Path.Combine(folderPath, CustomerNIDPath)))
                    {
                        File.Delete(Path.Combine(folderPath, CustomerNIDPath));
                    }

                    if (File.Exists(Path.Combine(folderPath, CustomerPhotoPath)))
                    {
                        File.Delete(Path.Combine(folderPath, CustomerPhotoPath));
                    }

                    File.Move(Path.Combine(TempFolder, CustomerNIDPath), Path.Combine(folderPath, CustomerNIDPath));
                    File.Move(Path.Combine(TempFolder, CustomerPhotoPath), Path.Combine(folderPath, CustomerPhotoPath));
                    
                    if(OfficeIDPath != "")
                    {
                        if (File.Exists(Path.Combine(folderPath, OfficeIDPath)))
                        {
                            File.Delete(Path.Combine(folderPath, OfficeIDPath));
                        }
                        File.Move(Path.Combine(TempFolder, OfficeIDPath), Path.Combine(folderPath, OfficeIDPath));
                    }

                    Message.Show(feedback);

                    Hashtable HT = new Hashtable();
                    HT.Add("CustomerID", Session[SessionInfo.loginid].ToString());

                    DataTable Data = objDBUitility.GetDataByProc(HT, "sp_GetCustomerDocumentSelfUp");
                    grdShowList.DataSource = Data;
                    grdShowList.DataBind();
                    CustomerListBody.Visible = true;
                    btnSubmitDoc.Visible = false;

                }

                else
                {
                    Message.Show("Error");


                }



            }

            catch (Exception ex)
            {
                Message.Show(ex.Message);
            }
        }


        private bool CheckImageExtenstion(string fileExtension)
        {
            bool status = false;
            string fileName = string.Empty;
            if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".gif" || fileExtension.ToLower() == ".pdf"
                    || fileExtension.ToLower() == ".png" || fileExtension.ToLower() == ".bmp" || fileExtension.ToLower() == ".jpeg"
                    )
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }


        private string SaveFilePhoto(string fileExtension, string folderPath, string prefix)
        {
            try
            {
                string fileName = string.Format("{0}_{1}{2}", prefix, Session[SessionInfo.loginid].ToString(), fileExtension);

                PictureUpload.SaveAs(Path.Combine(folderPath, fileName));
                return fileName;
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message.ToString());
                return "";
            }
        }

        private string SaveFileNID(string fileExtension, string folderPath, string prefix)
        {
            try
            {
                string fileName = string.Format("{0}_{1}{2}", prefix, Session[SessionInfo.loginid].ToString(), fileExtension);

                NIDUpload.SaveAs(Path.Combine(folderPath, fileName));
                return fileName;
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message.ToString());
                return "";
            }
        }

        private string SaveFileOfficeID(string fileExtension, string folderPath, string prefix)
        {
            try
            {
                string fileName = string.Format("{0}_{1}{2}", prefix, Session[SessionInfo.loginid].ToString(), fileExtension);

                OfficeIDUpload.SaveAs(Path.Combine(folderPath, fileName));
                return fileName;
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message.ToString());
                return "";
            }
        }



        #endregion

        protected void btndownload_Click(object sender, EventArgs e)
        {
            try
            {

                RadButton Button = (RadButton)sender;
                GridDataItem item = (GridDataItem)Button.NamingContainer;
                string DocPath = item["DocPath"].Text;

                WebClient req = new WebClient();
                HttpResponse response = HttpContext.Current.Response;
                string filePath = WebConfigurationManager.AppSettings["baseUrlmyswift"];
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.AddHeader("Content-Disposition", "attachment;filename=" + DocPath + "");
                byte[] data = req.DownloadData(filePath + DocPath);
                response.BinaryWrite(data);
                response.End();

            }
            catch (Exception ex)
            {
                Message.Show(ex.Message.ToString());
            }
        }

        //protected void PictureUpload_ValidatingFile(object sender, Telerik.Web.UI.Upload.ValidateFileEventArgs e)
        //{
        //    UploadedFile photoFile = PictureUpload.UploadedFiles != null ? PictureUpload.UploadedFiles[0] : null;
        //    var contentSize = photoFile.ContentLength;
        //    if(contentSize> 1000000)
        //    {
        //        Message.Show("Photo File must be less than 1 MB");
        //        return;

        //    }
        //} 
        //protected void NIDUpload_ValidatingFile(object sender, Telerik.Web.UI.Upload.ValidateFileEventArgs e)
        //{
        //    UploadedFile NidFile = NIDUpload.UploadedFiles != null ? PictureUpload.UploadedFiles[0] : null;
        //    var contentSize = NidFile.ContentLength;
        //    if(contentSize> 1000000)
        //    {
        //        Message.Show("NID File must be less than 1 MB");
        //        return;
        //    }
        //}




    }
}