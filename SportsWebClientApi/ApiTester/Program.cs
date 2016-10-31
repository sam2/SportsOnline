using SportsWebClientApi;
using SportsWebDtos;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiTester
{
    class Program
    {
        public static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        public static string token;
        public static bool loggedIn = false;

        public static async Task MainAsync(string[] args)
        {
            var client = new HttpClient();

            const string email = "test@account.com", password = "Test!11";

            await HttpRequestHelper.CreateAccountAsync(client, email, password);

            SportsWebClient.Login(email, password, OnLoginSuccess, OnFail);
            while (!loggedIn) {; }
            
            Console.WriteLine("Access token: {0}", token);

            Console.WriteLine();

            await HttpRequestHelper.GetResourceAsync<ObjectTest>(client, token, OnGetResourceSuccess, OnFail);            
           
            

            Console.ReadLine();
        }

        public static void OnGetResourceSuccess(ObjectTest s)
        {
            Console.WriteLine("API response: {0}", s.testMessage);
        }

        public static void OnLoginSuccess(LoginResponse s)
        {
            Console.WriteLine("success");
            token = s.access_token;
            loggedIn = true;
        }

        public static void OnFail(ErrorResult s)
        {
            Console.WriteLine("fail: "+s.error + " " + s.error_description);
        }
    }
}
