
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using BillingERPConn;

namespace BillingERPSelfCare.Utility
{

  
    public static class UniqueCode
    {
        static readonly DBUtility idb = new DBUtility();

        public static string AutoCodeGenerate()
        {
            string sqlCode;

            string pvCode = null;
            try
            {
                string pvSerial = idb.AggRetrive("SELECT isnull(max(serial),0)+1 FROM CollectionMaster").ToString();
                
                if (pvSerial.Length == 1)
                {
                    pvCode = "CI-00000" + pvSerial;
                }

                if (pvSerial.Length == 2)
                {
                    pvCode = "CI-0000" + pvSerial;
                }
                
                if (pvSerial.Length == 3)
                {
                    pvCode = "CI-000" + pvSerial;
                }
                
                if (pvSerial.Length == 4)
                {
                    pvCode = "CI-00" + pvSerial;
                }

                if (pvSerial.Length == 5)
                {
                    pvCode = "CI-0" + pvSerial;
                }

                if (pvSerial.Length == 6)
                {
                    pvCode = "CI-" + pvSerial;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return pvCode;
        }

        public static string ProductCodeGenerate(string p, string companyCode)
        {


            string pvCode = null;
            try
            {
                //string companyCode = txtCompany.Text;
                string year = Convert.ToString(DateTime.Now.Year);
                string pvSerial = null;

                //if (p == "ST")
                //{
                //    pvSerial = idb.AggRetrive("SELECT isnull( max(SerialNumber), 0)+1 FROM PBML_SDM_StockMaster where CompanyID='" + companyCode + "' AND Year='" + year + "' ").ToString();
                //}

                if (p == "DC")
                {
                    pvSerial = idb
                        .AggRetrive("SELECT isnull( max(SerialNo), 0)+1 FROM Sales_FDCMaster Where CompanyId = '" +
                                    companyCode + "' And [Year]= '" + year + "' ").ToString();

                }
                else if (p == "DN")
                {
                    pvSerial = idb
                        .AggRetrive("SELECT isnull( max(SerialNo), 0)+1 FROM DebitNoteMaster Where SellerId = '" +
                                    companyCode + "' And [Year]= '" + year + "' ").ToString();

                }
                else if (p == "PI")
                {
                    pvSerial = idb
                        .AggRetrive("SELECT isnull( max(SerialNo), 0)+1 FROM Sales_PIMaster Where companyId = '" +
                                    companyCode + "' And [Year]= '" + year + "' ").ToString();

                }
                else if (p == "LC")
                {
                    pvSerial = idb
                        .AggRetrive("SELECT isnull( max(SerialNo), 0)+1 FROM Sales_LCMaster Where companyId = '" +
                                    companyCode + "' And [Year]= '" + year + "' ").ToString();

                }
                else if (p == "LN")
                {
                    pvSerial = idb.AggRetrive("SELECT isnull( max(SerialNo), 0)+1 FROM LandMaster Where companyId = '" +
                                              companyCode + "' And [Year]= '" + year + "' ").ToString();

                }
                else if (p == "DO")
                {
                    pvSerial = idb
                        .AggRetrive("SELECT isnull( max(SerialNo), 0)+1 FROM Sales_HDOMaster Where companyId = '" +
                                    companyCode + "' And [Year]= '" + year + "' ").ToString();

                }
                else if (p == "ADO")
                {
                    pvSerial = idb
                        .AggRetrive("SELECT isnull( max(SerialNo), 0)+1 FROM Sales_ADOMaster Where companyId = '" +
                                    companyCode + "' And [Year]= '" + year + "' ").ToString();

                }
                else if (p == "PC")
                {
                    pvSerial = idb
                        .AggRetrive("SELECT isnull( max(SerialNo), 0)+1 FROM PRD_ProductionMaster Where companyId = '" +
                                    companyCode + "' And [year]= '" + year + "' ").ToString();

                }
                else if (p == "CDO")
                {
                    pvSerial = idb
                        .AggRetrive("SELECT isnull( max(SerialNo), 0)+1 FROM Sales_CDOMaster Where CompanyId = '" +
                                    companyCode + "' And [Year]= '" + year + "' ").ToString();

                }

                else if (p == "RN")
                {
                    pvSerial = idb
                        .AggRetrive("SELECT isnull( max(SerialNo), 0)+1 FROM INV_FGStockMaster Where CompanyId = '" +
                                    companyCode + "' And [Year]= '" + year + "' ").ToString();

                }
                else if (p == "CI")
                {
                    pvSerial = idb
                        .AggRetrive(
                            "SELECT isnull( max(SerialNo), 0)+1 FROM Commercial_InvoiceMaster Where CompanyId = '" +
                            companyCode + "' And [Year]= '" + year + "' ").ToString();

                }
                else if (p == "TR")
                {
                    pvSerial = idb
                        .AggRetrive(
                            "SELECT isnull( max(SerialNo), 0)+1 FROM Commercial_TruckRecieptMaster Where CompanyId = '" +
                            companyCode + "' And [Year]= '" + year + "' ").ToString();

                }


                //if (p == "GP")
                //{
                //    pvSerial = idb.AggRetrive("SELECT isnull( max(DCSerialNumber), 0)+1 FROM PBML_SDM_DCMaster Where CompanyID = '" + companyCode + "' And PYear= '" + year + "' ").ToString();

                //}


                if (pvSerial.Length == 1)
                {
                    pvCode = companyCode.Trim() + year + "-" + p + "-" + "00000" + pvSerial;
                }

                if (pvSerial.Length == 2)
                {
                    pvCode = companyCode.Trim() + year + "-" + p + "-" + "0000" + pvSerial;
                }


                if (pvSerial.Length == 3)
                {
                    pvCode = companyCode.Trim() + year + "-" + p + "-" + "000" + pvSerial;
                }


                if (pvSerial.Length == 4)
                {
                    pvCode = companyCode.Trim() + year + "-" + p + "-" + "00" + pvSerial;
                }

                if (pvSerial.Length == 5)
                {
                    pvCode = companyCode.Trim() + year + "-" + p + "-" + "0" + pvSerial;
                }

                if (pvSerial.Length == 6)
                {
                    pvCode = companyCode.Trim() + year + "-" + p + "-" + pvSerial;
                }


            }
            catch (Exception ex)
            {
                //Alert.Show(ex.Message);
            }

            return pvCode;
        }

        public static string CustIDGenerate(string p)
        {

            DBUtility idb = new DBUtility();

            string pvCode = null;
            try
            {
                //string companyCode = txtCompany.Text;
                //string year = Convert.ToString(DateTime.Now.Year);
                string pvSerial = null;

                //if (p == "ST")
                //{
                //    pvSerial = idb.AggRetrive("SELECT isnull( max(SerialNumber), 0)+1 FROM PBML_SDM_StockMaster where CompanyID='" + companyCode + "' AND Year='" + year + "' ").ToString();
                //}

                if (p == "SWB")
                {
                    pvSerial = idb.AggRetrive("SELECT isnull( max(CustomerSerial), 0)+1 FROM CustomerMaster")
                        .ToString();

                }

                else if (p == "C")
                {
                    pvSerial = idb.AggRetrive("SELECT isnull( max(CustomerSerial), 0)+1 FROM CustomerMaster")
                        .ToString();

                }


                //if (p == "GP")
                //{
                //    pvSerial = idb.AggRetrive("SELECT isnull( max(DCSerialNumber), 0)+1 FROM PBML_SDM_DCMaster Where CompanyID = '" + companyCode + "' And PYear= '" + year + "' ").ToString();

                //}


                if (pvSerial.Length == 1)
                {
                    pvCode = p + "-" + "0000" + pvSerial;
                }

                if (pvSerial.Length == 2)
                {
                    pvCode = p + "-" + "000" + pvSerial;
                }


                if (pvSerial.Length == 3)
                {
                    pvCode = p + "-" + "00" + pvSerial;
                }


                if (pvSerial.Length == 4)
                {
                    pvCode = p + "-" + "0" + pvSerial;
                }

                if (pvSerial.Length == 5)
                {
                    pvCode = p + "-" + pvSerial;
                }

                //if (pvSerial.Length == 6)
                //{
                //    pvCode =  p + "-" + pvSerial;
                //}


            }
            catch (Exception ex)
            {
                //Alert.Show(ex.Message);
            }

            return pvCode;
        }



        public static string GetDateFormat_dd_mm_yyyy(string date)
        {

            string formatedDate = "";
            try
            {
                DateTime newDate = DateTime.Parse(date);

                formatedDate = newDate.ToString("dd/MM/yyyy");
            }
            catch
            {
                formatedDate = "";
            }

            return formatedDate;
        }



        public static string IsDuplicate(string FieldName, string TableNmae, string FieldValue)
        {
            string IsDuplicate = "false";
            try
            {
                string FormatedFieldValue = FieldValue.Trim().Replace(" ", string.Empty);



                var idb = new DBUtility();
                var Dtab = new DataTable();
                Dtab = idb.GetDataBySQLString("SELECT REPLACE(" + FieldName + ", ' ', '') From " + TableNmae +
                                              " WHERE REPLACE(" + FieldName + ", ' ', '')='" + FormatedFieldValue +
                                              "'");

                IsDuplicate = Dtab.Rows.Count > 0 ? "true" : "false";
            }
            catch
            {
                IsDuplicate = "false";

            }

            return IsDuplicate;


        }

        public static DateTime GetDateFormat_MM_dd_yyy(string dmyFormat)
        {
            DateTime FormatedDate = Convert.ToDateTime(DateTime
                .ParseExact(dmyFormat, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                .ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
            return FormatedDate;

        }

        //public static void CustomerDisableEnable(BOClientInfo objClient)
        //{
        //    string message = "";
        //    BOMikrotikCommnError objError = new BOMikrotikCommnError();

        //    if (objClient.DueAmount1 > objClient.CreditLimit)
        //    {
        //        using (ITikConnection connection = ConnectionFactory.CreateConnection(TikConnectionType.Api))
        //        {

        //            connection.Open(objClient.Hostname, objClient.Username, objClient.Password);

        //            string ipAddress = objClient.IPAddress;

        //            var loadCmd = connection.CreateCommandAndParameters("/ip/arp/print", "address", ipAddress);
        //            var response = loadCmd.ExecuteList();

        //            int dlngth = response.Count();



        //            var itemId = "";

        //            for (int i = 0; i < dlngth; i++)
        //            {
        //                try
        //                {
        //                    var itemId1 = response.ElementAt(i);




        //                    itemId = itemId1.GetId();
        //                    var updateCmd = connection.CreateCommandAndParameters("/ip/arp/set", "disabled", "yes",
        //                   TikSpecialProperties.Id, itemId);
        //                    updateCmd.ExecuteNonQuery();

        //                    objClient.IsActive = false;
        //                    DaPop.UpdateCustomerStatus(objClient.CustomerID1, false);
        //                    DaPop.InsertMikrotikLog(objClient);
        //                }
        //                catch (Exception)
        //                {

        //                    continue;
        //                }
        //            }


        //            if (dlngth == 0)
        //            {
        //                message = "Customer should InActive. IP Mismatch, so no operation";
        //                objError.ErrorDescription = message;
        //                objError.Screen = HttpContext.Current.Request.Url.AbsoluteUri;
        //                objError.PinNumber = objClient.pin_number;
        //                objError.CustomerId = objClient.CustomerID1;
        //                objError.CustomerIPAddress = objClient.IPAddress;
        //                objError.Type = "Collection From SSL ";
        //                BLCollection.InsertMikrotikCommunicationLog(objError);
        //            }



        //        }

        //    }
        //    else
        //    {
        //        using (ITikConnection connection = ConnectionFactory.CreateConnection(TikConnectionType.Api))
        //        {

        //            connection.Open(objClient.Hostname, objClient.Username, objClient.Password);

        //            string iPAddress = objClient.IPAddress;

        //            var loadCmd = connection.CreateCommandAndParameters("/ip/arp/print", "address", iPAddress);
        //            var response = loadCmd.ExecuteList();
        //            int dlngth = response.Count();



        //            var itemId = "";

        //            for (int i = 0; i < dlngth; i++)
        //            {
        //                try
        //                {
        //                    var itemId1 = response.ElementAt(i);


        //                    itemId = itemId1.GetId();
        //                    var updateCmd = connection.CreateCommandAndParameters("/ip/arp/set", "disabled", "no",
        //                   TikSpecialProperties.Id, itemId);
        //                    updateCmd.ExecuteNonQuery();

        //                    objClient.IsActive = true;
        //                    DaPop.UpdateCustomerStatus(objClient.CustomerID1, true);
        //                    DaPop.ReleaseBillingLock(objClient.CustomerID1, objClient.pin_number);
        //                    DaPop.InsertMikrotikLog(objClient);
        //                }
        //                catch (Exception)
        //                {

        //                    continue;
        //                }


        //            }


        //            if (dlngth == 0)
        //            {
        //                message = "Customer should Active. IP Mismatch,so no operation";
        //                objError.ErrorDescription = message;
        //                objError.Screen = HttpContext.Current.Request.Url.AbsoluteUri;
        //                objError.PinNumber = objClient.pin_number;
        //                objError.CustomerId = objClient.CustomerID1;
        //                objError.CustomerIPAddress = objClient.IPAddress;
        //                objError.Type = "Collection From SSL";
        //                BLCollection.InsertMikrotikCommunicationLog(objError);
        //            }



        //        }


        //    }



        //}

        /// <summary>
        /// Customer Enable if no dues before process date
        /// </summary>
        /// <param name="objClient"></param>
        /// <returns></returns>

        //public static string CustomerEnableIfNoDues(BOClientInfo objClient)
        //{
        //    string message = "";
        //    BOMikrotikCommnError objError = new BOMikrotikCommnError();

        //    try
        //    {

        //        if (objClient.DueAmount1 <= objClient.CreditLimit)
        //        {
        //            using (ITikConnection connection = ConnectionFactory.CreateConnection(TikConnectionType.Api))
        //            {

        //                connection.Open(objClient.Hostname, objClient.Username, objClient.Password);

        //                string iPAddress = objClient.IPAddress;

        //                var loadCmd = connection.CreateCommandAndParameters("/ip/arp/print", "address", iPAddress);
        //                var response = loadCmd.ExecuteList();
        //                int dlngth = response.Count();

        //                var itemId = "";

        //                for (int i = 0; i < dlngth; i++)
        //                {
        //                    try
        //                    {
        //                        var itemId1 = response.ElementAt(i);

        //                        itemId = itemId1.GetId();
        //                        var updateCmd = connection.CreateCommandAndParameters("/ip/arp/set", "disabled", "no",
        //                       TikSpecialProperties.Id, itemId);
        //                        updateCmd.ExecuteNonQuery();

        //                        objClient.IsActive = true;
        //                        DaPop.UpdateCustomerStatus(objClient.CustomerID1, true);
        //                        DaPop.ReleaseBillingLock(objClient.CustomerID1, objClient.pin_number);
        //                        DaPop.InsertMikrotikLog(objClient);


        //                    }
        //                    catch (Exception)
        //                    {

        //                        continue;
        //                    }


        //                }



        //                if (dlngth == 0)
        //                {
        //                    message = "<br>Customer should Active. IP Mismatch " + objClient.NetworkID1 + ",so no operation";
        //                    objError.ErrorDescription = message;
        //                    objError.Screen = HttpContext.Current.Request.Url.AbsoluteUri;
        //                    objError.PinNumber = objClient.pin_number;
        //                    objError.CustomerId = objClient.CustomerID1;
        //                    objError.CustomerIPAddress = objClient.IPAddress;
        //                    objError.POPIP = objClient.Hostname;
        //                    objError.PopId = objClient.PopId;

        //                    objError.Type = "Collection  CustomerEnableIfNoDues dlngth == 0; objClient.DueAmount1 <= objClient.CreditLimit " + objClient.Screen;
        //                    BLCollection.InsertMikrotikCommunicationLog(objError);
        //                }



        //            }


        //        }



        //    }
        //    catch (Exception ex)
        //    {
        //        message = "<br>" + objClient.NetworkID1 + " POP: " + objClient.Hostname + " Client IP:  " + objClient.IPAddress + " ERORR: " + ex.Message.ToString();
        //        objError.Screen = HttpContext.Current.Request.Url.AbsoluteUri;
        //        objError.ErrorDescription = message;
        //        objError.PinNumber = objClient.pin_number;
        //        objError.CustomerId = objClient.CustomerID1;
        //        objError.CustomerIPAddress = objClient.IPAddress;
        //        objError.Type = "Collection Save Method:CustomerEnableIfNoDues()  Catch Block " + objClient.Screen;
        //        objError.POPIP = objClient.Hostname;
        //        objError.PopId = objClient.PopId;
        //        objError.CustomerIPAddress = objClient.IPAddress;
        //        objError.PinNumber = objClient.pin_number;
        //        BLCollection.InsertMikrotikCommunicationLog(objError);

        //    }

        //    return message;

        //}



        //public static string CustomerDisableEnableInvestigationError(BOClientInfo objClient)
        //{
        //    string message = "";
        //    BOMikrotikCommnError objError = new BOMikrotikCommnError();
        //    try
        //    {
        //        using (ITikConnection connection = ConnectionFactory.CreateConnection(TikConnectionType.Api))
        //        {

        //            connection.Open(objClient.Hostname, objClient.Username, objClient.Password);

        //            string ipAddress = objClient.IPAddress;

        //            var loadCmd = connection.CreateCommandAndParameters("/ip/arp/print", "address", ipAddress);
        //            var response = loadCmd.ExecuteList();

        //            int dlngth = response.Count();

        //            //for (int i = 0; i < 1 && dlngth != 0; i++)
        //            //{

        //            //    var itemId1 = response.ElementAt(i);

        //            //    var itemId = itemId1.GetId();


        //            //    var updateCmd = connection.CreateCommandAndParameters("/ip/arp/find", "address", ipAddress);
        //            //    updateCmd.ExecuteNonQuery();


        //            //}

        //            if (dlngth == 0)
        //            {
        //                message = "Customer IP Mismatch, Please Check";
        //                objError.ErrorDescription = message;
        //                objError.Screen = HttpContext.Current.Request.Url.AbsoluteUri;
        //                objError.PinNumber = objClient.pin_number;
        //                objError.CustomerId = objClient.CustomerID1;
        //                objError.CustomerIPAddress = objClient.IPAddress;
        //                objError.Type = "Collection Upload investigation Method:CustomerDisableEnableInvestigationError()  dlngth=0";
        //                objError.POPIP = objClient.Hostname;
        //                objError.PopId = objClient.PopId;
        //                objError.CustomerIPAddress = objClient.IPAddress;
        //                objError.PinNumber = objClient.pin_number;
        //                BLCollection.InsertMikrotikCommunicationLog(objError);
        //            }




        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        message = objClient.NetworkID1 + "POP: " + objClient.Hostname + " Client IP:  " + objClient.IPAddress + " ERORR: " + ex.Message.ToString();
        //        objError.Screen = HttpContext.Current.Request.Url.AbsoluteUri;
        //        objError.ErrorDescription = message;
        //        objError.PinNumber = objClient.pin_number;
        //        objError.CustomerId = objClient.CustomerID1;
        //        objError.CustomerIPAddress = objClient.IPAddress;
        //        objError.Type = "Collection Upload investigation Method:CustomerDisableEnableInvestigationError()  Catch Block";
        //        objError.POPIP = objClient.Hostname;
        //        objError.PopId = objClient.PopId;
        //        objError.CustomerIPAddress = objClient.IPAddress;
        //        objError.PinNumber = objClient.pin_number;
        //        BLCollection.InsertMikrotikCommunicationLog(objError);
        //    }




        //    return message;
        //}




        //public static void CustomerDisableEnableByStatus(BOClientInfo objClient)
        //{

        //    if (!objClient.IsActive)
        //    {
        //        using (ITikConnection connection = ConnectionFactory.CreateConnection(TikConnectionType.Api))
        //        {

        //            connection.Open(objClient.Hostname, objClient.Username, objClient.Password);

        //            string ipAddress = objClient.IPAddress;

        //            var loadCmd = connection.CreateCommandAndParameters("/ip/arp/print", "address", ipAddress);
        //            var response = loadCmd.ExecuteList();
        //            int dlngth = response.Count();

        //            for (int i = 0; i < 1 && dlngth != 0; i++)
        //            {

        //                var itemId1 = response.ElementAt(i);

        //                var itemId = itemId1.GetId();


        //                var updateCmd = connection.CreateCommandAndParameters("/ip/arp/set", "disabled", "yes", TikSpecialProperties.Id, itemId);
        //                updateCmd.ExecuteNonQuery();

        //            }


        //        }
        //        DaPop.UpdateCustomerStatus(objClient.CustomerID1, false);

        //    }
        //    else
        //    {
        //        using (ITikConnection connection = ConnectionFactory.CreateConnection(TikConnectionType.Api))
        //        {

        //            connection.Open(objClient.Hostname, objClient.Username, objClient.Password);

        //            string ipAddress = objClient.IPAddress;

        //            var loadCmd = connection.CreateCommandAndParameters("/ip/arp/print", "address", ipAddress);
        //            var response = loadCmd.ExecuteList();
        //            int dlngth = response.Count();

        //            for (int i = 0; i < 1 && dlngth != 0; i++)
        //            {

        //                var itemId1 = response.ElementAt(i);

        //                var itemId = itemId1.GetId();


        //                var updateCmd = connection.CreateCommandAndParameters("/ip/arp/set", "disabled", "no", TikSpecialProperties.Id, itemId);
        //                updateCmd.ExecuteNonQuery();

        //            }


        //        }
        //        DaPop.UpdateCustomerStatus(objClient.CustomerID1, true);
        //        DaPop.ReleaseBillingLock(objClient.CustomerID1, objClient.pin_number);


        //    }

        //    DaPop.InsertMikrotikLog(objClient);
        //}

        public static string GetDateFormat_dd_mm_yyyy_hh_mm_ss(string date)
        {

            var formatedDate = "";
            try
            {
                DateTime newDate = DateTime.Parse(date);

                formatedDate = newDate.ToString("dd/MM/yyyy HH:mm:ss");
            }
            catch
            {
                formatedDate = "";
            }
            return formatedDate;
        }

        public static DataTable ConvertToDataTable<T>(IList<T> list)
        {
            PropertyDescriptorCollection propertyDescriptorCollection = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
                Type propType = propertyDescriptor.PropertyType;
                if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    table.Columns.Add(propertyDescriptor.Name, Nullable.GetUnderlyingType(propType));
                }
                else
                {
                    table.Columns.Add(propertyDescriptor.Name, propType);
                }
            }
            object[] values = new object[propertyDescriptorCollection.Count];
            foreach (T listItem in list)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = propertyDescriptorCollection[i].GetValue(listItem);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public static DataTable AsDataTable<T>(this IEnumerable<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        //public static bool CustomerDisableBySystemDept(BOClientInfo objClient)
        //{
        //    try
        //    {



        //        using (ITikConnection connection = ConnectionFactory.CreateConnection(TikConnectionType.Api))
        //        {

        //            connection.Open(objClient.Hostname, objClient.Username, objClient.Password);

        //            string ipAddress = objClient.IPAddress;

        //            var loadCmd = connection.CreateCommandAndParameters("/ip/arp/print", "address", ipAddress);
        //            var response = loadCmd.ExecuteList();

        //            int dlngth = response.Count();

        //            for (int i = 0; i < 1; i++)
        //            {

        //                var itemId1 = response.ElementAt(i);

        //                var itemId = itemId1.GetId();


        //                var updateCmd = connection.CreateCommandAndParameters("/ip/arp/set", "disabled", "yes", TikSpecialProperties.Id, itemId);
        //                updateCmd.ExecuteNonQuery();

        //            }

        //        }


        //        DaPop.InsertMikrotikLogBySystemDept(objClient);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        //public static IRestResponse SentSMS(string customerId, string billingTel, decimal? Amount, SmsText smsText, string defaultText)
        //{
        //    string text = "";
        //    //if (billingTel.Length != 13)
        //    //{
        //    //    return null;
        //    //}


        //    switch (smsText)
        //    {
        //        case SmsText.NewCustomer:
        //            text = "We have received your payment TK. " + Amount + ".You can access our online billing on http://bit.do/ecHDr" +
        //                   "  .Username is " + customerId + " and Password is your email address.";
        //            break;
        //        case SmsText.RegularCustomer:
        //            text = "We have received your payment TK. " + Amount + ".You can access our online billing on http://bit.do/ecHDr" +
        //                 "  . Username is " + customerId + " and Password is your email address.";
        //            break;
        //        case SmsText.BillingCustomerAfterblock:
        //            text = "Your internet has temporarily blocked on billing dues.Please pay your dues to unlock your internet.For billing details visit http://bit.do/ecRXj";
        //            break;
        //        case SmsText.Billingcustomerafterunblock:
        //            text = "We have received your payment TK. " + Amount + ".We have already unblocked your internet.Visit http://bit.do/ecHDr  for billing details.";
        //            break;
        //        case SmsText.DefaultText:
        //            text = defaultText;
        //            break;
        //    }

        //    var listMsg = new List<Message>();
        //    var msg = new Message()
        //    {
        //        from = "Amber IT",
        //        to = billingTel,
        //        // to = "8801787653277,8801787653283",
        //        text = text,
        //        categoryName = "Promotion"
        //    };
        //    listMsg.Add(msg);
        //    var smsHttpRequest = new SmsHttpRequest()
        //    {
        //        messages = listMsg,
        //        campaignName = "Promotion",
        //        routeId = "1",
        //        scheduledDeliveryDateTime = DateTime.Now.AddMinutes(-1).ToString("yyyy-MM-dd HH:mm:ss"),
        //        validityPeriodInHour = 0,
        //        scheduledDeliveryDateTimeOffset = "+0600",
        //        smsTypeId = 1,
        //        responseType = 1

        //    };

        //    var url = "http://services.smsnet24.com/services/sms/sendbulksms";

        //    var username = "swiftsms@amberit.com.bd";
        //    var password = "AmIT321&$18#";
        //    var encoded = Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));

        //    var client = new RestClient(url);
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("Content-type", "application/json");
        //    request.AddHeader("Authorization", "Basic " + encoded);
        //    request.AddJsonBody(smsHttpRequest);

        //    IRestResponse response = client.Execute<SmsHttpResponse>(request);
        //    // var result = JsonConvert.DeserializeObject<SmsHttpResponse>(response.Content);
        //    return response;
        //}


        //public static bool CustomerMikrotikStatus(BOClientInfo objCustomer)
        //{
        //    string status = "Not Found";
        //    using (ITikConnection connection = ConnectionFactory.CreateConnection(TikConnectionType.Api))
        //    {

        //        connection.Open(objCustomer.Hostname, objCustomer.Username, objCustomer.Password);

        //        string ipAddress = objCustomer.IPAddress;

        //        var loadCmd = connection.CreateCommandAndParameters("/ip/arp/print", "address", ipAddress);
        //        var response = loadCmd.ExecuteList();
        //        int dlngth = response.Count();

        //        for (int i = 0; i < 1 && dlngth != 0; i++)
        //        {

        //            var itemId1 = response.ElementAt(i);

        //            //string status = itemId1.Words["disabled"];
        //            status = itemId1.GetResponseField("disabled");

        //        }


        //    }

        //    //return status;
        //    return status == "false";


        //}

        public static T ToObject<T>(this IDictionary<string, object> source)
            where T : class, new()
        {
            var someObject = new T();
            var someObjectType = someObject.GetType();

            foreach (var item in source)
            {
                var propertyInfo = someObjectType.GetProperty(item.Key);

                var value = Parse(propertyInfo.PropertyType, item.Value.ToString());
                propertyInfo.SetValue(someObject, value, null);
            }

            return someObject;
        }

        public static IDictionary<string, object> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );

        }

        private static T GetFromQueryString<T>() where T : new()
        {
            var obj = new T();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var valueAsString = HttpContext.Current.Request.QueryString[property.Name];
                var value = Parse(property.PropertyType, valueAsString);

                if (value == null)
                    continue;

                property.SetValue(obj, value, null);
            }
            return obj;
        }

        private static object Parse(Type dataType, string valueToConvert)
        {
            TypeConverter obj = TypeDescriptor.GetConverter(dataType);
            object value = obj.ConvertFromString(null, CultureInfo.InvariantCulture, valueToConvert);
            return value;
        }

        //public static bool GetPrevileage(AuthUser user)
        //{
        //    if (user != null)
        //    {
        //        return user.IsValid();
        //    }

        //    return false;
        //}
    }

    public enum SmsText
    {
        NewCustomer,
        RegularCustomer,
        BillingCustomerAfterblock,
        Billingcustomerafterunblock,
        DefaultText
    }

}