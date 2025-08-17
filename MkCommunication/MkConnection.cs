using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tik4net;

namespace MkCommunication
{
    public class MkConnection
    {

        /// <summary>
        /// Disable Customer IPAddress  
        /// </summary>
        /// <param name="Hostname"></param>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <param name="IPAddress"></param>
        /// <param name="customerID"></param>
        /// <returns></returns>
        /// 

        public MkConnStatus DisableMikrotik(string Hostname, string Username, string Password, string mkVersion, int ProtocolId, string customerID, int InsType, string mkUser)
        {
            MkConnStatus objConnStatus = new MkConnStatus();

            string message = "";

            try
            {
                if (customerID.Contains('-'))
                {

                    customerID = customerID.Replace("-", "");
                }

                if (InsType == 1)
                {
                    if (Hostname != "" && Username != "" && Password != "" && ProtocolId != 0)
                    {

                        if (ProtocolId == 1)
                        {
                            objConnStatus = IPV4EnableDisable("yes", Hostname, Username, Password, mkVersion, ProtocolId, customerID);
                            return objConnStatus;
                        }
                        else if (ProtocolId == 2)
                        {
                            objConnStatus = IPV6EnableDisable("yes", Hostname, Username, Password, mkVersion, ProtocolId, customerID);
                            return objConnStatus;
                        }

                        else if (ProtocolId == 3)
                        {
                            MkConnStatus objConnStatus1 = IPV4EnableDisable("yes", Hostname, Username, Password, mkVersion, ProtocolId, customerID);

                            MkConnStatus objConnStatus2 = IPV6EnableDisable("yes", Hostname, Username, Password, mkVersion, ProtocolId, customerID);

                            if (objConnStatus1.StatusCode == "200" || objConnStatus2.StatusCode == "200")
                            {
                                objConnStatus.StatusCode = "200";
                                objConnStatus.Status = "Success";
                                objConnStatus.RetMessage = "IP Disabled Sucess";
                                objConnStatus.CodePortion = "Protocol 3 block";
                            }
                            else
                            {
                                objConnStatus.StatusCode = "220";
                                objConnStatus.Status = "Failed";
                                objConnStatus.RetMessage = objConnStatus1.RetMessage + ", " + objConnStatus2.RetMessage;
                                objConnStatus.CodePortion = "Protocol 3 block";
                            }
                        }
                    }
                }

                else if (InsType == 2) /// PPPoE
                {

                    objConnStatus = EnableDisableforPPPoE("true", Hostname, Username, Password, mkVersion, customerID, mkUser);
                    return objConnStatus;
                }

            }
            catch (Exception ex)
            {

                message = customerID + " " + ex.Message.ToString();
                objConnStatus.StatusCode = "230";
                objConnStatus.Status = "Disabled Failed";
                objConnStatus.RetMessage = message;
                objConnStatus.CodePortion = "Main Catch Block";

            }

            return objConnStatus;
        }

        public MkConnStatus EnableMikrotik(string Hostname, string Username, string Password, string mkVersion, int ProtocolId, string customerID, int InsType, string mkUser)
        {
            MkConnStatus objConnStatus = new MkConnStatus();

            string message = "";
            try
            {
                if (customerID.Contains('-'))
                {

                    customerID = customerID.Replace("-", "");
                }

                if (InsType == 1)
                {
                    if (Hostname != "" && Username != "" && Password != "" && ProtocolId != 0)
                    {

                        if (ProtocolId == 1)
                        {
                            objConnStatus = IPV4EnableDisable("no", Hostname, Username, Password, mkVersion, ProtocolId, customerID);
                            return objConnStatus;
                        }
                        else if (ProtocolId == 2)
                        {
                            objConnStatus = IPV6EnableDisable("no", Hostname, Username, Password, mkVersion, ProtocolId, customerID);
                            return objConnStatus;
                        }

                        else if (ProtocolId == 3)
                        {
                            MkConnStatus objConnStatus1 = IPV4EnableDisable("no", Hostname, Username, Password, mkVersion, ProtocolId, customerID);

                            MkConnStatus objConnStatus2 = IPV6EnableDisable("no", Hostname, Username, Password, mkVersion, ProtocolId, customerID);

                            if (objConnStatus1.StatusCode == "200" || objConnStatus2.StatusCode == "200")
                            {
                                objConnStatus.StatusCode = "200";
                                objConnStatus.Status = "Success";
                                objConnStatus.RetMessage = "IP Enabled Success";
                                objConnStatus.CodePortion = "Protocol 3 block";
                            }
                            else
                            {
                                objConnStatus.StatusCode = "220";
                                objConnStatus.Status = "Failed";
                                objConnStatus.RetMessage = "IPV4: " + objConnStatus1.RetMessage + ", IPV6: " + objConnStatus2.RetMessage;
                                objConnStatus.CodePortion = "Protocol 3 block";
                            }
                        }
                    }
                }

                else if (InsType == 2)
                {
                    objConnStatus = EnableDisableforPPPoE("false", Hostname, Username, Password, mkVersion, customerID, mkUser);
                    return objConnStatus;
                }


            }
            catch (Exception ex)
            {

                message = customerID + " " + ex.Message.ToString();
                objConnStatus.StatusCode = "230";
                objConnStatus.Status = "Enabled Failed";
                objConnStatus.RetMessage = message;
                objConnStatus.CodePortion = "Main Catch Block";

            }

            return objConnStatus;
        }

        public MkConnStatus MikrotikStatus(string Hostname, string Username, string Password, string mkVersion, int ProtocolId, string customerID, int InsType, string mkUser)
        {
            MkConnStatus objConnStatus = new MkConnStatus();

            string message = "";
            try
            {
                if (customerID.Contains('-'))
                {

                    customerID = customerID.Replace("-", "");
                }

                if (InsType == 1)
                {
                    if (Hostname != "" && Username != "" && Password != "" && ProtocolId != 0)
                    {

                        if (ProtocolId == 1)
                        {
                            objConnStatus = FindIPV4Status(Hostname, Username, Password, mkVersion, ProtocolId, customerID);

                        }
                        else if (ProtocolId == 2)
                        {
                            objConnStatus = FindIPV6Status(Hostname, Username, Password, mkVersion, ProtocolId, customerID);

                        }

                        else if (ProtocolId == 3)
                        {
                            MkConnStatus objConnStatus1 = FindIPV4Status(Hostname, Username, Password, mkVersion, ProtocolId, customerID);

                            MkConnStatus objConnStatus2 = FindIPV6Status(Hostname, Username, Password, mkVersion, ProtocolId, customerID);

                            if (objConnStatus1.StatusCode == "200" || objConnStatus2.StatusCode == "200")
                            {

                                if (objConnStatus1.MikrotikStatus == 0 && objConnStatus2.MikrotikStatus == 0)
                                {
                                    objConnStatus.DLength = 0;
                                }

                                if (objConnStatus1.MikrotikStatus == 0 && objConnStatus2.MikrotikStatus == 0)
                                {
                                    objConnStatus.MikrotikStatus = 0;
                                }
                                else if (objConnStatus1.MikrotikStatus == 1 || objConnStatus2.MikrotikStatus == 1)
                                {
                                    objConnStatus.MikrotikStatus = 1;
                                }
                            }
                            else
                            {
                                objConnStatus.StatusCode = "220";
                                objConnStatus.Status = "Status not found";
                                objConnStatus.RetMessage = objConnStatus1.RetMessage + " " + objConnStatus2.RetMessage;
                                objConnStatus.CodePortion = "Inner Catch block";
                            }

                        }
                    }
                }

                else if (InsType == 2)
                {
                    objConnStatus = FindMKStatusforPPPoE(Hostname, Username, Password, mkVersion, customerID, mkUser);
                    return objConnStatus;
                }


            }
            catch (Exception ex)
            {

                message = customerID + " " + ex.Message.ToString();
                objConnStatus.StatusCode = "230";
                objConnStatus.Status = "Enabled Failed";
                objConnStatus.RetMessage = message;
                objConnStatus.CodePortion = "MikrotikStatus Main Catch Block";




            }

            return objConnStatus;
        }

        public MkConnStatus IPV4EnableDisable(string Operation, string Hostname, string Username, string Password, string mkVersion, int ProtocolId, string customerID)
        {
            MkConnStatus objConnStatus = new MkConnStatus();

            string message = "";
            string msg = "";

            if (Operation == "yes")
                msg = "Disabled";
            else msg = "Enabled";

            try
            {

                if (customerID.Contains('-'))
                {

                    customerID = customerID.Replace("-", "");
                }


                if (Hostname != "" && Username != "" && Password != "" && ProtocolId != 0)
                {
                    using (ITikConnection connection = ConnectionFactory.CreateConnection(TikConnectionType.Api))
                    {
                        int dlngth = 0;

                        IEnumerable<ITikReSentence> response = null;
                        connection.Open(Hostname, Username, Password, mkVersion);

                        var loadCmd = connection.CreateCommandAndParameters("/ip/arp/print", "comment", customerID);
                        response = loadCmd.ExecuteList();
                        dlngth = response.Count();

                        var itemId = "";

                        for (int i = 0; i < dlngth; i++)
                        {
                            try
                            {
                                var itemId1 = response.ElementAt(i);

                                itemId = itemId1.GetId();
                                var updateCmd = connection.CreateCommandAndParameters("/ip/arp/set", "disabled", Operation, TikSpecialProperties.Id, itemId);
                                updateCmd.ExecuteNonQuery();


                                objConnStatus.StatusCode = "200";
                                objConnStatus.Status = "Success";
                                objConnStatus.RetMessage = "IP" + msg + " Success";
                                objConnStatus.CodePortion = "Inner Try block";

                                break;
                            }
                            catch (Exception ex)
                            {

                                if (i == dlngth - 1)
                                {
                                    message = customerID + " " + ex.Message.ToString();
                                    objConnStatus.StatusCode = "220";
                                    objConnStatus.Status = msg + " Failed";
                                    objConnStatus.RetMessage = message;
                                    objConnStatus.CodePortion = "Inner Catch block";

                                }
                                continue;
                            }

                        }


                        if (dlngth == 0)
                        {
                            message = message + "Customer Comment Mismatch, " + customerID + " so no operation";

                            objConnStatus.StatusCode = "240";
                            objConnStatus.Status = msg + " Failed";
                            objConnStatus.RetMessage = message;
                            objConnStatus.CodePortion = "lenth0 block";
                        }
                    }

                }
                else
                {
                    objConnStatus.StatusCode = "210";
                    objConnStatus.Status = msg + " Failed";
                    objConnStatus.RetMessage = "Host, Username, Password, IP did not provided.";
                    objConnStatus.CodePortion = "Host, Username, Password, IP check Block";
                }

            }
            catch (Exception ex)
            {

                message = customerID + " " + ex.Message.ToString();
                objConnStatus.StatusCode = "230";
                objConnStatus.Status = msg + " Failed";
                objConnStatus.RetMessage = message;
                objConnStatus.CodePortion = "Main Catch Block";

            }

            return objConnStatus;
        }

        public MkConnStatus IPV6EnableDisable(string Operation, string Hostname, string Username, string Password, string mkVersion, int ProtocolId, string customerID)
        {
            MkConnStatus objConnStatus = new MkConnStatus();

            string message = "";

            string msg = "";
            if (Operation == "yes")
                msg = "Disabled";
            else msg = "Enabled";

            try
            {

                if (customerID.Contains('-'))
                {

                    customerID = customerID.Replace("-", "");
                }


                if (Hostname != "" && Username != "" && Password != "" && ProtocolId != 0)
                {
                    using (ITikConnection connection = ConnectionFactory.CreateConnection(TikConnectionType.Api))
                    {
                        int dlngth = 0;
                        IEnumerable<ITikReSentence> response = null;
                        connection.Open(Hostname, Username, Password, mkVersion);

                        var loadCmd = connection.CreateCommandAndParameters("/ipv6/address/print", "comment", customerID);
                        response = loadCmd.ExecuteList();

                        dlngth = response.Count();

                        var itemId = "";

                        for (int i = 0; i < dlngth; i++)
                        {
                            try
                            {
                                var itemId1 = response.ElementAt(i);

                                itemId = itemId1.GetId();
                                var updateCmd = connection.CreateCommandAndParameters("/ipv6/address/set", "disabled", Operation, TikSpecialProperties.Id, itemId);
                                updateCmd.ExecuteNonQuery();

                                objConnStatus.StatusCode = "200";
                                objConnStatus.Status = "Success";
                                objConnStatus.RetMessage = "IP" + msg + " Sucess";
                                objConnStatus.CodePortion = "Inner Try block";

                                break;
                            }
                            catch (Exception ex)
                            {

                                if (i == dlngth - 1)
                                {
                                    message = customerID + " " + ex.Message.ToString();
                                    objConnStatus.StatusCode = "220";
                                    objConnStatus.Status = msg + " Failed";
                                    objConnStatus.RetMessage = message;
                                    objConnStatus.CodePortion = "Inner Catch block";

                                }
                                continue;
                            }

                        }


                        if (dlngth == 0)
                        {
                            message = message + "Customer comment mismatch in router for " + customerID + ", so no operation executed";

                            objConnStatus.StatusCode = "240";
                            objConnStatus.Status = msg + " Failed";
                            objConnStatus.RetMessage = message;
                            objConnStatus.CodePortion = "lenth0 block";
                        }
                    }

                }
                else
                {
                    objConnStatus.StatusCode = "210";
                    objConnStatus.Status = msg + " Failed";
                    objConnStatus.RetMessage = "Host, Username, Password, IP did not provided.";
                    objConnStatus.CodePortion = "Host, Username, Password, IP check Block";
                }

            }
            catch (Exception ex)
            {

                message = customerID + " " + ex.Message.ToString();
                objConnStatus.StatusCode = "230";
                objConnStatus.Status = msg + " Failed";
                objConnStatus.RetMessage = message;
                objConnStatus.CodePortion = "Main Catch Block";

            }

            return objConnStatus;
        }

        public MkConnStatus FindIPV4Status(string Hostname, string Username, string Password, string mkVersion, int ProtocolId, string customerID)
        {
            MkConnStatus objConnStatus = new MkConnStatus();

            string message = "";
            try
            {

                if (customerID.Contains('-'))
                {

                    customerID = customerID.Replace("-", "");
                }


                if (Hostname != "" && Username != "" && Password != "" && ProtocolId != 0)
                {
                    using (ITikConnection connection = ConnectionFactory.CreateConnection(TikConnectionType.Api))
                    {
                        int dlngth = 0;
                        IEnumerable<ITikReSentence> response = null;
                        connection.Open(Hostname, Username, Password, mkVersion);

                        var loadCmd = connection.CreateCommandAndParameters("/ip/arp/print", "comment", customerID);
                        response = loadCmd.ExecuteList();
                        dlngth = response.Count();
                        objConnStatus.DLength = dlngth;

                        var itemId = "";

                        for (int i = 0; i < dlngth; i++)
                        {
                            try
                            {
                                var itemId1 = response.ElementAt(i);

                                var disable_status = "";
                                var invalid_status = "";
                                var dynamic_status = "";


                                itemId = itemId1.GetId();
                                disable_status = itemId1.GetResponseField("disabled");
                                invalid_status = itemId1.GetResponseField("invalid");
                                dynamic_status = itemId1.GetResponseField("dynamic");

                                if (disable_status == "false" && invalid_status == "false" && dynamic_status == "false")
                                {
                                    objConnStatus.MikrotikStatus = 1;
                                    objConnStatus.StatusCode = "200";

                                }
                                else
                                {
                                    objConnStatus.MikrotikStatus = 0;
                                    objConnStatus.StatusCode = "200";

                                }


                                //objConnStatus.StatusCode = "200";
                                //objConnStatus.Status = "Success";
                                //objConnStatus.RetMessage = "IP Enabled Sucess";
                                //objConnStatus.CodePortion = "Inner Try block";

                                // break;
                            }
                            catch (Exception ex)
                            {

                                if (i == dlngth - 1)
                                {
                                    message = customerID + " " + ex.Message.ToString();
                                    objConnStatus.StatusCode = "220";
                                    objConnStatus.Status = "Status not found";
                                    objConnStatus.RetMessage = message;
                                    objConnStatus.CodePortion = "Inner Catch block";

                                }

                                continue;
                            }

                        }

                        if (dlngth == 0)
                        {
                            message = message + "Customer comment mismatch in router for " + customerID + ", so no operation executed";

                            objConnStatus.StatusCode = "240";
                            objConnStatus.Status = "Status not found";
                            objConnStatus.RetMessage = message;
                            objConnStatus.CodePortion = "lenth0 block";
                        }
                    }

                }
                else
                {
                    objConnStatus.StatusCode = "210";
                    objConnStatus.Status = "Status not found";
                    objConnStatus.RetMessage = "Host, Username, Password, IP did not provided.";
                    objConnStatus.CodePortion = "Host, Username, Password, IP check Block";
                }

            }
            catch (Exception ex)
            {

                message = customerID + " " + ex.Message.ToString();
                objConnStatus.StatusCode = "230";
                objConnStatus.Status = "Status not found";
                objConnStatus.RetMessage = message;
                objConnStatus.CodePortion = "Main Catch Block";

            }

            return objConnStatus;
        }

        public MkConnStatus FindIPV6Status(string Hostname, string Username, string Password, string mkVersion, int ProtocolId, string customerID)
        {
            MkConnStatus objConnStatus = new MkConnStatus();

            string message = "";
            try
            {

                if (customerID.Contains('-'))
                {

                    customerID = customerID.Replace("-", "");
                }

                if (Hostname != "" && Username != "" && Password != "" && ProtocolId != 0)
                {
                    using (ITikConnection connection = ConnectionFactory.CreateConnection(TikConnectionType.Api))
                    {
                        int dlngth = 0;
                        IEnumerable<ITikReSentence> response = null;
                        connection.Open(Hostname, Username, Password, mkVersion);

                        var loadCmd = connection.CreateCommandAndParameters("/ipv6/address/print", "comment", customerID);
                        response = loadCmd.ExecuteList();

                        dlngth = response.Count();
                        objConnStatus.DLength = dlngth;

                        var itemId = "";

                        for (int i = 0; i < dlngth; i++)
                        {
                            try
                            {
                                var itemId1 = response.ElementAt(i);

                                var disable_status = "";
                                var invalid_status = "";
                                var dynamic_status = "";


                                disable_status = itemId1.GetResponseField("disabled");
                                invalid_status = itemId1.GetResponseField("invalid");
                                dynamic_status = itemId1.GetResponseField("dynamic");

                                if (disable_status == "false" && invalid_status == "false")
                                {
                                    objConnStatus.MikrotikStatus = 1;
                                    objConnStatus.StatusCode = "200";
                                }
                                else
                                {
                                    objConnStatus.MikrotikStatus = 0;
                                    objConnStatus.StatusCode = "200";
                                }

                                //objConnStatus.StatusCode = "200";
                                //objConnStatus.Status = "Success";
                                //objConnStatus.RetMessage = "IP Enabled Sucess";
                                //objConnStatus.CodePortion = "Inner Try block";
                                //break;
                            }
                            catch (Exception ex)
                            {

                                if (i == dlngth - 1)
                                {
                                    message = customerID + " " + ex.Message.ToString();
                                    objConnStatus.StatusCode = "220";
                                    objConnStatus.Status = "Status not found";
                                    objConnStatus.RetMessage = message;
                                    objConnStatus.CodePortion = "Inner Catch block";
                                }

                                continue;
                            }

                        }

                        if (dlngth == 0)
                        {
                            message = message + "Customer comment mismatch in router for " + customerID + ", so no operation executed";

                            objConnStatus.StatusCode = "240";
                            objConnStatus.Status = "Status not found";
                            objConnStatus.RetMessage = message;
                            objConnStatus.CodePortion = "lenth0 block";
                        }
                    }
                }
                else
                {
                    objConnStatus.StatusCode = "210";
                    objConnStatus.Status = "Status not found";
                    objConnStatus.RetMessage = "Host, Username, Password, IP did not provided.";
                    objConnStatus.CodePortion = "Host, Username, Password, IP check Block";
                }

            }
            catch (Exception ex)
            {

                message = customerID + " " + ex.Message.ToString();
                objConnStatus.StatusCode = "230";
                objConnStatus.Status = "Status not found";
                objConnStatus.RetMessage = message;
                objConnStatus.CodePortion = "Main Catch Block";

            }

            return objConnStatus;
        }



        public MkConnStatus EnableDisableforPPPoE(string Operation, string Hostname, string Username, string Password, string mkVersion, string customerID, string mkUser)
        {
            MkConnStatus objConnStatus = new MkConnStatus();

            string message = "";
            string msg = "";

            if (Operation == "true")
                msg = "Disabled";
            else msg = "Enabled";

            try
            {

                if (customerID.Contains('-'))
                {

                    customerID = customerID.Replace("-", "");
                }

                using (ITikConnection connection = ConnectionFactory.CreateConnection(TikConnectionType.Api))
                {

                    IEnumerable<ITikReSentence> response = null;
                    connection.Open(Hostname, Username, Password, mkVersion);

                    ITikReSentence secret = connection.CreateCommandAndParameters("/ppp/secret/print", "name", mkUser).ExecuteSingleRow();

                    // Mk real time status check
                    string statusMk = secret.Words["disabled"].ToString();

                    // Disable the secret.
                    connection.CreateCommandAndParameters("/ppp/secret/set", ".id", secret.Words[".id"], "disabled", Operation).ExecuteNonQuery();


                    objConnStatus.StatusCode = "200";
                    objConnStatus.Status = "Success";
                    objConnStatus.RetMessage = "IP" + msg + " Success";
                    objConnStatus.CodePortion = "Inner Try block";


                    try
                    {
                        // Remove from Active directory
                        if (Operation.Equals("true"))
                        {
                            ITikReSentence activeSentence = connection.CreateCommandAndParameters("/ppp/active/print", "name", mkUser).ExecuteSingleRow();
                            connection.CreateCommandAndParameters("/ppp/active/remove", ".id", activeSentence.Words[".id"]).ExecuteNonQuery();
                        }


                    }
                    catch (Exception)
                    {

                        objConnStatus.StatusCode = "200";
                        objConnStatus.Status = "Success";
                        objConnStatus.RetMessage = "IP" + msg + " Success";
                        objConnStatus.CodePortion = "Inner Catch block";
                    }


                }



            }
            catch (Exception ex)
            {

                message = customerID + " " + ex.Message;
                objConnStatus.StatusCode = "230";
                objConnStatus.Status = msg + " Failed";
                objConnStatus.RetMessage = message;
                objConnStatus.CodePortion = "Main Catch Block";

            }

            return objConnStatus;
        }


        public MkConnStatus FindMKStatusforPPPoE(string Hostname, string Username, string Password, string mkVersion, string customerID, string mkUser)
        {
            MkConnStatus objConnStatus = new MkConnStatus();

            string message = "";
            try
            {

                if (customerID.Contains('-'))
                {

                    customerID = customerID.Replace("-", "");
                }


                using (ITikConnection connection = ConnectionFactory.CreateConnection(TikConnectionType.Api))
                {

                    IEnumerable<ITikReSentence> response = null;
                    connection.Open(Hostname, Username, Password, mkVersion);

                    ITikReSentence secret = connection.CreateCommandAndParameters("/ppp/secret/print", "name", mkUser).ExecuteSingleRow();


                    // Mk real time status check
                    string statusMk = secret.Words["disabled"].ToString();


                    if (statusMk == "false")
                    {
                        objConnStatus.MikrotikStatus = 1;
                        objConnStatus.StatusCode = "200";

                    }
                    else
                    {
                        objConnStatus.MikrotikStatus = 0;
                        objConnStatus.StatusCode = "200";
                    }
                }
            }
            catch (Exception ex)
            {

                message = customerID + " " + ex.Message.ToString();
                objConnStatus.StatusCode = "230";
                objConnStatus.Status = "Status not found";
                objConnStatus.RetMessage = message;
                objConnStatus.CodePortion = "FindMKStatusforPPPoE Main Catch Block";



            }

            return objConnStatus;
        }

    }
}
