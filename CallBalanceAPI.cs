using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace FMPAPI
{
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
*/

            return balancerepos;

                }
            }
        }
    

