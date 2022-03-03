using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FMPAPI
{
    public partial class CurrencyRepository
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("rates")]
        public Rates Rates { get; set; }
    }

    public partial class Rates
    {
        [JsonProperty("AED")]
        public double AED { get; set; }

        [JsonProperty("AFN")]
        public double AFN { get; set; }

        [JsonProperty("ALL")]
        public double ALL { get; set; }

        [JsonProperty("AMD")]
        public double AMD { get; set; }

        [JsonProperty("ANG")]
        public double ANG { get; set; }

        [JsonProperty("AOA")]
        public double AOA { get; set; }

        [JsonProperty("ARS")]
        public double ARS { get; set; }

        [JsonProperty("AUD")]
        public double AUD { get; set; }

        [JsonProperty("AWG")]
        public double AWG { get; set; }

        [JsonProperty("AZN")]
        public double AZN { get; set; }

        [JsonProperty("BAM")]
        public double BAM { get; set; }

        [JsonProperty("BBD")]
        public double BBD { get; set; }

        [JsonProperty("BDT")]
        public double BDT { get; set; }

        [JsonProperty("BGN")]
        public double BGN { get; set; }

        [JsonProperty("BHD")]
        public double BHD { get; set; }

        [JsonProperty("BIF")]
        public double BIF { get; set; }

        [JsonProperty("BMD")]
        public double BMD { get; set; }

        [JsonProperty("BND")]
        public double BND { get; set; }

        [JsonProperty("BOB")]
        public double BOB { get; set; }

        [JsonProperty("BRL")]
        public double BRL { get; set; }

        [JsonProperty("BSD")]
        public double BSD { get; set; }

        [JsonProperty("BTC")]
        public double BTC { get; set; }

        [JsonProperty("BTN")]
        public double BTN { get; set; }

        [JsonProperty("BWP")]
        public double BWP { get; set; }

        [JsonProperty("BYN")]
        public double BYN { get; set; }

        [JsonProperty("BYR")]
        public double BYR { get; set; }

        [JsonProperty("BZD")]
        public double BZD { get; set; }

        [JsonProperty("CAD")]
        public double CAD { get; set; }

        [JsonProperty("CDF")]
        public double CDF { get; set; }

        [JsonProperty("CHF")]
        public double CHF { get; set; }

        [JsonProperty("CLF")]
        public double CLF { get; set; }

        [JsonProperty("CLP")]
        public double CLP { get; set; }

        [JsonProperty("CNY")]
        public double CNY { get; set; }

        [JsonProperty("COP")]
        public double COP { get; set; }

        [JsonProperty("CRC")]
        public double CRC { get; set; }

        [JsonProperty("CUC")]
        public double CUC { get; set; }

        [JsonProperty("CUP")]
        public double CUP { get; set; }

        [JsonProperty("CVE")]
        public double CVE { get; set; }

        [JsonProperty("CZK")]
        public double CZK { get; set; }

        [JsonProperty("DJF")]
        public double DJF { get; set; }

        [JsonProperty("DKK")]
        public double DKK { get; set; }

        [JsonProperty("DOP")]
        public double DOP { get; set; }

        [JsonProperty("DZD")]
        public double DZD { get; set; }

        [JsonProperty("EGP")]
        public double EGP { get; set; }

        [JsonProperty("ERN")]
        public double ERN { get; set; }

        [JsonProperty("ETB")]
        public double ETB { get; set; }

        [JsonProperty("EUR")]
        public double EUR { get; set; }

        [JsonProperty("FJD")]
        public double FJD { get; set; }

        [JsonProperty("FKP")]
        public double FKP { get; set; }

        [JsonProperty("GBP")]
        public double GBP { get; set; }

        [JsonProperty("GEL")]
        public double GEL { get; set; }

        [JsonProperty("GGP")]
        public double GGP { get; set; }

        [JsonProperty("GHS")]
        public double GHS { get; set; }

        [JsonProperty("GIP")]
        public double GIP { get; set; }

        [JsonProperty("GMD")]
        public double GMD { get; set; }

        [JsonProperty("GNF")]
        public double GNF { get; set; }

        [JsonProperty("GTQ")]
        public double GTQ { get; set; }

        [JsonProperty("GYD")]
        public double GYD { get; set; }

        [JsonProperty("HKD")]
        public double HKD { get; set; }

        [JsonProperty("HNL")]
        public double HNL { get; set; }

        [JsonProperty("HRK")]
        public double HRK { get; set; }

        [JsonProperty("HTG")]
        public double HTG { get; set; }

        [JsonProperty("HUF")]
        public double HUF { get; set; }

        [JsonProperty("IDR")]
        public double IDR { get; set; }

        [JsonProperty("ILS")]
        public double ILS { get; set; }

        [JsonProperty("IMP")]
        public double IMP { get; set; }

        [JsonProperty("INR")]
        public double INR { get; set; }

        [JsonProperty("IQD")]
        public double IQD { get; set; }

        [JsonProperty("IRR")]
        public double IRR { get; set; }

        [JsonProperty("ISK")]
        public double ISK { get; set; }

        [JsonProperty("JEP")]
        public double JEP { get; set; }

        [JsonProperty("JMD")]
        public double JMD { get; set; }

        [JsonProperty("JOD")]
        public double JOD { get; set; }

        [JsonProperty("JPY")]
        public double JPY { get; set; }

        [JsonProperty("KES")]
        public double KES { get; set; }

        [JsonProperty("KGS")]
        public double KGS { get; set; }

        [JsonProperty("KHR")]
        public double KHR { get; set; }

        [JsonProperty("KMF")]
        public double KMF { get; set; }

        [JsonProperty("KPW")]
        public double KPW { get; set; }

        [JsonProperty("KRW")]
        public double KRW { get; set; }

        [JsonProperty("KWD")]
        public double KWD { get; set; }

        [JsonProperty("KYD")]
        public double KYD { get; set; }

        [JsonProperty("KZT")]
        public double KZT { get; set; }

        [JsonProperty("LAK")]
        public double LAK { get; set; }

        [JsonProperty("LBP")]
        public double LBP { get; set; }

        [JsonProperty("LKR")]
        public double LKR { get; set; }

        [JsonProperty("LRD")]
        public double LRD { get; set; }

        [JsonProperty("LSL")]
        public double LSL { get; set; }

        [JsonProperty("LTL")]
        public double LTL { get; set; }

        [JsonProperty("LVL")]
        public double LVL { get; set; }

        [JsonProperty("LYD")]
        public double LYD { get; set; }

        [JsonProperty("MAD")]
        public double MAD { get; set; }

        [JsonProperty("MDL")]
        public double MDL { get; set; }

        [JsonProperty("MGA")]
        public double MGA { get; set; }

        [JsonProperty("MKD")]
        public double MKD { get; set; }

        [JsonProperty("MMK")]
        public double MMK { get; set; }

        [JsonProperty("MNT")]
        public double MNT { get; set; }

        [JsonProperty("MOP")]
        public double MOP { get; set; }

        [JsonProperty("MRO")]
        public double MRO { get; set; }

        [JsonProperty("MUR")]
        public double MUR { get; set; }

        [JsonProperty("MVR")]
        public double MVR { get; set; }

        [JsonProperty("MWK")]
        public double MWK { get; set; }

        [JsonProperty("MXN")]
        public double MXN { get; set; }

        [JsonProperty("MYR")]
        public double MYR { get; set; }

        [JsonProperty("MZN")]
        public double MZN { get; set; }

        [JsonProperty("NAD")]
        public double NAD { get; set; }

        [JsonProperty("NGN")]
        public double NGN { get; set; }

        [JsonProperty("NIO")]
        public double NIO { get; set; }

        [JsonProperty("NOK")]
        public double NOK { get; set; }

        [JsonProperty("NPR")]
        public double NPR { get; set; }

        [JsonProperty("NZD")]
        public double NZD { get; set; }

        [JsonProperty("OMR")]
        public double OMR { get; set; }

        [JsonProperty("PAB")]
        public double PAB { get; set; }

        [JsonProperty("PEN")]
        public double PEN { get; set; }

        [JsonProperty("PGK")]
        public double PGK { get; set; }

        [JsonProperty("PHP")]
        public double PHP { get; set; }

        [JsonProperty("PKR")]
        public double PKR { get; set; }

        [JsonProperty("PLN")]
        public double PLN { get; set; }

        [JsonProperty("PYG")]
        public double PYG { get; set; }

        [JsonProperty("QAR")]
        public double QAR { get; set; }

        [JsonProperty("RON")]
        public double RON { get; set; }

        [JsonProperty("RSD")]
        public double RSD { get; set; }

        [JsonProperty("RUB")]
        public double RUB { get; set; }

        [JsonProperty("RWF")]
        public double RWF { get; set; }

        [JsonProperty("SAR")]
        public double SAR { get; set; }

        [JsonProperty("SBD")]
        public double SBD { get; set; }

        [JsonProperty("SCR")]
        public double SCR { get; set; }

        [JsonProperty("SDG")]
        public double SDG { get; set; }

        [JsonProperty("SEK")]
        public double SEK { get; set; }

        [JsonProperty("SGD")]
        public double SGD { get; set; }

        [JsonProperty("SHP")]
        public double SHP { get; set; }

        [JsonProperty("SLL")]
        public double SLL { get; set; }

        [JsonProperty("SOS")]
        public double SOS { get; set; }

        [JsonProperty("SRD")]
        public double SRD { get; set; }

        [JsonProperty("STD")]
        public double STD { get; set; }

        [JsonProperty("SVC")]
        public double SVC { get; set; }

        [JsonProperty("SYP")]
        public double SYP { get; set; }

        [JsonProperty("SZL")]
        public double SZL { get; set; }

        [JsonProperty("THB")]
        public double THB { get; set; }

        [JsonProperty("TJS")]
        public double TJS { get; set; }

        [JsonProperty("TMT")]
        public double TMT { get; set; }

        [JsonProperty("TND")]
        public double TND { get; set; }

        [JsonProperty("TOP")]
        public double TOP { get; set; }

        [JsonProperty("TRY")]
        public double TRY { get; set; }

        [JsonProperty("TTD")]
        public double TTD { get; set; }

        [JsonProperty("TWD")]
        public double TWD { get; set; }

        [JsonProperty("TZS")]
        public double TZS { get; set; }

        [JsonProperty("UAH")]
        public double UAH { get; set; }

        [JsonProperty("UGX")]
        public double UGX { get; set; }

        [JsonProperty("USD")]
        public double USD { get; set; }

        [JsonProperty("UYU")]
        public double UYU { get; set; }

        [JsonProperty("UZS")]
        public double UZS { get; set; }

        [JsonProperty("VEF")]
        public double VEF { get; set; }

        [JsonProperty("VND")]
        public double VND { get; set; }

        [JsonProperty("VUV")]
        public double VUV { get; set; }

        [JsonProperty("WST")]
        public double WST { get; set; }

        [JsonProperty("XAF")]
        public double XAF { get; set; }

        [JsonProperty("XAG")]
        public double XAG { get; set; }

        [JsonProperty("XAU")]
        public double XAU { get; set; }

        [JsonProperty("XCD")]
        public double XCD { get; set; }

        [JsonProperty("XDR")]
        public double XDR { get; set; }

        [JsonProperty("XOF")]
        public double XOF { get; set; }

        [JsonProperty("XPF")]
        public double XPF { get; set; }

        [JsonProperty("YER")]
        public double YER { get; set; }

        [JsonProperty("ZAR")]
        public double ZAR { get; set; }

        [JsonProperty("ZMK")]
        public double ZMK { get; set; }

        [JsonProperty("ZMW")]
        public double ZMW { get; set; }

        [JsonProperty("ZWL")]
        public double ZWL { get; set; }

         
        double[] CurrencyItems;
        public Rates(double Rates)
        {
            CurrencyItems = new double []
                { EUR,
                  GBP,
   //               Usd
                }
                ;
        }
        
        public SymbolEnumerator GetEnumerator()
        {
            return new SymbolEnumerator(this);
        }

        // Declare the enumerator class:  
        public class SymbolEnumerator
        {
            int nIndex;
            Rates collection;
            public SymbolEnumerator(Rates coll)
            {
                collection = coll;
                nIndex = -1;
            }
            public bool MoveNext()
            {
                nIndex++;
                return (nIndex < collection.CurrencyItems.Length);
            }

            public double Current => collection.CurrencyItems[nIndex];

        }

}


public partial class CurrencyRepository
    {
        public static CurrencyRepository FromJson(string json) => JsonConvert.DeserializeObject<CurrencyRepository>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this CurrencyRepository self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

}

