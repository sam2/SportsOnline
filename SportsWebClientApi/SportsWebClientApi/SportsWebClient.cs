using Newtonsoft.Json;
using SportsWebDtos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SportsWebClientApi
{
    public class SportsWebClient
    {
        const string k_url = "http://localhost:5000/";

        private static string authToken = "";

        static HttpClient m_Client = new HttpClient() { BaseAddress = new Uri(k_url) };
        
        public static event Action<string> OnRequestFailed;

        public static async Task<bool> Login(string username, string password)
        {          
            var request = new HttpRequestMessage(HttpMethod.Post, "connect/token");
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "password",
                ["username"] = username,
                ["password"] = password
            });

            HttpResponseMessage response;

            try
            {
                response = await m_Client.SendAsync(request);
            }
            catch
            {
                OnRequestFailed("Could not reach server.");
                return false;
            }            
            
            if(response.StatusCode == HttpStatusCode.OK)
            {
                LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(await response.Content.ReadAsStringAsync());
                authToken = loginResponse.access_token;
                return true;
            }
            else
            {
                ErrorResult errorResult = JsonConvert.DeserializeObject<ErrorResult>(await response.Content.ReadAsStringAsync());
                OnRequestFailed(errorResult.error_description);
                return false;
            }

                                          
        }

        #region Helpers

        
        
        #endregion
    }
}
