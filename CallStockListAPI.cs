using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;

namespace FMPAPI
{
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
}
