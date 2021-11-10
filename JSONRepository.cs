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

    public class JSONRepository
    {

  
        // Values from Company Key Stats (profile as endpoint)
      
                [JsonPropertyName("symbol")]
               public string Symbol { get; set; }

                [JsonPropertyName("price")]
                public double Price { get; set; }

                [JsonPropertyName("beta")]
                public double Beta { get; set; }

                [JsonPropertyName("volAvg")]
                public double VolAvg { get; set; }

                [JsonPropertyName("mktCap")]
                public double MarketCap { get; set; }

                [JsonPropertyName("lastDiv")]
                public double LastDiv { get; set; }

                [JsonPropertyName("range")]
                public string Range { get; set; }

                [JsonPropertyName("changes")]
                public double Changes { get; set; }

                [JsonPropertyName("companyName")]
                public string CompanyName { get; set; }

                [JsonPropertyName("currency")]
                public string Currency { get; set; }

                [JsonPropertyName("exchangeShortName")]
                public string ExchangeShortName { get; set; }

                [JsonPropertyName("description")]
                public string Description { get; set; }




            // Values from Balance Sheet (Balance-Sheet as endpoint)

            [JsonPropertyName("date")]
            public string BalanceSheetDate { get; set; }

                [JsonPropertyName("period")]
                public string Period { get; set; }

                 [JsonPropertyName("propertyPlantEquipmentNet")]
                 public double NetFixedAssets { get; set; }

                [JsonPropertyName("cashAndCashEquivalents")]
                public double Cash { get; set; }

                [JsonPropertyName("totalCurrentAssets")]
                public double CurrentAssets { get; set; }

                [JsonPropertyName("totalCurrentLiabilities")]
                public double CurrentLiabilities { get; set; }

                [JsonPropertyName("totalDebt")]
                public double TotalDebt { get; set; }




            // Values from Income Statement (Income-Statement as endpoint)

                [JsonPropertyName("reportedCurrency")]
                public string ReportedCurrency { get; set; }

                [JsonPropertyName("fillingDate")]
                public string FilingDate { get; set; }

                [JsonPropertyName("operatingIncome")]
                public double OperatingProfit { get; set; }

            }
        }



        /*
         * 
         * Example of Profile call:
         * 
         * [ {
          "symbol" : "NEX.L",
          "price" : 224.4,
          "beta" : 1.489589,
          "volAvg" : 1824285,
          "mktCap" : 1378008960,
          "lastDiv" : 11.19,
          "range" : "104.6-413.5",
          "changes" : 7.399994,
          "companyName" : "National Express Group PLC",
          "currency" : "GBp",
          "cik" : null,
          "isin" : "GB0006215205",
          "cusip" : "G6374M109",
          "exchange" : "LSE",
          "exchangeShortName" : "LSE",
          "industry" : "Railroads",
          "website" : "http://www.nationalexpressgroup.com",
          "description" : "National Express Group PLC provides public transport services in the United Kingdom, Continental Europe, North Africa, North America, and the Middle East. The company operates through UK, German Rail, ALSA, and North America segments. It owns and leases buses, coaches, and trains to deliver local, regional, national, and international transportation services. The company also provides urban bus and transit services; scheduled coach services; and private hire, B2B, and commuter coach travel services. In addition, the company operates service areas and other transport-related businesses, such as fuel distribution; and offers student transportation and shuttle services. The company has a fleet of approximately 31,700 vehicles. National Express Group PLC was incorporated in 1991 and is headquartered in Birmingham, the United Kingdom.",
          "ceo" : " S. Richard Bowker",
          "sector" : "Road & Rail",
          "country" : "GB",
          "fullTimeEmployees" : "47971",
          "phone" : "448450130130",
          "address" : "National Express House, Mill Lane, Digbeth",
          "city" : "Birmingham",
          "state" : "WARWICKSHIRE",
          "zip" : null,
          "dcfDiff" : null,
          "dcf" : null,
          "image" : "https://financialmodelingprep.com/image-stock/NEX.L.png",
          "ipoDate" : "1995-04-26",
          "defaultImage" : false,
          "isEtf" : false,
          "isActivelyTrading" : true,
          "isAdr" : false,
          "isFund" : false
        } ]
        *
        * Example of Income-Statement Call:


        */
    
