using Common.Infrastructure.Models;
using Newtonsoft.Json;
using TrafficProcessorService.Extensions;
using TrafficProcessorService.Models;

namespace TrafficProcessorService.ApiIntegration
{
    public class BillgenixRadiusClient
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _setting;


        public BillgenixRadiusClient(AppSettingsSevice setting, HttpClient client)
        {
            var uri = new Uri(setting.baseUrlRadiusAPI);
            _httpClient = client;
            _httpClient.BaseAddress = uri;
        }

        
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
