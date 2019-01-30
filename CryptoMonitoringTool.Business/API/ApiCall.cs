using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMonitoringTool.Business.API
{
    public class ApiCall
    {
        #region Apicall

        public string apiKey;
        public string apiSecret;
        public byte[] apiSecretBytes;
        public const string SignHeaderName = "apisign";
        public readonly Encoding encoding = Encoding.UTF8;
        public HttpClient httpClient;

        public ApiCall()
        {
            this.apiKey = null;
            this.apiSecret = null;
            this.apiSecretBytes = null;
            this.httpClient = new HttpClient();
        }


        public ApiCall(string apiKey, string apiSecret)
        {
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
            this.apiSecretBytes = encoding.GetBytes(apiSecret);
            this.httpClient = new HttpClient();
        }
        

        private string ConvertParameterListToString(IDictionary<string, string> parameters)
        {
            if (parameters.Count == 0) return "";
            return parameters.Select(param => WebUtility.UrlEncode(param.Key) + "=" + WebUtility.UrlEncode(param.Value)).Aggregate((l, r) => l + "&" + r);
        }

        private RequestAuthentication CreateRequestAuthentication(string uri)
        {
            return CreateRequestAuthentication(uri, new Dictionary<string, string>());
        }
        private RequestAuthentication CreateRequestAuthentication(string uri, IDictionary<string, string> parameters)
        {
            parameters = new Dictionary<string, string>(parameters);
            var nonce = DateTime.Now.Ticks;
            parameters.Add("apikey", apiKey);
            parameters.Add("nonce", nonce.ToString());

            var parameterString = ConvertParameterListToString(parameters);
            var completeUri = uri + "?" + parameterString;

            var uriBytes = encoding.GetBytes(completeUri);
            using (var hmac = new HMACSHA512(apiSecretBytes))
            {
                var hash = hmac.ComputeHash(uriBytes);
                var hashText = ByteToString(hash);
                return new RequestAuthentication(completeUri, hashText);
            }
        }

        protected HttpRequestMessage CreateRequest(HttpMethod httpMethod, string uri, bool includeAuthentication = true)
        {
            return CreateRequest(httpMethod, uri, new Dictionary<string, string>(), includeAuthentication);
        }

        protected HttpRequestMessage CreateRequest(HttpMethod httpMethod, string uri, IDictionary<string, string> parameters, bool includeAuthentication)
        {
            if (includeAuthentication)
            {
                RequestAuthentication requestAuthentication = CreateRequestAuthentication(uri, parameters);
                var Request = new HttpRequestMessage(httpMethod, requestAuthentication.Uri);
                Request.Headers.Add(SignHeaderName, requestAuthentication.Hash);
                return Request;
            }
            else
            {
                var parameterString = ConvertParameterListToString(parameters);
                var completeUri = $"{uri}?{parameterString}";
                var Request = new HttpRequestMessage(httpMethod, completeUri);
                return Request;
            }
        }

        protected async Task<JToken> Request(HttpMethod httpMethod, string uri, bool includeAuthentication = true)
        {
            return await Request(httpMethod, uri, new Dictionary<string, string>(), includeAuthentication);
        } 

        protected async Task<JToken> Request(HttpMethod httpMethod, string uri, IDictionary<string, string> parameters, bool includeAuthentication = true)
        {
            var Request = CreateRequest(HttpMethod.Get, uri, parameters, includeAuthentication);
            HttpResponseMessage response = null;
            while (response == null)
            {
                try
                {
                    response = httpClient.SendAsync(Request).Result;
                }
                catch (Exception)
                {
                    response = null;
                }
            }
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResponse>(content);
            if (!result.Success) throw new Exception("Request failed: " + result.Message);
            return result.Result;
        }

        private string ByteToString(byte[] buffor)
        {
            string sbinary = "";
            for (int i = 0; i < buffor.Length; i++)
            {
                sbinary += buffor[i].ToString("X2");
            }

            return sbinary;
        }
        #endregion
    }
}
