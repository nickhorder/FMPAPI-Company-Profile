using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using Newtonsoft.Json;


namespace FMPAPI
{
    /*
      *  Definitions of Json Properties in the FMP API, converted to their equivalents in 
      *  this C# program. This is essentially a mapper
    */

    public class StockListRepository
    {


        // Values from Stock List Endpoint
        // https://financialmodelingprep.com/api/v3/stock/list? 

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("name")]
        public string SecurityName { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("exchange")]
        public string Exchange { get; set; }

        [JsonPropertyName("exchangeShortName")]
        public string ExchangeShortName { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }


    }
}

 
