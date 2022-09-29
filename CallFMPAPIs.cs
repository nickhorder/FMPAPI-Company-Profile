using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace FMPAPI
{

    // This class (named CallFMPAPIs) is an aggregate of awaitable async Tasks that call the FMP APIs.

    public class CallStockListAPI

    {
        public static readonly HttpClient client = new HttpClient();

        public static async Task<List<StockListRepository>> StockListAPI(string StockListURI)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            var streamTask1 = client.GetStreamAsync($"{StockListURI}");
            var stocklistrepos = await JsonSerializer.DeserializeAsync<List<StockListRepository>>(await streamTask1);

            return stocklistrepos;

        }


    }


    public class CallProfileAPI
    {
        public static readonly HttpClient client = new HttpClient();

        public static async Task<List<JSONRepository>> ProfileAPI(string ProfileURI)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            //   client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask1 = client.GetStreamAsync($"{ProfileURI}");
            var profilerepos = await JsonSerializer.DeserializeAsync<List<JSONRepository>>(await streamTask1);
            //   var profilerepos = await System.Text.Json.JsonSerializer.DeserializeAsync<List<JSONRepository>>(await streamTask1);

            return profilerepos;
        }
    }



    public class CallIncomeAPI
    {
        public static readonly HttpClient client = new HttpClient();

        public static async Task<List<JSONRepository>> IncomeAPI(string IncomeURI)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            var streamTask3 = client.GetStreamAsync($"{IncomeURI}");
            var incomerepos = await JsonSerializer.DeserializeAsync<List<JSONRepository>>(await streamTask3);
            
            return (incomerepos);
        }
    }



    public class CallBalanceAPI
    {
        public static readonly HttpClient client = new HttpClient();

        public static async Task<List<JSONRepository>> BalanceAPI(string BalanceURI)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            var streamTask2 = client.GetStreamAsync($"{BalanceURI}");
            var balancerepos = await System.Text.Json.JsonSerializer.DeserializeAsync<List<JSONRepository>>(await streamTask2);
            /*
                            string jsonContent = string.Empty;

                        var apiResponse = client.GetAsync($"{BalanceURI}").Result;
                        if (apiResponse.IsSuccessStatusCode)

                            jsonContent = apiResponse.Content.ReadAsStringAsync().Result;
                        var printObj = JsonConvert.DeserializeObject<List<JSONRepository>>(jsonContent);
                        Console.WriteLine(jsonContent);
            */
            return balancerepos;

        }
    }
    

}
