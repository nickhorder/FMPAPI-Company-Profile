using System.Text.Json.Serialization;

//namespace FMPAPICompanyProfile
//{
public class ProfileRepository
    { 
/*
     *  Definitions of Json Properties in the FMP API, converted to their equivalents in 
     *  this C# program. This is essentially a mapper
*/
    // Values from Company Key Stats (profile as endpoint)

            [JsonPropertyName("companyName")]
            public string CompanyName { get; set; }

            [JsonPropertyName("exchangeShortName")]
            public string ExchangeShortName { get; set; }

            [JsonPropertyName("description")]
            public string Description { get; set; }
        }
    