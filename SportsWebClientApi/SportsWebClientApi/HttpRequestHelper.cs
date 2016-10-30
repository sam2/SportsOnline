using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SportsWebDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SportsWebClientApi
{
    public class ErrorResult
    {
        public string error;
        public string error_description;
    }

    public class HttpRequestHelper
    {      

        public static async Task InterPretResponse<T>(HttpResponseMessage response, Action<T> onSuccess, Action<ErrorResult> onFail)
        {
            string json = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {                
                T obj = JsonConvert.DeserializeObject<T>(json);
                if (obj != null)
                {
                    onSuccess(obj);
                }
                else
                    onFail(JsonConvert.DeserializeObject<ErrorResult>(json));
            }
            else onFail(JsonConvert.DeserializeObject<ErrorResult>(json));
            
        }

        public static async Task CreateAccountAsync(HttpClient client, string email, string password)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:58795/Account/Register");
            request.Content = new StringContent(JsonConvert.SerializeObject(new { email, password }), Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            response.EnsureSuccessStatusCode();
        }

        public static async Task GetTokenAsync<T>(HttpClient client, string email, string password, Action<T> onSucess, Action<ErrorResult> onFail)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:58795/connect/token");
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "password",
                ["username"] = email,
                ["password"] = password
            });

            HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            await InterPretResponse<T>(response, onSucess, onFail);
            
        }

        public static async Task<HttpResponseMessage> GetResourceAsync(HttpClient client, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:58795/api/message");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);          

            return response;
        }
    }
}
