using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace FMPAPI
{
    public class FMPAPIProgram

    /*
         FMPAPICompanyProfile

         This C# program calls the Financial Modeling Prep API asynchronously with API Key and the relevant  
         company ticker symbol (instrument code - i.e EXPN.L). It retrieves financial data (income statement/
         balance sheet). It calculates return on net assets and earnings/enterprise value, to be ranked within
         the output CSV, to identify companies that are efficiently profitable but undervalued by the market.
         Because the API holds all-time financial data, there is lots of FirstOrDefault processing done on the 
         data returned from each endpoint, to ensure we take only the most recent income statement/balance sheet
         records.
          
    */

    {
        public static async Task Main(string[] args)
        {
            string CurrencyListEndPoint = "http://api.exchangeratesapi.io/v1/latest?";
            string CurrencyAPIKey = "redact";
            string StockListEndPoint = "https://financialmodelingprep.com/api/v3/stock/list?";
            string ProfileEndPoint = "https://financialmodelingprep.com/api/v3/profile/";
            string BalanceEndPoint = "https://financialmodelingprep.com/api/v3/balance-sheet-statement/";
            string IncomeEndPoint = "https://financialmodelingprep.com/api/v3/income-statement/";
            string APIKey = "apikey=redact";
            string QuestionM = "?";

            List<APIListRepository> IncomeList = new List<APIListRepository>();
            List<APIListRepository> ProfileList = new List<APIListRepository>();
            List<APIListRepository> BalanceSheetList = new List<APIListRepository>();

            //Call CurrencyAPI to get exchange rates (to EUR)

            string CurrencyURI = CurrencyListEndPoint + CurrencyAPIKey;
            Console.WriteLine(CurrencyURI);
            var currencyrepos = await CallCurrencyAPI.CurrencyAPI(CurrencyURI);
            double GBPEUR = (currencyrepos.Rates.EUR / currencyrepos.Rates.GBP);
            double GBPUSD = (currencyrepos.Rates.USD / currencyrepos.Rates.GBP);
            double GBPJPY = (currencyrepos.Rates.JPY / currencyrepos.Rates.GBP);
            double GBPSEK = (currencyrepos.Rates.SEK / currencyrepos.Rates.GBP);
            double GBPNOK = (currencyrepos.Rates.NOK / currencyrepos.Rates.GBP);
            double GBPDKK = (currencyrepos.Rates.DKK / currencyrepos.Rates.GBP);
            double GBPAUD = (currencyrepos.Rates.AUD / currencyrepos.Rates.GBP);
            double GBPCAD = (currencyrepos.Rates.CAD / currencyrepos.Rates.GBP);

            //Call StockListAPI to get available Symbols

            string StockListURI = StockListEndPoint + APIKey;
            var stocklistrepos = await CallStockListAPI.StockListAPI(StockListURI);

            //Create list of companies from only selected indexes
 
             List<string> CompaniesToSearchFiltering = new List<string>();
             foreach (var ddd in stocklistrepos)
                      if (ddd.Type == "stock")
                            {
                                {
                                    var rt = new StockListRepository();
                                    rt.ExchangeShortName = ddd.ExchangeShortName;
                                    rt.Symbol = ddd.Symbol;
                                    rt.SecurityName = ddd.SecurityName.Replace(",", " ");
                                    rt.Exchange = ddd.Exchange;
                                    rt.Type = ddd.Type;

                                    Exchanges.ExchangeMethod(rt, out string SelectedCompanies);
                                    CompaniesToSearchFiltering.Add(SelectedCompanies);
                                    CompaniesToSearchFiltering.Remove("Other Exchange");                      
                        //    Console.WriteLine(SelectedSymbols);
                                }
                            }
                            List<string> FinalCompanyList = CompaniesToSearchFiltering.Distinct().ToList();

                        double CompaniesCount = FinalCompanyList.Where(SelectedCompanies => SelectedCompanies != null).Count();
                        Console.WriteLine($"The number of companies that will be reviewed is: " + CompaniesCount);
                        foreach (string Companies in FinalCompanyList)
                        {

                        string profilefilepath2 = @"C:\Users\X1 Carbon\OneDrive\Documents\Investments\Equities\Magic Formula\Magic Formula 2022\SymbolList.csv";
                        using (StreamWriter file = new StreamWriter(profilefilepath2, true))
                            {
                                file.WriteLine(Companies);
                            }
                        }
  /*
            //Single call section - start
            string Ticker1 = "AFK.OL";
            string Ticker2 = "WIRTEK.CO";
            string Ticker3 = "TOBII.ST";
        //    string Ticker4 = "2GB.DE";

            List<string> SingleSymbolList = new List<string>();
            //"MSGN";    //Not actively trading
            // "ALUPG.PA"; //MarketCap of only 10m
            //"OGN"; //Not enough history to create income array
            //"0A05.L"; //Reports in CHF, so MarketCapConv will be default and less than 50
            //"ALMIL.PA"; // Loss maker
            //"JD"; // Balance in CNY (Profile must be in USD?) 
            SingleSymbolList.Add(Ticker1);
            SingleSymbolList.Add(Ticker2);
            SingleSymbolList.Add(Ticker3);
       //    SingleSymbolList.Add(Ticker4);
*/ 

             //////////////////////////////////////////////  Call ProfileAPI //////////////////////////////////////////////

             List<string> ProfileAPISymbols = new List<string>();
     //        foreach (string Ticker in SingleSymbolList)

             foreach (string Ticker in CompaniesToSearchFiltering)
            {
                string ProfileURI = ProfileEndPoint + Ticker + QuestionM + APIKey;
                var ProfileAPIReturn = await CallProfileAPI.ProfileAPI(ProfileURI);
                Console.WriteLine($"Calling: " + ProfileURI);

                foreach (var aaa in ProfileAPIReturn)
                {
                    var rt = new JSONRepository();
                    rt.Symbol = aaa.Symbol;
                    rt.Exchange = aaa.Exchange;
                    rt.CompanyName = aaa.CompanyName;
                    rt.Currency = aaa.Currency;
                    rt.MarketCap = aaa.MarketCap;
                    string ProfileCurrency = aaa.Currency;

                    APIReturnHandling.ProfileRepoHandling(rt, ProfileCurrency, 
                    GBPEUR, GBPUSD, GBPJPY, GBPSEK, GBPNOK, GBPDKK, GBPAUD, GBPCAD,
                    out double MarketCapConv);

                    if (MarketCapConv > 50 && aaa.IsActivelyTrading == true)
                    {
                        ProfileList.Add(new APIListRepository()
                        {
                            ProfileSymbol = aaa.Symbol,
                            Exchange = aaa.Exchange,
                            CompanyName = aaa.CompanyName.Replace(",", " "),
                            MarketCap = MarketCapConv,
                            ProfileCurrency = aaa.Currency
                            
                    });
                  //      Console.WriteLine(aaa.Exchange);
                        ProfileAPISymbols.Add(aaa.Symbol);
                        double CompletedProfileAPICalls = ProfileAPISymbols.Where(ProfileSymbol => ProfileSymbol != null).Count();
                        double Percent = Math.Round(CompletedProfileAPICalls / (CompaniesCount) * 100, 2);
         
                        Console.WriteLine("Total Progression in ProfileAPI calls: " + CompletedProfileAPICalls + "/" + CompaniesCount + " " + Percent + "%");
                    }

                }

            }
            var SymbolCount = ProfileList.Where(ProfileSymbol => ProfileSymbol != null).Count();
            Console.WriteLine("List Count After ProfileAPI: " + SymbolCount);
            foreach (var item in ProfileList)
            {

                string profilefilepath2 = @"C:\Users\X1 Carbon\OneDrive\Documents\Investments\Equities\Magic Formula\Magic Formula 2022\SymbolsSearched.csv";
                using (StreamWriter file = new StreamWriter(profilefilepath2, true))
                {
                    file.WriteLine($"Profile Symbol searched in API at: {DateTime.Now}" + "," + item.ProfileSymbol);
                }
            }



            //////////////////////////////////////////////  Call IncomeAPI  /////////////////////////////////////////////////////

            List<string> IncomeAPISymbols = new List<string>();

            //  foreach (var ProfileSymbol in ProfileAPISymbols)

            foreach (var item in ProfileList)
            {
                string IncomeURI = IncomeEndPoint + item.ProfileSymbol + QuestionM + APIKey;
                var IncomeAPIReturn = await CallIncomeAPI.IncomeAPI(IncomeURI);
                Console.WriteLine($"Calling: " + IncomeURI);

                var IncomeSymbolList = new List<string>();
                var IncomeFilingList = new List<string>();
                var IncomeCurrencyList = new List<string>();
                var IncomeOpProfitList = new List<double>();
                var IncomeRevenueList = new List<double>();

                foreach (var ddd in IncomeAPIReturn)
                {
                    IncomeSymbolList.Add(ddd.Symbol);
                    IncomeFilingList.Add(ddd.FilingDate);
                    IncomeCurrencyList.Add(ddd.ReportedCurrency);
                    IncomeOpProfitList.Add(ddd.OperatingProfit);
                    IncomeRevenueList.Add(ddd.Revenue);
                }
                string[] IncomeSymbolArray = IncomeSymbolList.ToArray();
                string[] IncomeFilingArray = IncomeFilingList.ToArray();
                string[] IncomeCurrencyArray = IncomeCurrencyList.ToArray();
                double[] IncomeOpProfitArray = IncomeOpProfitList.ToArray();
                double[] IncomeRevenueArray = IncomeRevenueList.ToArray();
                string RecentIncomeSymbol = IncomeSymbolArray.FirstOrDefault();
                string RecentIncomeFiling = IncomeFilingArray.FirstOrDefault();
                string RecentCurrencyFiling = IncomeCurrencyArray.FirstOrDefault();
                double RecentOpProfit = IncomeOpProfitArray.FirstOrDefault();
                double RecentRevenue = IncomeRevenueArray.FirstOrDefault();

                // Convert Operating Profit to GBP
                CurrencyConversion.IncomeConvert(RecentCurrencyFiling, RecentOpProfit,
                GBPEUR, GBPUSD, GBPJPY, GBPSEK, GBPNOK, GBPDKK, GBPAUD, GBPCAD,
                out double IncomeConverted);


                // Scoring Calculations - Additional insight over and above the usual RONA / EV/EBITDA metrics

                if (IncomeOpProfitArray.Length > 4 && RecentOpProfit > 0)
                {
                    ScoringCalculations.ProfitVariation(IncomeOpProfitArray, RecentOpProfit, out double ProfVariation);
                    ScoringCalculations.ProfitVariationScore(ProfVariation, out double ProfitVariationScore);
                    ScoringCalculations.RevenueScore(IncomeRevenueArray, out double RecentRevenueScore, out double[] RecentRevenueArray);
                    ScoringCalculations.ProfitScore(IncomeOpProfitArray, out double RecentProfitScore, out double[] RecentProfitArray);
                    ScoringCalculations.ProfitableYearsScore(RecentProfitArray, out double ProfitableYearsScore);
                    ScoringCalculations.NetRevenueChangeScore(RecentRevenueArray, out double NetRevenueChangeScore);
                    ScoringCalculations.NetProfitChangeScore(RecentProfitArray, out double NetProfitChangeScore);
                    //TOTAL SCORE
                    double TotalScore = (ProfitVariationScore + RecentRevenueScore + RecentProfitScore +
                                         ProfitableYearsScore + NetRevenueChangeScore + NetProfitChangeScore);


                    // Add Income API outputs to List

                    if (ProfitVariationScore >= 4)

                        IncomeList.Add(new APIListRepository()
                        {
                            IncomeSymbol = RecentIncomeSymbol,
                            IncomeDate = RecentIncomeFiling,
                            IncomeCurrency = RecentCurrencyFiling,
                            Income = IncomeConverted,
                            Revenue = RecentRevenue,
                            ProfitVariation = ProfVariation,
                            ProfitVariationScore = ProfitVariationScore,
                            RecentRevenueScore = RecentRevenueScore,
                            RecentProfitScore = RecentProfitScore,
                            ProfitableYearsScore = ProfitableYearsScore,
                            NetRevenueChangeScore = NetRevenueChangeScore,
                            NetProfitChangeScore = NetProfitChangeScore,
                            TotalScore = TotalScore
                        });
                    IncomeAPISymbols.Add(RecentIncomeSymbol);
                    double CompletedProfileAPICalls = ProfileAPISymbols.Where(ProfileSymbol => ProfileSymbol != null).Count();
                    double CompletedIncomeAPICalls = IncomeAPISymbols.Where(IncomeSymbol => IncomeSymbol != null).Count();
                    double Percent = Math.Round(CompletedIncomeAPICalls / CompletedProfileAPICalls * 100, 2);

                    Console.WriteLine("Total Progression in IncomeAPI calls: " + CompletedIncomeAPICalls + "/" + CompletedProfileAPICalls + " " + Percent + "%");

                }
            }

            //Count for report of Income Symbols searched

            var IncomeCount = IncomeList.Where(IncomeSymbol => IncomeSymbol != null).Count();
            Console.WriteLine("List Count After IncomeAPI: " + IncomeCount);

            //Write Income Symbols searched to CSV

            foreach (var item in IncomeList)
            {

                string profilefilepath2 = @"C:\Users\X1 Carbon\OneDrive\Documents\Investments\Equities\Magic Formula\Magic Formula 2022\SymbolsSearched.csv";
                using (StreamWriter file = new StreamWriter(profilefilepath2, true))
                {
                    file.WriteLine($"Income Symbol searched in API at: {DateTime.Now}" + "," + item.IncomeSymbol);
                }
            }


            //////////////////////////////////////////////  Call BalanceAPI  //////////////////////////////////////////////

            List<string> BalanceAPISymbols = new List<string>();
            foreach (var IncomeSymbol in IncomeAPISymbols)
            {
                string BalanceURI = BalanceEndPoint + IncomeSymbol + QuestionM + APIKey;
                var balancerepos = await CallBalanceAPI.BalanceAPI(BalanceURI);
                Console.WriteLine($"Calling: " + BalanceURI);

                var BalanceSymbolList = new List<string>();
                var BalDateList = new List<string>();
                var BalanceCurrencyList = new List<string>();
                var BalancePeriodList = new List<string>();
                var BalCurrLiaList = new List<double>();
                var BalCurrAssList = new List<double>();
                var BalNetFixAssList = new List<double>();
                var BalCashList = new List<double>();
                var BalDebtList = new List<double>();

                foreach (var ddd in balancerepos)
                {
                    if (ddd.FilingDate != null)
                        BalanceSymbolList.Add(ddd.Symbol);
                    BalDateList.Add(ddd.FilingDate);
                    BalanceCurrencyList.Add(ddd.ReportedCurrency);
                    BalancePeriodList.Add(ddd.Period);
                    BalCurrLiaList.Add(ddd.CurrentLiabilities);
                    BalCurrAssList.Add(ddd.CurrentAssets);
                    BalNetFixAssList.Add(ddd.NetFixedAssets);
                    BalCashList.Add(ddd.Cash);
                    BalDebtList.Add(ddd.TotalDebt);
                }

                string[] BalSymbolArray = BalanceSymbolList.ToArray();
                string RecentBalanceSymbol = BalSymbolArray.FirstOrDefault();
                string[] DateArray = BalDateList.ToArray();
                string BalanceSheetDate = DateArray.FirstOrDefault();
                string[] BalanceCurrencyArray = BalanceCurrencyList.ToArray();
                string BalanceCurrency = BalanceCurrencyArray.FirstOrDefault();
                string[] BalancePeriodArray = BalancePeriodList.ToArray();
                string BalancePeriod = BalancePeriodArray.FirstOrDefault() + "(" + BalanceCurrency + ")";
                double[] CurrLiaArray = BalCurrLiaList.ToArray();
                double CurrentLiabilities = CurrLiaArray.FirstOrDefault();
                double[] CurrAssArray = BalCurrAssList.ToArray();
                double CurrentAssets = (CurrAssArray.FirstOrDefault());
                double[] NetFixAssArray = BalNetFixAssList.ToArray();
                double NetFixedAssets = (NetFixAssArray.FirstOrDefault());
                double[] CashArray = BalCashList.ToArray();
                double TotalCash = (CashArray.FirstOrDefault());
                double[] DebtArray = BalDebtList.ToArray();
                double TotalDebt = (DebtArray.FirstOrDefault());

                // Convert Current Assets to GBP  
                CurrencyConversion.CurrentAssetConvert(BalanceCurrency, CurrentAssets, 
                GBPEUR, GBPUSD, GBPJPY, GBPSEK, GBPNOK, GBPDKK, GBPAUD, GBPCAD,
                out double CurrentAssetsConverted);
                // Convert Current Liabilities to GBP  
                CurrencyConversion.CurrentLiabilityConvert(BalanceCurrency, CurrentLiabilities, 
                GBPEUR, GBPUSD, GBPJPY, GBPSEK, GBPNOK, GBPDKK, GBPAUD, GBPCAD,
                out double CurrentLiabilitiesConverted);
                // Convert Net Fixed Assets to GBP  
                CurrencyConversion.NetFixedAssetsConvert(BalanceCurrency, NetFixedAssets, 
                GBPEUR, GBPUSD, GBPJPY, GBPSEK, GBPNOK, GBPDKK, GBPAUD, GBPCAD,
                out double NetFixedAssetsConverted);
                // Convert Cash to GBP  
                CurrencyConversion.CashConvert(BalanceCurrency, TotalCash, 
                GBPEUR, GBPUSD, GBPJPY, GBPSEK, GBPNOK, GBPDKK, GBPAUD, GBPCAD,
                out double CashConverted);
                // Convert Debt to GBP  
                CurrencyConversion.DebtConvert(BalanceCurrency, TotalDebt, 
                GBPEUR, GBPUSD, GBPJPY, GBPSEK, GBPNOK, GBPDKK, GBPAUD, GBPCAD,
                out double DebtConverted);
                // Calculate NetAssets for RONA calculation
                double NetAssets = Math.Round(CurrentAssetsConverted - CurrentLiabilitiesConverted + NetFixedAssetsConverted, 2);

                BalanceSheetList.Add(new APIListRepository()
                {
                    BalanceSymbol = RecentBalanceSymbol,
                    BalanceSheetDate = BalanceSheetDate,
                    BalanceCurrency = BalanceCurrency,
                    BalancePeriod = BalancePeriod,
                    CurrentLiabilities = CurrentLiabilitiesConverted,
                    CurrentAssets = CurrentAssetsConverted,
                    NetFixedAssets = NetFixedAssetsConverted,
                    Cash = CashConverted,
                    Debt = DebtConverted,
                    NetAssets = NetAssets
                });
                BalanceAPISymbols.Add(RecentBalanceSymbol);
                double CompletedIncomeAPICalls = IncomeAPISymbols.Where(IncomeSymbol => IncomeSymbol != null).Count();
                double CompletedBalanceAPICalls = BalanceAPISymbols.Where(BalanceSymbol => BalanceSymbol != null).Count();
                double Percent = Math.Round(CompletedBalanceAPICalls / CompletedIncomeAPICalls * 100, 2);

                Console.WriteLine("Total Progression in BalanceAPI calls: " + CompletedBalanceAPICalls + "/" + CompletedIncomeAPICalls + " " + Percent + "%");


            }

            var BalanceCount = BalanceSheetList.Where(BalanceSymbol => BalanceSymbol != null).Count();
            Console.WriteLine("List Count After BalanceAPI: " + BalanceCount);
            foreach (var item in BalanceSheetList)
            {
                string profilefilepath2 = @"C:\Users\X1 Carbon\OneDrive\Documents\Investments\Equities\Magic Formula\Magic Formula 2022\SymbolsSearched.csv";
                using (StreamWriter file = new StreamWriter(profilefilepath2, true))
                {
                    file.WriteLine($"Balance Symbol searched in API at: {DateTime.Now}" + "," + item.BalanceSymbol);
                }
            }


            //    Join ProfileList + IncomeList, then that joined list to BalanceSheetList, with key of Symbol.
            //  
            var JoinedAPIOutput = from L1 in ProfileList
                                  join L2 in IncomeList

                     on L1.ProfileSymbol equals L2.IncomeSymbol
                                  select new
                                  {
                                      IncomeSymbol = L2.IncomeSymbol,
                                      CompanyName = L1.CompanyName,
                                      Exchange = L1.Exchange,
                                      MarketCap = L1.MarketCap,
                                      Income = L2.Income,
                                      IncomeDate = L2.IncomeDate,
                                      ProfitVariation = L2.ProfitVariation,
                                      ProfitVariationScore = L2.ProfitVariationScore,
                                      RecentRevenueScore = L2.RecentRevenueScore,
                                      RecentProfitScore = L2.RecentProfitScore,
                                      ProfitableYearsScore = L2.ProfitableYearsScore,
                                      NetRevenueChangeScore = L2.NetRevenueChangeScore,
                                      NetProfitChangeScore = L2.NetProfitChangeScore,
                                      TotalScore = L2.TotalScore
                                  } into FirstJoin
                                  join L3 in BalanceSheetList on FirstJoin.IncomeSymbol equals L3.BalanceSymbol
                                  select new
                                  {
                                      L3.BalanceSymbol,
                                      FirstJoin.CompanyName,
                                      FirstJoin.Exchange,
                                      FirstJoin.MarketCap,
                                      FirstJoin.Income,
                                      FirstJoin.IncomeDate,
                                      FirstJoin.ProfitVariation,
                                      FirstJoin.ProfitVariationScore,
                                      FirstJoin.RecentRevenueScore,
                                      FirstJoin.RecentProfitScore,
                                      FirstJoin.ProfitableYearsScore,
                                      FirstJoin.NetRevenueChangeScore,
                                      FirstJoin.NetProfitChangeScore,
                                      FirstJoin.TotalScore,
                                      L3.BalanceSheetDate,
                                      L3.BalanceCurrency,
                                      L3.BalancePeriod,
                                      L3.CurrentLiabilities,
                                      L3.CurrentAssets,
                                      L3.NetFixedAssets,
                                      L3.Cash,
                                      L3.Debt,
                                      L3.NetAssets
                                  };


            List<APIListRepository> RONAList = new List<APIListRepository>();
            List<APIListRepository> EYList = new List<APIListRepository>();
            List<APIListRepository> EYandRONAList = new List<APIListRepository>();
            List<APIListRepository> APIOutputList = new List<APIListRepository>();

            //Calculate RONA and EY/EBIT. Add the values to separate lists.
            //Add remaining API outputs to single list (ProfileIncomeBalanceList)
            foreach (var item in JoinedAPIOutput)
            {
                double EnterpriseValue = ((item.MarketCap + item.Debt - item.Cash) * 100);
                double EarningsToEV = Math.Round(item.Income / (EnterpriseValue) * 100, 2);
                double ReturnOnNetAssets = Math.Round(item.Income / item.NetAssets, 2);

                RONAList.Add(new APIListRepository()
                {
                    BalanceSymbol = item.BalanceSymbol,
                    ReturnOnNetAssets = ReturnOnNetAssets
                });
                EYList.Add(new APIListRepository()
                {
                    BalanceSymbol = item.BalanceSymbol,
                    EarningsToEV = EarningsToEV
                });
                APIOutputList.Add(new APIListRepository()
                {
                    BalanceSymbol = item.BalanceSymbol,
                    CompanyName = item.CompanyName,
                    Exchange = item.Exchange,
                    MarketCap = item.MarketCap,
                    Income = item.Income,
                    IncomeDate = item.IncomeDate,
                    ProfitVariation = item.ProfitVariation,
                    ProfitVariationScore = item.ProfitVariationScore,
                    RecentRevenueScore = item.RecentRevenueScore,
                    RecentProfitScore = item.RecentProfitScore,
                    ProfitableYearsScore = item.ProfitableYearsScore,
                    NetRevenueChangeScore = item.NetRevenueChangeScore,
                    NetProfitChangeScore = item.NetProfitChangeScore,
                    TotalScore = item.TotalScore,
                    BalanceSheetDate = item.BalanceSheetDate,
                    BalanceCurrency = item.BalanceCurrency,
                    BalancePeriod = item.BalancePeriod,
                    CurrentLiabilities = item.CurrentLiabilities,
                    CurrentAssets = item.CurrentAssets,
                    NetFixedAssets = item.NetFixedAssets,
                    Cash = item.Cash,
                    Debt = item.Debt,
                    NetAssets = item.NetAssets
                });
            }

            // Create descending sorted list of RONA with sequence ascending
            List<APIListRepository> RONASortedList = RONAList.OrderByDescending(o => o.ReturnOnNetAssets).ToList();
            for (int i = 0; i < RONASortedList.Count; i++)
            {
                RONASortedList[i].RONASequence = i + 1;
            }
            // Create descending sorted list of EY/EBIT with sequence ascending
            List<APIListRepository> EYSortedList = EYList.OrderByDescending(o => o.EarningsToEV).ToList();

            for (int i = 0; i < EYSortedList.Count; i++)
            {
                EYSortedList[i].EYSequence = i + 1;

            }
            //Join the sorted RONA and EY/EBIT lists into a variable
            var EYAndRONAJoin = from L1 in RONASortedList
                                join L2 in EYSortedList

             on L1.BalanceSymbol equals L2.BalanceSymbol
                                select new
                                {
                                    BalanceSymbol = L2.BalanceSymbol,
                                    ReturnOnNetAssets = L1.ReturnOnNetAssets,
                                    RONASequence = L1.RONASequence,
                                    EarningsToEV = L2.EarningsToEV,
                                    EYSequence = L2.EYSequence
                                };
            // Add the sorted RONA and EY/EBIT var into a list, adding TotalRank which is the sequence of each added
            // together. The lower the value, the higher the ranking.
            foreach (var item in EYAndRONAJoin)
            {
                EYandRONAList.Add(new APIListRepository()
                {
                    BalanceSymbol = item.BalanceSymbol,
                    ReturnOnNetAssets = item.ReturnOnNetAssets,
                    RONASequence = item.RONASequence,
                    EarningsToEV = item.EarningsToEV,
                    EYSequence = item.EYSequence,
                    TotalRank = item.RONASequence + item.EYSequence
                });

            }

            // Join the APIOutputList and EYandRONAListSortedList lists 
            var FinalJoin = from L1 in APIOutputList
                            join L2 in EYandRONAList
              on L1.BalanceSymbol equals L2.BalanceSymbol
                            select new
                            {
                                BalanceSymbol = L1.BalanceSymbol,
                                Exchange = L1.Exchange,
                                CompanyName = L1.CompanyName,
                                TotalRank = L2.TotalRank,
                                ReturnOnNetAssets = L2.ReturnOnNetAssets,
                                RONASequence = L2.RONASequence,
                                EarningsToEV = L2.EarningsToEV,
                                EYSequence = L2.EYSequence,
                                TotalScore = L1.TotalScore,
                                ProfitVariationScore = L1.ProfitVariationScore,
                                RecentRevenueScore = L1.RecentRevenueScore,
                                RecentProfitScore = L1.RecentProfitScore,
                                ProfitableYearsScore = L1.ProfitableYearsScore,
                                NetRevenueChangeScore = L1.NetRevenueChangeScore,
                                NetProfitChangeScore = L1.NetProfitChangeScore,
                                ProfitVariation = L1.ProfitVariation,
                                MarketCap = L1.MarketCap,
                                Income = L1.Income,
                                CurrentLiabilities = L1.CurrentLiabilities,
                                CurrentAssets = L1.CurrentAssets,
                                NetFixedAssets = L1.NetFixedAssets,
                                Cash = L1.Cash,
                                Debt = L1.Debt,
                                NetAssets = L1.NetAssets,
                                IncomeDate = L1.IncomeDate,
                                BalanceSheetDate = L1.BalanceSheetDate,
                                BalanceCurrency = L1.BalanceCurrency,
                                BalancePeriod = L1.BalancePeriod

                            };

            // And place into a final list
            List<APIListRepository> FinalList = new List<APIListRepository>();
            foreach (var item in FinalJoin)
            {
                FinalList.Add(new APIListRepository()
                {                   
                    CompanyName = item.CompanyName,                    
                    BalanceSymbol = item.BalanceSymbol,
                    Exchange = item.Exchange,
                    TotalRank = item.TotalRank,
                    ReturnOnNetAssets = item.ReturnOnNetAssets,
                    RONASequence = item.RONASequence,
                    EarningsToEV = item.EarningsToEV,
                    EYSequence = item.EYSequence,
                    TotalScore = item.TotalScore,
                    ProfitVariationScore = item.ProfitVariationScore,
                    RecentRevenueScore = item.RecentRevenueScore,
                    RecentProfitScore = item.RecentProfitScore,
                    ProfitableYearsScore = item.ProfitableYearsScore,
                    NetRevenueChangeScore = item.NetRevenueChangeScore,
                    NetProfitChangeScore = item.NetProfitChangeScore,
                    ProfitVariation = item.ProfitVariation,
                    MarketCap = item.MarketCap,
                    Income = item.Income,
                    CurrentLiabilities = item.CurrentLiabilities,
                    CurrentAssets = item.CurrentAssets,
                    NetFixedAssets = item.NetFixedAssets,
                    Cash = item.Cash,
                    Debt = item.Debt,
                    NetAssets = item.NetAssets,
                    IncomeDate = item.IncomeDate,
                    BalanceSheetDate = item.BalanceSheetDate,
                    BalanceCurrency = item.BalanceCurrency,
                    BalancePeriod = item.BalancePeriod
                });

            }
            // Order FinalList by Total Rank of EY and RONA together
            List<APIListRepository> FullCSVList = FinalList.OrderBy(o => o.TotalRank).ToList();
            // Dedup multiple instances of CompanyName
            List<APIListRepository> CSVListDeduped = FullCSVList.GroupBy(s => s.CompanyName)
                                                 .Select(grp => grp.FirstOrDefault())
                                                 .OrderBy(s => s.CompanyName)
                                                 .ToList();


            for (int i = 0; i < CSVListDeduped.Count; i++)
 
                try
                {
                    string profilefilepath = @"C:\Users\X1 Carbon\OneDrive\Documents\Investments\Equities\Magic Formula\Magic Formula 2022\FMPAPIReturn.csv";
                    using (StreamWriter file = new StreamWriter(profilefilepath, true))
                    {
                        file.WriteLine($"Data Return from FMP API at {DateTime.Now}" + "," +
                           "Company:," + FullCSVList[i].CompanyName + "," +
                           "Symbol:," + FullCSVList[i].BalanceSymbol + "," +
                           "Exchange:," + FullCSVList[i].Exchange + "," +
                           "Total Rank:," + FullCSVList[i].TotalRank + "," +
                           "RONA:," + FullCSVList[i].ReturnOnNetAssets + "," +
                           "RONASeq:," + FullCSVList[i].RONASequence + "," +
                           "EY/EBIT:," + FullCSVList[i].EarningsToEV + "," +
                           "EYSeq:," + FullCSVList[i].EYSequence + "," +
                           "Total Score:," + FullCSVList[i].TotalScore + "," +
                           "ProfitVariationScore:," + FullCSVList[i].ProfitVariationScore + "," +
                           "RecentRevenueScore:," + FullCSVList[i].RecentRevenueScore + "," +
                           "RecentProfitScore:," + FullCSVList[i].RecentProfitScore + "," +
                           "ProfitableYearsScore:," + FullCSVList[i].ProfitableYearsScore + "," +
                           "NetRevenueChangeScore:," + FullCSVList[i].NetRevenueChangeScore + "," +
                           "NetProfitChangeScore:," + FullCSVList[i].NetProfitChangeScore + "," +
                           "ProfitVariation:," + FullCSVList[i].ProfitVariation + "," +
                           "MarketCap(GBPm):," + FullCSVList[i].MarketCap + "," +
                           "Profit:," + FullCSVList[i].Income + "," +
                           "CurrentLiabilities:," + FullCSVList[i].CurrentLiabilities + "," +
                           "CurrentAssets:," + FullCSVList[i].CurrentAssets + "," +
                           "NetFixedAssets:," + FullCSVList[i].NetFixedAssets + "," +
                           "Cash:," + FullCSVList[i].Cash + "," +
                           "Debt:," + FullCSVList[i].Debt + "," +
                           "NetAssets:," + FullCSVList[i].NetAssets + "," +
                           "Reporting Date:," + FullCSVList[i].IncomeDate + "," +
                           "Balance Period:," + FullCSVList[i].BalancePeriod);                          
                    }
                }

                catch (Exception ex)
                {
                    throw new ApplicationException("Exception when attempting to write to CSV (Full Output):", ex);
                }

      //      List<APIListRepository> Query = FullCSVList.Where(o => o.TotalRank < 6);

     //      if (FullCSVList.ProfitVariationScore > 4 && RecentOpProfit > 0)
      //      {

            }
                                              

    }
}
      
 

      