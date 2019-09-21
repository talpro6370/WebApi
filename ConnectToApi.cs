using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication2
{
    public class ConnectToApi
    {
        private const string URL = "http://localhost:64047/api/food/search?name=p";
        static async Task<Uri> CreateFoodAsync(Food food, HttpClient client)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/Food", food);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }
        //..........Get..........\\

        public void Get()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("").Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var dataObjects = response.Content.ReadAsAsync<IEnumerable<Food>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                foreach (var d in dataObjects)
                {
                    Console.Write("{0} ", d.ID);
                    Console.Write("{0} ", d.Name);
                    Console.Write("{0} ", d.Grade);
                    Console.Write("{0} ", d.Ingridients);
                    Console.Write("{0} ", d.Calories);
                    Console.WriteLine();
                }
            }
        }


    }
}