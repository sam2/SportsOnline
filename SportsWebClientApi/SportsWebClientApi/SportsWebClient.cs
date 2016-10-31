using SportsWebDtos;
using System;
using System.Net.Http;

namespace SportsWebClientApi
{
    public class SportsWebClient
    {
        const string k_url = "";

        private static string authToken = "";

        static HttpClient m_Client = new HttpClient();

        public static void Login(string username, string password, Action<LoginResponse> onSuccess, Action<ErrorResult> onFail)
        {
            HttpRequestHelper.GetTokenAsync(m_Client, username, password, onSuccess, onFail);
        }
    }
}
