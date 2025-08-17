using BillgenixProcessorService.Models;
using Newtonsoft.Json;

namespace BillgenixProcessorService.ApiIntegration
{
    public class BillgenixRadiusClient
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _setting;


        public BillgenixRadiusClient(AppSettings setting, HttpClient client)
        {
            var uri = new Uri(setting.baseUrlRadiusAPI);
            _httpClient = client;
            _httpClient.BaseAddress = uri;
        }

        //public MkStatus GetMkStatus(string cid)
        //{
        //    var status = new MkStatus();
        //    try
        //    {
        //        HttpResponseMessage response = httpClient.GetAsync("/api/Radius/GetMkStatus?cid=" + cid).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string responseBody = response.Content.ReadAsStringAsync().Result;
        //            status = JsonConvert.DeserializeObject<MkStatus>(responseBody);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        status.RetMessage = "MIKROTIK NOT RESPONED";
        //    }
        //    return status;
        //}


        //public string GetMrtgGraphBase64(string cid)
        //{


        //    try
        //    {
        //        HttpResponseMessage response = httpClient.GetAsync(string.Format("/api/Mrtg/GraphByCID?cid={0}", cid)).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string responseBody = response.Content.ReadAsStringAsync().Result;
        //            if (!string.IsNullOrEmpty(responseBody))
        //            {
        //                return JsonConvert.DeserializeObject(responseBody).ToString();

        //            }

        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //    return "";
        //}

        public CustomerTraffic GetTrafficData(string cid)
        {
            try
            {
                HttpResponseMessage response = _httpClient.GetAsync(string.Format("/api/Mrtg/GetTraffic?cid={0}", cid)).Result;
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
