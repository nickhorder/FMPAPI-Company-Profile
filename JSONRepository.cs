using System.Text.Json.Serialization;

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
                public double? MarketCap { get; set; }

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

                [JsonPropertyName("exchange")]
                 public string Exchange { get; set; }

                [JsonPropertyName("exchangeShortName")]
                public string ExchangeShortName { get; set; }

                [JsonPropertyName("industry")]
                public string Industry { get; set; }

                [JsonPropertyName("isEtf")]
                public bool IsETF { get; set; }

                [JsonPropertyName("isActivelyTrading")]
                public bool IsActivelyTrading { get; set; }

                [JsonPropertyName("sector")]
                public string Sector { get; set; }

  

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

                [JsonPropertyName("revenue")]
                public double Revenue { get; set; }
/*
        public string[] LSEItems;
        public JSONRepository(string CompanyName)
        {
            LSEItems = new string[] { CompanyName };
        }

        public SymbolEnumerator GetEnumerator()
        {
            return new SymbolEnumerator(this);
        }

        // Declare the enumerator class:  
        public class SymbolEnumerator
        {
            int nIndex;
            JSONRepository collection;
            public SymbolEnumerator(JSONRepository coll)
            {
                collection = coll;
                nIndex = -1;
            }
            public bool MoveNext()
            {
                nIndex++;
                return (nIndex < collection.LSEItems.Length);
            }

            public string Current => collection.LSEItems[nIndex];

        }
 */ 
               

    }

    }
        

    
