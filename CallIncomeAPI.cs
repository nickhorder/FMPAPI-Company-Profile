using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace FMPAPI
{
    public class CallIncomeAPI
    {
        public static readonly HttpClient client = new HttpClient();

        public static async Task<List<JSONRepository>> IncomeAPI(string IncomeURI)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
//          client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask3 = client.GetStreamAsync($"{IncomeURI}");
            var incomerepos = await JsonSerializer.DeserializeAsync<List<JSONRepository>>(await streamTask3);



            return (incomerepos);
        }
    }
}
