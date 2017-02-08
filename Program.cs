using System;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.WebUtilities;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    public class Program
    {
        private static HttpClient client = new HttpClient();
        public static void Main(string[] args)
        {
            Console.WriteLine("Please paste URL");
            var pictureUrl = Console.ReadLine();
            Console.WriteLine("URL is: " + pictureUrl);

            call_service(pictureUrl).Wait();
        }

        private static async Task call_service(string pictureUrl)
        {
            var queryString = QueryHelpers.ParseQuery(string.Empty);
            cogSecret cogSecret;
            using(var reader = new StreamReader(File.OpenRead("Secret.json")))
            {
                var data = reader.ReadToEnd();
                cogSecret = JsonConvert.DeserializeObject<cogSecret>(data);
            }

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", cogSecret.cog_key);
            queryString["visualFeatures"] = "Categories,Description";
            var uri = "https://westus.api.cognitive.microsoft.com/vision/v1.0/analyze?" + queryString;

            HttpResponseMessage response;
           
            var messageBody = new MessageBody { url = pictureUrl };
            var pictureJson = JsonConvert.SerializeObject(messageBody);
            byte[] byteData = Encoding.UTF8.GetBytes(pictureJson); 

            using (var content = new ByteArrayContent(byteData))
            {
               content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
               response = await client.PostAsync(uri, content);
            } 
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
    public class MessageBody
    {
        public string url;
    }
    public class cogSecret
    {
        public string cog_key {get; set;}
    }
}