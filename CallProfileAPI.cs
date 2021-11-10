using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;

namespace FMPAPI
{
    public class CallProfileAPI

    {
        public static readonly HttpClient client = new HttpClient();

        public static async Task<List<JSONRepository>> ProfileAPI(string ProfileURI)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            //          client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask1 = client.GetStreamAsync($"{ProfileURI}");
            var profilerepos = await JsonSerializer.DeserializeAsync<List<JSONRepository>>(await streamTask1);

            

            return profilerepos;
        }
    }
}
