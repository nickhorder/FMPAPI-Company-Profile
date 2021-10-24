using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;

namespace FMPAPICompanyProfile
{

    public class FMPAPICompanyProfileProgram

/*
     FMPAPICompanyProfile
     
     This C# program calls the Financial Modeling Prep API asynchronously with API Key and the relevant  
     company ticker symbol (instrument code - i.e EXPN.L). It retrieves company profile data, for example
     name, where the company is listed, description of it's business activities.
     Output is written to Console and CSV.
*/

    {
        private static readonly HttpClient client = new HttpClient();
        public static async Task Main(string[] args)
        {


/*      Build the URL, call Method CallAPI with input string URLCalled. foreach will build and send 
*       discrete URLs based on enumerator Tickers, that contains multiple tickers. HttpClient to make the call
*/
            {
                string ProfileEndPoint = "https://financialmodelingprep.com/api/v3/profile/";
                string APIKey = "apikey=39464dcbe9e6ca8ea538e7bba66e4773";
                string QuestionM = "?";
              
                TickerStore Tickers = new TickerStore();
                foreach (string Ticker in Tickers)
                {
             //       try
              //      {
                        string URLCalled = ProfileEndPoint + Ticker + QuestionM + APIKey;
                        var Profilerepos = await CallAPI(URLCalled);
                        Console.WriteLine($"The URL " + URLCalled + "\n" + "Returns the following:");
              //      }
                //    catch (Exception ex)
              //      {
                //        throw new ApplicationException("Exception when attempting to call API :", ex);
              //      }

/*      Output; the return from CallAPI is sent to CSV using the useful CsvHelper package.    
*      Write to Console in addition.
*/                 
                    foreach (var companyAttribute in Profilerepos)
                    {
                        try
                        {
                            // Write return to CSV   
                            string profilefilepath = @"C:\Users\X1 Carbon\OneDrive\Documents\FMPAPIProfile.csv";
                            using (StreamWriter file = new StreamWriter(profilefilepath, true))
                            {
                            file.WriteLine($"Company:," + companyAttribute.CompanyName + "," 
                              + "MarketCap " + companyAttribute.Currency + "(m):" + ","  
                              + (companyAttribute.MarketCap / 1e6));
                            }
                        }

                        catch (Exception ex)
                        {
                            throw new ApplicationException("Exception when attempting to write to CSV :", ex);
                        }
                        // Write values to Console

                        Console.WriteLine($"Company: " + companyAttribute.CompanyName + "\n"
                           + " MarketCap " + companyAttribute.Currency + "(m): "
                           + (companyAttribute.MarketCap / 1e6) + "\n");                       
                    }
                    
                }
           //     Console.WriteLine("End of API Calls");
            }

/*
*       Call FMP API with built URL, and pass JSON back for Deserialisation. JSON converted to 
*       C# string values defined in Class ProfileRepository.
*/

            static async Task<List<ProfileRepository>> CallAPI(string URLCalled)
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

                var streamTask1 = client.GetStreamAsync($"{URLCalled}");
                var repositories1 = await JsonSerializer.DeserializeAsync<List<ProfileRepository>>(await streamTask1);

                return repositories1;
            }

        }
    }
}