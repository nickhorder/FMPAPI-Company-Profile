using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMPAPI
{
    public class APIReturnHandling
    {
        public static double ProfileRepoHandling(JSONRepository obj, 
                                                 string ProfileCurrency,
                                                 double GBPEUR, 
                                                 double GBPUSD, 
                                                 double GBPJPY,
                                                 double GBPSEK,
                                                 double GBPNOK,
                                                 double GBPDKK,
                                                 double GBPAUD,
                                                 double GBPCAD,
                                                 out double MarketCapConv)           
        {
            double MarketCapNullTest = obj.MarketCap ?? -9999; //returns -9999 if null, otherwise the value found
           
            MarketCapConv = ProfileCurrency switch
                {
                    "GBp" => Math.Round((MarketCapNullTest) / 1e6, 2),
                    "EUR" => Math.Round((MarketCapNullTest / GBPEUR) / 1e6, 2),
                    "USD" => Math.Round((MarketCapNullTest / GBPUSD) / 1e6, 2),
                    "JPY" => Math.Round((MarketCapNullTest / GBPJPY) / 1e6, 2),
                    "SEK" => Math.Round((MarketCapNullTest / GBPSEK) / 1e6, 2),
                    "NOK" => Math.Round((MarketCapNullTest / GBPNOK) / 1e6, 2),
                    "DKK" => Math.Round((MarketCapNullTest / GBPDKK) / 1e6, 2),
                    "AUD" => Math.Round((MarketCapNullTest / GBPAUD) / 1e6, 2),
                    "CAD" => Math.Round((MarketCapNullTest / GBPCAD) / 1e6, 2),
                    _ => -999
                };
            return MarketCapConv; 
        }


            //  
        public static string IncomeRepoHandling(JSONRepository obj, out string IncomeFilingDate2)

        {
            var IncomeSymbolsList = new List<string>();
            IncomeSymbolsList.Add(obj.Symbol);

            var IncomeFilingsList = new List<string>();
            IncomeFilingsList.Add(obj.FilingDate);
            
            string[] IncomeFilingsListArray = IncomeFilingsList.ToArray();            
            IncomeFilingDate2 = IncomeFilingsListArray.FirstOrDefault();
 
            return IncomeFilingDate2;
            
        }
   
    
    
    
    
    }

}

