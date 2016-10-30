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

            TokenAuthentication.Login(email, password, OnSuccess, OnFail);
            while (!loggedIn) {; }

            
            Console.WriteLine("Access token: {0}", token);

            Console.WriteLine();

            var resource = await HttpRequestHelper.GetResourceAsync(client, token);

            
           
            Console.WriteLine("API response: {0}", resource);

            Console.ReadLine();
        }

        public static void OnSuccess(LoginResponse s)
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
