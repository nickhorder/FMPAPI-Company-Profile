using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMPAPI
{
    public class CurrencyConversion

    // A collection of Methods that convert values from Profile/Income/Balance Sheet APIs, back to GBP where needed.
    {

        public static double MarketCapConvert(string returnProfCurr, 
                                              double returnMarketCap, 
                                              double GBPEUR,
                                              double GBPUSD,
                                              double GBPJPY,
                                              out double MarketCapConv)
        {            
            MarketCapConv = returnProfCurr switch
            {
                "GBp" => Math.Round((returnMarketCap) / 1e6, 2),
                "EUR" => Math.Round((returnMarketCap / GBPEUR) / 1e6, 2),
                "USD" => Math.Round((returnMarketCap / GBPUSD) / 1e6, 2),
                "JPY" => Math.Round((returnMarketCap / GBPJPY) / 1e6, 2),
            _ => 0
            };
            return (MarketCapConv);
        }



        public static double IncomeConvert(string returnIncomeCurrency,
                                           double returnIncome,
                                           double GBPEUR,
                                           double GBPUSD,
                                           double GBPJPY,
                                           out double IncomeConverted)
        {
            IncomeConverted = returnIncomeCurrency switch
            {
                "GBp" => Math.Round((returnIncome) / 1e6, 2),
                "EUR" => Math.Round((returnIncome / GBPEUR) / 1e6, 2),
                "USD" => Math.Round((returnIncome / GBPUSD) / 1e6, 2),
                "JPY" => Math.Round((returnIncome / GBPJPY) / 1e6, 2),
                _ => 0
            };
            return (IncomeConverted);
        }



        public static double CurrentAssetConvert(string returnBalanceCurrency,
                                                 double returnCurrAss,
                                                 double GBPEUR,
                                                 double GBPUSD,
                                                 double GBPJPY,
                                                 out double CurrentAssetConverted)
        {
            CurrentAssetConverted = returnBalanceCurrency switch
            {
                "GBp" => Math.Round((returnCurrAss) / 1e6, 2),
                "EUR" => Math.Round((returnCurrAss / GBPEUR) / 1e6, 2),
                "USD" => Math.Round((returnCurrAss / GBPUSD) / 1e6, 2),
                "JPY" => Math.Round((returnCurrAss / GBPJPY) / 1e6, 2),
                _ => 0
            };
            return (CurrentAssetConverted);
        }


        public static double CurrentLiabilityConvert(string returnBalanceCurrency,
                                                     double returnCurrLia,
                                                     double GBPEUR,
                                                     double GBPUSD,
                                                     double GBPJPY,
                                                     out double CurrentLiabilitiesConverted)
        {
            CurrentLiabilitiesConverted = returnBalanceCurrency switch
            {
                "GBp" => Math.Round((returnCurrLia) / 1e6, 2),
                "EUR" => Math.Round((returnCurrLia / GBPEUR) / 1e6, 2),
                "USD" => Math.Round((returnCurrLia / GBPUSD) / 1e6, 2),
                "JPY" => Math.Round((returnCurrLia / GBPJPY) / 1e6, 2),
                _ => 0
            };
            return (CurrentLiabilitiesConverted);
        }


        public static double NetFixedAssetsConvert(string returnBalanceCurrency,
                                                   double returnNetFixAss,
                                                   double GBPEUR,
                                                   double GBPUSD,
                                                   double GBPJPY,
                                                   out double NetFixedAssetsConverted)
        {
            NetFixedAssetsConverted = returnBalanceCurrency switch
            {
                "GBp" => Math.Round((returnNetFixAss) / 1e6, 2),
                "EUR" => Math.Round((returnNetFixAss / GBPEUR) / 1e6, 2),
                "USD" => Math.Round((returnNetFixAss / GBPUSD) / 1e6, 2),
                "JPY" => Math.Round((returnNetFixAss / GBPJPY) / 1e6, 2),
                _ => 0
            };
            return (NetFixedAssetsConverted);
        }


        public static double CashConvert(string returnBalanceCurrency,
                                         double returnCash,
                                         double GBPEUR,
                                         double GBPUSD,
                                         double GBPJPY,
                                         out double CashConverted)
        {

            CashConverted = returnBalanceCurrency switch
            {
                "GBp" => Math.Round((returnCash) / 1e6, 2),
                "EUR" => Math.Round((returnCash / GBPEUR) / 1e6, 2),
                "USD" => Math.Round((returnCash / GBPUSD) / 1e6, 2),
                "JPY" => Math.Round((returnCash / GBPJPY) / 1e6, 2),
                _ => 0
            };
            return (CashConverted);
        }


        public static double DebtConvert(string returnBalanceCurrency,
                                         double returnDebt,
                                         double GBPEUR,
                                         double GBPUSD,
                                         double GBPJPY,
                                         out double DebtConverted)
        {
            DebtConverted = returnBalanceCurrency switch
            {
                "GBp" => Math.Round((returnDebt) / 1e6, 2),
                "EUR" => Math.Round((returnDebt / GBPEUR) / 1e6, 2),
                "USD" => Math.Round((returnDebt / GBPUSD) / 1e6, 2),
                "JPY" => Math.Round((returnDebt / GBPJPY) / 1e6, 2),
                _ => 0
            };
            return (DebtConverted);
        }
    }
}

