using BillingERPSelfCare.Bkash.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;

namespace BillingERPSelfCare.Bkash
{
    public sealed class BkashService
    {
        private const string ContentType = "application/json";
        private const string AcceptHeader = "application/json";
        private readonly string baseUrl;
        private readonly string username;
        private readonly string password;
        private readonly string appKey;
        private readonly string appSecret;
        public BkashService()
        {
            baseUrl = ConfigurationManager.AppSettings["Bkash_BaseUrl"];
            username = ConfigurationManager.AppSettings["Bkash_Username"];
            password = ConfigurationManager.AppSettings["Bkash_Password"];
            appKey = ConfigurationManager.AppSettings["Bkash_AppKey"];
            appSecret = ConfigurationManager.AppSettings["Bkash_AppSecret"];
        }
        private T BaseRequest<T>(string url, Method method, object body, Dictionary<string, string> headers)
        {
            try
            {
                var client = new RestClient(url);
                var request = new RestRequest(string.Empty, method);

                // Set common headers
                request.AddHeader("Content-Type", ContentType);
                request.AddHeader("Accept", AcceptHeader);

                // Add additional headers
                foreach (var header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }

                // Add body
                var jsonBody = JsonConvert.SerializeObject(body);
                request.AddParameter(ContentType, jsonBody, ParameterType.RequestBody);
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                // Execute request
                var response = client.Execute(request);

                if (!response.IsSuccessful)
                {
                    throw new InvalidOperationException($"Error executing request: {response.StatusDescription}");
                }

                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while executing the request.", ex);
            }
        }
        public Models.TokenResponse GrantToken()
        {
            try
            {
                var reqBody = new
                {
                    app_key = this.appKey,
                    app_secret = this.appSecret
                };
                string url = this.baseUrl + "/token/grant";
                Dictionary<string, string> headers = new Dictionary<string, string>
                {
                    { "username", this.username },
                    { "password", this.password }
                };
                return BaseRequest<Models.TokenResponse>(url, Method.Post, reqBody, headers);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Models.TokenResponse RefreshToken(string refreshToken)
        {
            try
            {
                var reqBody = new
                {
                    app_key = this.appKey,
                    app_secret = this.appSecret,
                    refresh_token = refreshToken
                };
                string url = this.baseUrl + "/token/refresh";
                Dictionary<string, string> headers = new Dictionary<string, string>
                {
                    { "username", this.username },
                    { "password", this.password }
                };
                return BaseRequest<Models.TokenResponse>(url, Method.Post, reqBody, headers);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public AgreementCreateResponse CreateAgreement(string id_token, string invoiceAmount,string callBackBase, string payerRef)
        {
           
            var reqBody = new
            {
                mode = "0000",
                payerReference = payerRef,
                callbackURL = callBackBase + "/Bkash/BKashAccountAdd.aspx",
                amount = invoiceAmount,
                currency = "BDT",
                intent = "sale",
                merchantInvoiceNumber = GenerateUniqueInvoiceNumber()
            };
            string url = this.baseUrl + "/create";
            Dictionary<string, string> headers = new Dictionary<string, string>
                {
                    { "Authorization", id_token },
                    { "x-app-key", this.appKey }
                };
            return BaseRequest<AgreementCreateResponse>(url, Method.Post, reqBody, headers);
        }

        public AgreementExecuteResponse ExecuteAgreement(string paymentID, string id_token)
        {
            var reqBody = new
            {
                paymentID = paymentID
            };
            string url = this.baseUrl + "/execute";
            Dictionary<string, string> headers = new Dictionary<string, string>
                {
                    { "Authorization", id_token },
                    { "x-app-key", this.appKey }
                };
            // var res=BaseRequest<AgreementExecuteResponse>(url, Method.Post, reqBody, headers);
            //AgreementStatus(res.agreementID, id_token);
            return BaseRequest<AgreementExecuteResponse>(url, Method.Post, reqBody, headers);
        }

        public AgreementStatusResponse AgreementStatus(string agreementID, string id_token)
        {
            var reqBody = new
            {
                agreementID = agreementID
            };
            string url = this.baseUrl + "/agreement/status";
            Dictionary<string, string> headers = new Dictionary<string, string>
                {
                    { "Authorization", id_token },
                    { "x-app-key", this.appKey }
                };
            return BaseRequest<AgreementStatusResponse>(url, Method.Post, reqBody, headers);
        }
        public AgreementCancelResponse CancelAgreement(string agreementID, string id_token)
        {
            var reqBody = new
            {
                agreementID = agreementID
            };
            string url = this.baseUrl + "/agreement/cancel";
            Dictionary<string, string> headers = new Dictionary<string, string>
                {
                    { "Authorization", id_token },
                    { "x-app-key", this.appKey }
                };
            return BaseRequest<AgreementCancelResponse>(url, Method.Post, reqBody, headers);
        }

        public PaymentCreateResponse CreatePayment(string agreementID, string id_token, string invoiceAmount,string callBackBaseURL,string payerRef)
        {
            string merchantAssociation = null;
            var reqBody = new
            {
                mode = "0001",
                payerReference = payerRef,
                callbackURL = callBackBaseURL + "/UI/Client/PaymentGateway.aspx",
                agreementID = agreementID,
                amount = invoiceAmount,
                currency = "BDT",
                intent = "sale",
                merchantInvoiceNumber = GenerateUniqueInvoiceNumber(),
                merchantAssociationInfo = merchantAssociation
            };
            string url = this.baseUrl + "/create";
            Dictionary<string, string> headers = new Dictionary<string, string>
                {
                    { "Authorization", id_token },
                    { "x-app-key", this.appKey }
                };
            return BaseRequest<PaymentCreateResponse>(url, Method.Post, reqBody, headers);
        }

        public PaymentExecuteResponse ExecutePayment(string paymentID, string id_token)
        {
            var reqBody = new
            {
                paymentID = paymentID
            };
            string url = this.baseUrl + "/execute";
            Dictionary<string, string> headers = new Dictionary<string, string>
                {
                    { "Authorization", id_token },
                    { "x-app-key", this.appKey }
                };
            return BaseRequest<PaymentExecuteResponse>(url, Method.Post, reqBody, headers);
        }
        public PaymentStatusResponse PaymentStatus(string paymentID, string id_token)
        {
            var reqBody = new
            {
                paymentID = paymentID
            };
            string url = this.baseUrl + "/payment/status";
            Dictionary<string, string> headers = new Dictionary<string, string>
                {
                    { "Authorization", id_token },
                    { "x-app-key", this.appKey }
                };
            return BaseRequest<PaymentStatusResponse>(url, Method.Post, reqBody, headers);
        }
        private string GenerateUniqueInvoiceNumber()
        {
            string invoiceNumber = Guid.NewGuid().ToString();
            return invoiceNumber;
        }
    }
}
