using System;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;

namespace ConsoleApplication
{
    public class Program
    {
        private static HttpClient client = new HttpClient();
        public static void Main(string[] args)
        {
            Console.WriteLine("Please paste URL");
            var url = Console.ReadLine();
            Console.WriteLine("URL is: " + url);
        }
    }
}