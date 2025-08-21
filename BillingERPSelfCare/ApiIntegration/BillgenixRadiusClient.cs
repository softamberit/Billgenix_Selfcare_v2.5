using BillgenixTicketing.Models;
using BillingERPSelfCare.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web;

namespace BillgenixTicketing.ApiIntigration
{
    public class BillgenixRadiusClient
    {
        private readonly HttpClient httpClient;
        public BillgenixRadiusClient()
        {
            var uri = new Uri(ConfigurationManager.AppSettings["baseUrlRadiusAPI"].ToString());
            httpClient = new HttpClient();
            httpClient.BaseAddress = uri;
        }

        public MkStatus GetMkStatus(string cid)
        {
            var status = new MkStatus();
            try
            {
                HttpResponseMessage response = httpClient.GetAsync("/api/Radius/GetMkStatus?cid=" + cid).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    status = JsonConvert.DeserializeObject<MkStatus>(responseBody);
                }
            }
            catch (Exception)
            {
                status.RetMessage = "MIKROTIK NOT RESPONED";
            }
            return status;
        }


        public string GetMrtgGraphBase64(string cid,string type)
        {


            try
            {
                HttpResponseMessage response = httpClient.GetAsync(string.Format("/api/Mrtg/GraphByCID?cid={0}&type={1}", cid, type)).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(responseBody))
                    {
                        return JsonConvert.DeserializeObject(responseBody).ToString();

                    }

                }
            }
            catch (Exception)
            {

            }
            return "";
        }

        public CustomerTraffic GetTrafficData(string cid)
        {
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(string.Format("/api/Mrtg/GetTraffic?cid={0}", cid)).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(responseBody))
                    {
                        return JsonConvert.DeserializeObject<CustomerTraffic>(responseBody);

                    }

                }
            }
            catch (Exception)
            {

            }
            return new CustomerTraffic();
        }

    }
}