using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;


namespace FMPAPI
{

    public class FMPAPICompanyProfileProgram

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
            string CurrencyAPIKey = " removed from github";
            string StockListEndPoint = "https://financialmodelingprep.com/api/v3/stock/list?";
            string ProfileEndPoint = "https://financialmodelingprep.com/api/v3/profile/";
            string BalanceEndPoint = "https://financialmodelingprep.com/api/v3/balance-sheet-statement/";
            string IncomeEndPoint = "https://financialmodelingprep.com/api/v3/income-statement/";
            string APIKey = " removed from github";
            string QuestionM = "?";

//Call CurrencyAPI to get exchange rates (to EUR)

            string CurrencyURI = CurrencyListEndPoint + CurrencyAPIKey;
            Console.WriteLine(CurrencyURI);
            var currencyrepos = await CallCurrencyAPI.CurrencyAPI(CurrencyURI);
            double GBPEUR = (currencyrepos.Rates.EUR / currencyrepos.Rates.GBP);
            double GBPUSD = (currencyrepos.Rates.USD / currencyrepos.Rates.GBP);
            double GBPJPY = (currencyrepos.Rates.JPY / currencyrepos.Rates.GBP);

//Call StockListAPI to get available Symbols

            string StockListURI = StockListEndPoint + APIKey;
            var stocklistrepos = await CallStockListAPI.StockListAPI(StockListURI);

            //Create list of companies from only selected indexes

            List<string> ListOfCompaniesToSearch = new List<string>();
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
                        ListOfCompaniesToSearch.Add(SelectedCompanies);
                        ListOfCompaniesToSearch.Remove("Other Exchange");
                        //    Console.WriteLine(SelectedSymbols);
                    }
                }
            foreach (string Companies in ListOfCompaniesToSearch)
            {

                string profilefilepath2 = @"C:\Users\X1 Carbon\OneDrive\Documents\Investments\Equities\Magic Formula\Magic Formula 2022\SymbolList.csv";
                using (StreamWriter file = new StreamWriter(profilefilepath2, true))
                {
                    file.WriteLine(Companies);
                }
            }

/*
            //Single call section - start
            string Ticker1 = "RCUS";
            string Ticker2 = "888.L";
            string Ticker3 = "DWS.DE";
            string Ticker4 = "CTO.L";
            string Ticker5 = "ULVR.L";
            string Ticker6 = "TUP";
            List<string> SingleSymbolList = new List<string>();
            SingleSymbolList.Add(Ticker1);
            SingleSymbolList.Add(Ticker2);
            SingleSymbolList.Add(Ticker3);
            SingleSymbolList.Add(Ticker4);
            SingleSymbolList.Add(Ticker5);
            SingleSymbolList.Add(Ticker6);

            foreach (string Ticker in SingleSymbolList)
*/
             foreach (string Ticker in ListOfCompaniesToSearch)

            {

//Call ProfileAPI                                       

                string ProfileURI = ProfileEndPoint + Ticker + QuestionM + APIKey;
                var profilerepos = await CallProfileAPI.ProfileAPI(ProfileURI);
                Console.WriteLine($"Calling: " + ProfileURI);
                var ProfSymbolList = new List<string>();
                var CompanyNames = new List<string>();
                var CompanyMarketCap = new List<double>();
                var ProfCurrList = new List<string>();

                foreach (var aaa in profilerepos)
                {
                    if (aaa.Symbol != null)
                        ProfSymbolList.Add(aaa.Symbol);
                    CompanyNames.Add(aaa.CompanyName);
                    double MarketCapNullTest = aaa.MarketCap ?? -9999; //returns -9999 if null, otherwise the value found
                    CompanyMarketCap.Add(MarketCapNullTest);
                    ProfCurrList.Add(aaa.Currency);
                }
         
                    string[] ProfSymbolArray = ProfSymbolList.ToArray();
                    string returnProfSymbol = ProfSymbolArray.FirstOrDefault();
              //      Console.WriteLine("Currently Processing: " + returnProfSymbol);

                    string[] CompanyNameArray = CompanyNames.ToArray();
                    string FirstCompany = CompanyNameArray.FirstOrDefault();
                    string returnCoName = FirstCompany.Replace(",", " ");

                    double[] MarketCapArray = CompanyMarketCap.ToArray();
                    double returnMarketCap = MarketCapArray.FirstOrDefault();

                    string[] ProfCurrArray = ProfCurrList.ToArray();
                    string returnProfCurr = ProfCurrArray.FirstOrDefault();


                    // Ensure Market Capitalisation is in GBP
                    CurrencyConversion.MarketCapConvert(returnProfCurr, returnMarketCap, GBPEUR, GBPUSD, GBPJPY, out double MarketCapConv);


//Call IncomeAPI
                    string IncomeURI = IncomeEndPoint + Ticker + QuestionM + APIKey;
                    var incomerepos = await CallIncomeAPI.IncomeAPI(IncomeURI);
        
                    var IncomeDateList = new List<string>();
                    var IncomeCurrencyList = new List<string>();
                    var IncomePeriodList = new List<string>();
                    var IncomeList = new List<double>();
                    var RevenueList = new List<double>();
                    foreach (var ddd in incomerepos)
                    {
                        if (ddd.OperatingProfit > 0)
                        IncomeDateList.Add(ddd.FilingDate);
                        IncomeCurrencyList.Add(ddd.ReportedCurrency);
                        IncomePeriodList.Add(ddd.Period);
                        IncomeList.Add(ddd.OperatingProfit);
                        RevenueList.Add(ddd.Revenue);
                   }
                    string[] IncomeDateArray = IncomeDateList.ToArray();
                    string returnIncomeDate = IncomeDateArray.FirstOrDefault();

                    string[] IncomeCurrencyArray = IncomeCurrencyList.ToArray();
                    string returnIncomeCurrency = IncomeCurrencyArray.FirstOrDefault();

                    string[] IncomePeriodArray = IncomePeriodList.ToArray();
                    string returnIncomePeriod = IncomePeriodArray.FirstOrDefault()
                    + "(" + returnIncomeCurrency + ")";

                    double[] IncomeArray = IncomeList.ToArray();
                    double[] RevenueArray = RevenueList.ToArray();

// Scoring Calculations - Additional insight over and above the usual RONA / EV/EBITDA metrics

                // This if statement is left open, so no further processing for the rest of the program
                // will be done for companies that have less than 4 years worth of financial data.
                if (IncomeArray.Length > 4)
                {
                    ScoringCalculations.ProfitVariation(IncomeArray, out double ProfVariation, out double returnIncome);
             //       Console.WriteLine(ProfVariation + " " + returnIncome);

                    ScoringCalculations.ProfitVariationScore(ProfVariation, out double ProfitVariationScore);
              //      Console.WriteLine($"Profit Variation Score: " + ProfitVariationScore);

                    ScoringCalculations.RevenueScore(RevenueArray, out double RecentRevenueScore, out double[] RecentRevenueArray);
              //      Console.WriteLine($"Revenue Score: " + RecentRevenueScore);

                    ScoringCalculations.ProfitScore(IncomeArray, out double RecentProfitScore, out double[] RecentProfitArray);
             //       Console.WriteLine($"Profit Score: " + RecentProfitScore);

                    ScoringCalculations.ProfitableYearsScore(RecentProfitArray, out double ProfitableYearsScore);
             //       Console.WriteLine($"Profitable Years Score: " + ProfitableYearsScore);

                    ScoringCalculations.NetRevenueChangeScore(RecentRevenueArray, out double NetRevenueChangeScore);
              //      Console.WriteLine($"NetRevenueChangeScore: " + NetRevenueChangeScore);

                    ScoringCalculations.NetProfitChangeScore(RecentProfitArray, out double NetProfitChangeScore);
            //        Console.WriteLine($"NetProfitChangeScore: " + NetProfitChangeScore);

                    //TOTAL SCORE
                    double TotalScore = (ProfitVariationScore + RecentRevenueScore + RecentProfitScore +
                                         ProfitableYearsScore + NetRevenueChangeScore + NetProfitChangeScore);
           //         Console.WriteLine(TotalScore);

                    // Convert Operating Profit to GBP  
                    CurrencyConversion.IncomeConvert(returnIncomeCurrency, returnIncome, GBPEUR, GBPUSD, GBPJPY, out double IncomeConverted);


//Call BalanceAPI
                        string BalanceURI = BalanceEndPoint + Ticker + QuestionM + APIKey;
                        var balancerepos = await CallBalanceAPI.BalanceAPI(BalanceURI);


                        //Obtain first instance of Balance Sheet Date
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
                            BalDateList.Add(ddd.FilingDate);
                            BalanceCurrencyList.Add(ddd.ReportedCurrency);
                            BalancePeriodList.Add(ddd.Period);
                            BalCurrLiaList.Add(ddd.CurrentLiabilities);
                            BalCurrAssList.Add(ddd.CurrentAssets);
                            BalNetFixAssList.Add(ddd.NetFixedAssets);
                            BalCashList.Add(ddd.Cash);
                            BalDebtList.Add(ddd.TotalDebt);
                        }
                        string[] DateArray = BalDateList.ToArray();
                        string returnDate = DateArray.FirstOrDefault();

                        string[] BalanceCurrencyArray = BalanceCurrencyList.ToArray();
                        string returnBalanceCurrency = BalanceCurrencyArray.FirstOrDefault();

                        string[] BalancePeriodArray = BalancePeriodList.ToArray();
                        string returnBalancePeriod = BalancePeriodArray.FirstOrDefault()
                            + "(" + returnBalanceCurrency + ")";

                        double[] CurrLiaArray = BalCurrLiaList.ToArray();
                        double returnCurrLia = CurrLiaArray.FirstOrDefault();

                        double[] CurrAssArray = BalCurrAssList.ToArray();
                        double returnCurrAss = (CurrAssArray.FirstOrDefault());

                        double[] NetFixAssArray = BalNetFixAssList.ToArray();
                        double returnNetFixAss = (NetFixAssArray.FirstOrDefault());

                        double[] CashArray = BalCashList.ToArray();
                        double returnCash = (CashArray.FirstOrDefault());

                        double[] DebtArray = BalDebtList.ToArray();
                        double returnDebt = (DebtArray.FirstOrDefault());

                        // Convert Current Assets to GBP  
                        CurrencyConversion.CurrentAssetConvert(returnBalanceCurrency, returnCurrAss, 
                                                               GBPEUR, GBPUSD, GBPJPY, out double CurrentAssetConverted);

                        // Convert Current Liabilities to GBP  
                        CurrencyConversion.CurrentLiabilityConvert(returnBalanceCurrency, returnCurrLia,
                                                               GBPEUR, GBPUSD, GBPJPY, out double CurrentLiabilitiesConverted);
                      
                        // Convert Net Fixed Assets to GBP  
                        CurrencyConversion.NetFixedAssetsConvert(returnBalanceCurrency, returnNetFixAss,
                                                               GBPEUR, GBPUSD, GBPJPY, out double NetFixedAssetsConverted);
                
                        // Convert Cash to GBP  
                        CurrencyConversion.CashConvert(returnBalanceCurrency, returnCash,
                                                              GBPEUR, GBPUSD, GBPJPY, out double CashConverted);

                        // Convert Debt to GBP  
                        CurrencyConversion.DebtConvert(returnBalanceCurrency, returnDebt,
                                                              GBPEUR, GBPUSD, GBPJPY, out double DebtConverted);
 

                        // Calculate NetAssets for RONA calculation
                        double NetAssets = Math.Round(CurrentAssetConverted - CurrentLiabilitiesConverted + NetFixedAssetsConverted, 2);

                        //Calculate RONA (ReturnOnNetAssets) = Earnings/(Assets - Liabilities + Fixed)
                        double ReturnOnNetAssets = Math.Round(IncomeConverted / NetAssets, 2);

                        //EarningsToEV = Earnings/(MarketCap + Debt - Cash)
                        double EarningsToEV = Math.Round(IncomeConverted /
                                           (MarketCapConv + DebtConverted - CashConverted) * 100, 2);
 

//Write values to CSV

                        // Only write Companies that meet the following criteria:
                        // - Operating Profit greater than 0
                        // - Market Capitalisation above £50m.
                        // - Variation in this year's profit compared to average of last 4 year's profit is not
                        //   greater than a multiple of X.
                        if (IncomeConverted > 0 && 
                            MarketCapConv > 50 &&
                            ProfVariation <= 150)
                       
                        {
                            try
                            {
                                string profilefilepath = @"C:\Users\X1 Carbon\OneDrive\Documents\Investments\Equities\Magic Formula\Magic Formula 2022\FMPAPIReturn.csv";
                                using (StreamWriter file = new StreamWriter(profilefilepath, true))
                                {
                                file.WriteLine($"Data Return from FMP API at {DateTime.Now}" + "," +
                                "Company:," + returnCoName + "," +
                                "Symbol:," + returnProfSymbol + "," +
                                "MarketCap(GBPm):," + MarketCapConv + "," +                               
                                "Operating Profit(GBPm):," + IncomeConverted + "," +
                                "Profit Variation:," + ProfVariation + "," +
                                "Balance Sheet Date:," + returnDate + "," +
                                "Balance Sheet Period:," + returnBalancePeriod + "," +
                                "Income Statement Date:," + returnIncomeDate + "," +
                                "Income Statement Period:," + returnIncomePeriod + "," +
                                //    "Net Fixed Assets:," + NetFixedAssetsConverted + "," +
                                //    "Total Cash:," + CashConverted + "," +
                                //     "Total Current Assets:," + CurrentAssetConverted + "," +
                                //      "Total Current Liabilities:," + CurrentLiabilitiesConverted + "," +
                                //     "Total Debt:," + DebtConverted + "," +
                                "ReturnOnNetAssets:," + ReturnOnNetAssets + "," +
                                "EarningsToEV:," + EarningsToEV + "," +
                                "Profit Variation:," + ProfVariation + "," +
                                "Profit Variation Score:," + ProfitVariationScore + "," +
                                "Revenue Score:," + RecentRevenueScore + "," +
                                "Profit Score:," + RecentProfitScore + "," +
                                "Profitable Years Score:," + ProfitableYearsScore + "," +
                                "NetRevenueChangeScore:," + NetRevenueChangeScore + "," +
                                "NetProfitChangeScore:," + NetProfitChangeScore + "," +
                                "Total Score:," + TotalScore);
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new ApplicationException("Exception when attempting to write to CSV (Profile Data):", ex);
                            }
                        }
                    }

                }
            }
        }
    }
 