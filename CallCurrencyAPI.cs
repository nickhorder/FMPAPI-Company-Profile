using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
 
namespace FMPAPI
{
    public class CallCurrencyAPI
    {
        public static async Task<CurrencyRepository> CurrencyAPI(string CurrencyURI)
        {
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(CurrencyURI);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync(CurrencyURI);
                    response.EnsureSuccessStatusCode();
                    var responseAsString = await response.Content.ReadAsStringAsync();
                    var currencyrepos = JsonConvert.DeserializeObject<CurrencyRepository>(responseAsString);

                    // Write outcome of call to Currency API
                    bool CurrencyAPIOutcome = currencyrepos.Success;
                    switch (CurrencyAPIOutcome)
                    {
                        case true:
                            {
                                Console.WriteLine($"Call to Currency API was successful on " + currencyrepos.Date +
                                    " at " + currencyrepos.Timestamp + "."
                                    + " The base currency is: " + currencyrepos.Base);
                            }
                            break;
                        case false:
                            {
                                Console.WriteLine("Call to Currency API was not successful");
                            }
                            break;
                    }

                 return currencyrepos;
                  
                }
                

            }
            
        }

    }

}

    
