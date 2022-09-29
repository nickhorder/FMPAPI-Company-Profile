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

 



        public static double IncomeConvert(string RecentCurrencyFiling,
                                           double RecentOpProfit,
                                           double GBPEUR,
                                           double GBPUSD,
                                           double GBPJPY,
                                           double GBPSEK, 
                                           double GBPNOK, 
                                           double GBPDKK,
                                           double GBPAUD,
                                           double GBPCAD,
                                           out double IncomeConverted)
        {
            IncomeConverted = RecentCurrencyFiling switch
            {
                "GBP" => Math.Round((RecentOpProfit) / 1e6, 2),
                "EUR" => Math.Round((RecentOpProfit / GBPEUR) / 1e6, 2),
                "USD" => Math.Round((RecentOpProfit / GBPUSD) / 1e6, 2),
                "JPY" => Math.Round((RecentOpProfit / GBPJPY) / 1e6, 2),
                "SEK" => Math.Round((RecentOpProfit / GBPSEK) / 1e6, 2),
                "NOK" => Math.Round((RecentOpProfit / GBPNOK) / 1e6, 2),
                "DKK" => Math.Round((RecentOpProfit / GBPDKK) / 1e6, 2),
                "AUD" => Math.Round((RecentOpProfit / GBPAUD) / 1e6, 2),
                "CAD" => Math.Round((RecentOpProfit / GBPCAD) / 1e6, 2),
                _ => -999
            };
           
            return (IncomeConverted);
        }



        public static double CurrentAssetConvert(string BalanceCurrency,
                                                 double CurrentAssets,
                                                 double GBPEUR,
                                                 double GBPUSD,
                                                 double GBPJPY,
                                                 double GBPSEK,
                                                 double GBPNOK,
                                                 double GBPDKK,
                                                 double GBPAUD,
                                                 double GBPCAD,
                                                 out double CurrentAssetsConverted)
        {
            CurrentAssetsConverted = BalanceCurrency switch
            {
                "GBP" => Math.Round((CurrentAssets) / 1e6, 2),
                "EUR" => Math.Round((CurrentAssets / GBPEUR) / 1e6, 2),
                "USD" => Math.Round((CurrentAssets / GBPUSD) / 1e6, 2),
                "JPY" => Math.Round((CurrentAssets / GBPJPY) / 1e6, 2),
                "SEK" => Math.Round((CurrentAssets / GBPSEK) / 1e6, 2),
                "NOK" => Math.Round((CurrentAssets / GBPNOK) / 1e6, 2),
                "DKK" => Math.Round((CurrentAssets / GBPDKK) / 1e6, 2),
                "AUD" => Math.Round((CurrentAssets / GBPAUD) / 1e6, 2),
                "CAD" => Math.Round((CurrentAssets / GBPCAD) / 1e6, 2),
                _ => -999
            };
            return (CurrentAssetsConverted);
        }


        public static double CurrentLiabilityConvert(string BalanceCurrency,
                                                     double CurrentLiabilities,
                                                     double GBPEUR,
                                                     double GBPUSD,
                                                     double GBPJPY,
                                                     double GBPSEK,
                                                     double GBPNOK,
                                                     double GBPDKK,
                                                     double GBPAUD,
                                                     double GBPCAD,
                                                     out double CurrentLiabilitiesConverted)
        {
            CurrentLiabilitiesConverted = BalanceCurrency switch
            {
                "GBP" => Math.Round((CurrentLiabilities) / 1e6, 2),
                "EUR" => Math.Round((CurrentLiabilities / GBPEUR) / 1e6, 2),
                "USD" => Math.Round((CurrentLiabilities / GBPUSD) / 1e6, 2),
                "JPY" => Math.Round((CurrentLiabilities / GBPJPY) / 1e6, 2),
                "SEK" => Math.Round((CurrentLiabilities / GBPSEK) / 1e6, 2),
                "NOK" => Math.Round((CurrentLiabilities / GBPNOK) / 1e6, 2),
                "DKK" => Math.Round((CurrentLiabilities / GBPDKK) / 1e6, 2),
                "AUD" => Math.Round((CurrentLiabilities / GBPAUD) / 1e6, 2),
                "CAD" => Math.Round((CurrentLiabilities / GBPCAD) / 1e6, 2),
                _ => -999
            };
            return (CurrentLiabilitiesConverted);
        }


        public static double NetFixedAssetsConvert(string BalanceCurrency,
                                                   double NetFixedAssets,
                                                   double GBPEUR,
                                                   double GBPUSD,
                                                   double GBPJPY,
                                                   double GBPSEK,
                                                   double GBPNOK,
                                                   double GBPDKK,
                                                   double GBPAUD,
                                                   double GBPCAD,
                                                   out double NetFixedAssetsConverted)
        {
            NetFixedAssetsConverted = BalanceCurrency switch
            {
                "GBP" => Math.Round((NetFixedAssets) / 1e6, 2),
                "EUR" => Math.Round((NetFixedAssets / GBPEUR) / 1e6, 2),
                "USD" => Math.Round((NetFixedAssets / GBPUSD) / 1e6, 2),
                "JPY" => Math.Round((NetFixedAssets / GBPJPY) / 1e6, 2),
                "SEK" => Math.Round((NetFixedAssets / GBPSEK) / 1e6, 2),
                "NOK" => Math.Round((NetFixedAssets / GBPNOK) / 1e6, 2),
                "DKK" => Math.Round((NetFixedAssets / GBPDKK) / 1e6, 2),
                "AUD" => Math.Round((NetFixedAssets / GBPAUD) / 1e6, 2),
                "CAD" => Math.Round((NetFixedAssets / GBPCAD) / 1e6, 2),
                _ => -999
            };
            return (NetFixedAssetsConverted);
        }


        public static double CashConvert(string BalanceCurrency,
                                         double TotalCash,
                                         double GBPEUR,
                                         double GBPUSD,
                                         double GBPJPY,
                                         double GBPSEK,
                                         double GBPNOK,
                                         double GBPDKK,
                                         double GBPAUD,
                                         double GBPCAD,
                                         out double CashConverted)
        {

            CashConverted = BalanceCurrency switch
            {
                "GBP" => Math.Round((TotalCash) / 1e6, 2),
                "EUR" => Math.Round((TotalCash / GBPEUR) / 1e6, 2),
                "USD" => Math.Round((TotalCash / GBPUSD) / 1e6, 2),
                "JPY" => Math.Round((TotalCash / GBPJPY) / 1e6, 2),
                "SEK" => Math.Round((TotalCash / GBPSEK) / 1e6, 2),
                "NOK" => Math.Round((TotalCash / GBPNOK) / 1e6, 2),
                "DKK" => Math.Round((TotalCash / GBPDKK) / 1e6, 2),
                "AUD" => Math.Round((TotalCash / GBPAUD) / 1e6, 2),
                "CAD" => Math.Round((TotalCash / GBPCAD) / 1e6, 2),
                _ => -999
            };
            return (CashConverted);
        }


        public static double DebtConvert(string BalanceCurrency,
                                         double TotalDebt,
                                         double GBPEUR,
                                         double GBPUSD,
                                         double GBPJPY,
                                         double GBPSEK,
                                         double GBPNOK,
                                         double GBPDKK,
                                         double GBPAUD,
                                         double GBPCAD,
                                         out double DebtConverted)
        {
            DebtConverted = BalanceCurrency switch
            {
                "GBP" => Math.Round((TotalDebt) / 1e6, 2),
                "EUR" => Math.Round((TotalDebt / GBPEUR) / 1e6, 2),
                "USD" => Math.Round((TotalDebt / GBPUSD) / 1e6, 2),
                "JPY" => Math.Round((TotalDebt / GBPJPY) / 1e6, 2),
                "SEK" => Math.Round((TotalDebt / GBPSEK) / 1e6, 2),
                "NOK" => Math.Round((TotalDebt / GBPNOK) / 1e6, 2),
                "DKK" => Math.Round((TotalDebt / GBPDKK) / 1e6, 2),
                "AUD" => Math.Round((TotalDebt / GBPAUD) / 1e6, 2),
                "CAD" => Math.Round((TotalDebt / GBPCAD) / 1e6, 2),
                _ => -999
            };
            return (DebtConverted);
        }
    }
}

