using SportsWebClientApi;
using SportsWebDtos;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiTester
{
    class Program
    {
        public static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

       
       
        public static async Task MainAsync(string[] args)
        {
            SportsWebClient.OnRequestFailed += ErrorReporter;
            var client = new HttpClient();

            const string username = "test@account.com", password = "Test!11";


            if (await SportsWebClient.Login(username, password))
            {
                Console.WriteLine(username + " is logged in.");
            }     
            else
            {
                Console.WriteLine("Failed to login.");
            }

            Console.ReadLine();
        }

        public static void ErrorReporter(string error)
        {
            Console.WriteLine("ERROR: "+ error);
        }
    }
}
