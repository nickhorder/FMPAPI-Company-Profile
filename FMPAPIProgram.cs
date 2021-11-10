using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;

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
         Currently running on FTSE350. 
          
    */

    {
        public static async Task Main(string[] args)
        {


            string ProfileEndPoint = "https://financialmodelingprep.com/api/v3/profile/";
            string BalanceEndPoint = "https://financialmodelingprep.com/api/v3/balance-sheet-statement/";
            string IncomeEndPoint = "https://financialmodelingprep.com/api/v3/income-statement/";
            string APIKey = "apikey=notforgithub";
            string QuestionM = "?";
            double GBPUSD = 1.35;
            double GBPEUR = 1.17;
            TickerStoreProfile Tickers = new TickerStoreProfile();
  
            {
                foreach (string Ticker in Tickers)
                {
//Call ProfileAPI
                    string ProfileURI = ProfileEndPoint + Ticker + QuestionM + APIKey;
                    var profilerepos = await CallProfileAPI.ProfileAPI(ProfileURI);
                

                    //Profile API - Obtain first instance of Company Name
                    var CompanyNameList = new List<string>();
                    foreach (var ddd in profilerepos)
                    {
                        CompanyNameList.Add(ddd.CompanyName);
                    }
                    string[] CompanyNameArray = CompanyNameList.ToArray();
                    string returnCoName = CompanyNameArray.FirstOrDefault();
            //        Console.WriteLine("Company:, " + returnCoName);

                    //Profile API - Obtain first instance of Symbol
                    var ProfSymbolList = new List<string>();
                    foreach (var ddd in profilerepos)
                    {
                        ProfSymbolList.Add(ddd.Symbol);
                    }
                    string[] ProfSymbolArray = ProfSymbolList.ToArray();
                    string returnProfSymbol = ProfSymbolArray.FirstOrDefault();
                    Console.WriteLine("Symbol:," + returnProfSymbol);

                    //Profile API - Obtain first instance of Currency
                    var ProfCurrList = new List<string>();
                    foreach (var ddd in profilerepos)
                    {
                        ProfCurrList.Add(ddd.Currency);
                    }
                    string[] ProfCurrArray = ProfCurrList.ToArray();
                    string returnProfCurr =  ProfCurrArray.FirstOrDefault();

                    //Profile API - Obtain first instance of Market Capitalisation 

                    var MarketCapList = new List<double>();
                    foreach (var ddd in profilerepos)
                    {
                        MarketCapList.Add(ddd.MarketCap);
                    }
                    double[] MarketCapArray = MarketCapList.ToArray();
                    double returnMarketCap = MarketCapArray.FirstOrDefault();

                    // Ensure Market Capitalisation is in GBP
                    double MarketCapConv = new Func<double>(delegate ()
                    {
                        switch (returnProfCurr)
                        {
                            case "GBp": return Math.Round((returnMarketCap)/ 1e6, 2);
                            case "EUR": return Math.Round((returnMarketCap / GBPEUR)/ 1e6, 2);
                            case "USD": return Math.Round((returnMarketCap / GBPUSD) / 1e6, 2);
                            default: return 123456789;
                        }
                    }
                    )();
                    if (MarketCapConv == 123456789)
                    {
                        Console.WriteLine("New Profile Currency to be added, or no return for Company");
                    }
            //        Console.WriteLine("Market Cap (GBPm):, " + MarketCapConv);


//Call IncomeAPI
                    string IncomeURI = IncomeEndPoint + Ticker + QuestionM + APIKey;
                    var incomerepos = await CallIncomeAPI.IncomeAPI(IncomeURI);


                    //Obtain first instance of Income Statement Date
                    var IncomeDateList = new List<string>();
                    foreach (var ddd in incomerepos)
                    {
                        IncomeDateList.Add(ddd.FilingDate);
                    }
                    string[] IncomeDateArray = IncomeDateList.ToArray();
                    string returnIncomeDate =  IncomeDateArray.FirstOrDefault();
            //        Console.WriteLine("Income Statement Date:, " + returnIncomeDate);

                    //Obtain first instance of Income Statement Currency
                    var IncomeCurrencyList = new List<string>();
                    foreach (var ddd in incomerepos)
                    {
                        IncomeCurrencyList.Add(ddd.ReportedCurrency);
                    }
                    string[] IncomeCurrencyArray = IncomeCurrencyList.ToArray();
                    string returnIncomeCurrency = IncomeCurrencyArray.FirstOrDefault();
                    
                    //Obtain first instance of Income Statement Period, and add on IncomeCurrency for display
                    var IncomePeriodList = new List<string>();
                    foreach (var ddd in incomerepos)
                    {
                        IncomePeriodList.Add(ddd.Period);
                    }
                    string[] IncomePeriodArray = IncomePeriodList.ToArray();
                    string returnIncomePeriod = IncomePeriodArray.FirstOrDefault() 
                        + "(" + returnIncomeCurrency + ")";
            //        Console.WriteLine("Inc.Statement Period/Currency:, " + returnIncomePeriod);
  
                    //Obtain first instance of Operating Profit and tag on IncomeCurrency
                    var IncomeList = new List<double>();
                    foreach (var ddd in incomerepos)
                    {
                        IncomeList.Add(ddd.OperatingProfit);
                    }
                    double[] IncomeArray = IncomeList.ToArray();
                    double returnIncome = IncomeArray.FirstOrDefault();

                    // Convert Operating Profit to GBP  
                    double OperatingProfitConv = new Func<double>(delegate ()
                    {
                        switch (returnIncomeCurrency)
                        {
                            case "GBP": return Math.Round(returnIncome / 1e6, 2);
                            case "USD": return Math.Round((returnIncome / GBPUSD)/1e6, 2);
                            case "EUR": return Math.Round((returnIncome / GBPEUR)/1e6, 2);
                            default: return 123456789;
                        }
                    }
                    )();
                    if (OperatingProfitConv == 123456789)
                    {
                        Console.WriteLine("No Income Statement Currency Found - Needs to be Added");
                    }
            //        Console.WriteLine("Operating Profit (GBPm):, " + OperatingProfitConv);



//Call BalanceAPI
                    string BalanceURI = BalanceEndPoint + Ticker + QuestionM + APIKey;
                    var balancerepos = await CallBalanceAPI.BalanceAPI(BalanceURI);


                    //Obtain first instance of Balance Sheet Date
                    var BalDateList = new List<string>();
                    foreach (var ddd in balancerepos)
                    {
                        BalDateList.Add(ddd.FilingDate);
                    }
                    string[] DateArray = BalDateList.ToArray();
                    string returnDate = DateArray.FirstOrDefault();
            //        if (returnDate == null)
            //        {
            //            returnDate = " ";
             //       }
            //        Console.WriteLine("Balance Sheet Date:, " + returnDate);

                    //Obtain first instance of Balance Sheet Currency
                    var BalanceCurrencyList = new List<string>();
                    foreach (var ddd in balancerepos)
                    {
                        BalanceCurrencyList.Add(ddd.ReportedCurrency);
                    }
                    string[] BalanceCurrencyArray = BalanceCurrencyList.ToArray();
                    string returnBalanceCurrency = BalanceCurrencyArray.FirstOrDefault();

                    //Obtain first instance of Balance Sheet Period, and add on BalanceCurrency for display
                    var BalancePeriodList = new List<string>();
                    foreach (var ddd in balancerepos)
                    {
                        BalancePeriodList.Add(ddd.Period);
                    }
                    string[] BalancePeriodArray = BalancePeriodList.ToArray();
                    string returnBalancePeriod =  BalancePeriodArray.FirstOrDefault()
                        + "(" + returnBalanceCurrency + ")";
           //         Console.WriteLine("Bal.Statement Period/Currency:, " + returnBalancePeriod);

                    //Obtain first instance of CurrentLiabilities
                    var BalCurrLiaList = new List<double>();
                    foreach (var ddd in balancerepos)
                    {
                        BalCurrLiaList.Add(ddd.CurrentLiabilities);
                    }
                    double[] CurrLiaArray = BalCurrLiaList.ToArray();
                    double returnCurrLia = CurrLiaArray.FirstOrDefault();   

                    //Obtain first instance of CurrentAssets
                    var BalCurrAssList = new List<double>();
                    foreach (var ddd in balancerepos)
                    {
                        BalCurrAssList.Add(ddd.CurrentAssets);
                    }
                    double[] CurrAssArray = BalCurrAssList.ToArray();
                   double returnCurrAss = (CurrAssArray.FirstOrDefault());

                    //Obtain first instance of NetFixedAssets
                    var BalNetFixAssList = new List<double>();
                    foreach (var ddd in balancerepos)
                    {
                        BalNetFixAssList.Add(ddd.NetFixedAssets);
                    }
                    double[] NetFixAssArray = BalNetFixAssList.ToArray();
                    double returnNetFixAss = (NetFixAssArray.FirstOrDefault());

                    //Obtain first instance of Cash
                    var BalCashList = new List<double>();
                    foreach (var ddd in balancerepos)
                    {
                        BalCashList.Add(ddd.Cash);
                    }
                    double[] CashArray = BalCashList.ToArray();
                    double returnCash = (CashArray.FirstOrDefault());

                    //Obtain first instance of Debt
                    var BalDebtList = new List<double>();
                    foreach (var ddd in balancerepos)
                    {
                        BalDebtList.Add(ddd.TotalDebt);
                    }
                    double[] DebtArray = BalDebtList.ToArray();
                    double returnDebt = (DebtArray.FirstOrDefault());


                    // Convert Current Assets to GBP  
                    double CurrAssConv = new Func<double>(delegate ()
                    {
                        switch (returnBalanceCurrency)
                        {
                            case "GBP": return Math.Round(returnCurrAss / 1e6, 2);
                            case "USD": return Math.Round((returnCurrAss / GBPUSD)/1e6, 2);
                            case "EUR": return Math.Round((returnCurrAss / GBPEUR)/1e6, 2);
                            default: return 123456789;
                        }                  
                    }                
                    )();
                    if (CurrAssConv == 123456789)
                    {
                        Console.WriteLine("No Balance Sheet Currency Found (Current Assets)");
                    }                 
           //         Console.WriteLine("Current Assets (GBPm):, " + CurrAssConv);


                    // Convert Current Liabilities to GBP  
                    double CurrLiaConv = new Func<double>(delegate ()
                    {
                        switch (returnBalanceCurrency)
                        {
                            case "GBP": return Math.Round(returnCurrLia / 1e6, 2);
                            case "USD": return Math.Round((returnCurrLia / GBPUSD)/1e6, 2);
                            case "EUR": return Math.Round((returnCurrLia / GBPEUR)/1e6, 2);
                            default: return 123456789;
                        }
                    }
                    )();
                    if (CurrLiaConv == 123456789)
                    {
                        Console.WriteLine("No Balance Sheet Currency Found (Current Liabilities)");
                    }
          //          Console.WriteLine("Current Liabilities (GBPm):, " + CurrLiaConv);


                    // Convert Net Fixed Assets to GBP  
                    double NetFixAssConv = new Func<double>(delegate ()
                    {
                        switch (returnBalanceCurrency)
                        {
                            case "GBP": return Math.Round(returnNetFixAss / 1e6, 2);
                            case "USD": return Math.Round((returnNetFixAss / GBPUSD)/1e6, 2);
                            case "EUR": return Math.Round((returnNetFixAss / GBPEUR)/1e6, 2);
                            default: return 123456789;
                        }
                    }
                    )();
                    if (NetFixAssConv == 123456789)
                    {
                        Console.WriteLine("No Balance Sheet Currency Found (Net Fixed Assets)");
                    }
          //          Console.WriteLine("Net Fixed Assets (GBPm):, " + NetFixAssConv);


                    // Convert Cash to GBP  
                    double CashConv = new Func<double>(delegate ()
                    {
                        switch (returnBalanceCurrency)
                        {
                            case "GBP": return Math.Round(returnCash / 1e6, 2);
                            case "USD": return Math.Round((returnCash / GBPUSD)/1e6, 2);
                            case "EUR": return Math.Round((returnCash / GBPEUR)/1e6, 2);
                            default: return 123456789;
                        }
                    }
                    )();
                    if (CashConv == 123456789)
                    {
                        Console.WriteLine("No Balance Sheet Currency Found (Cash)");
                    }
          //          Console.WriteLine("Cash (GBPm):, " + CashConv);


                    // Convert Debt to GBP  
                    double DebtConv = new Func<double>(delegate ()
                    {
                        switch (returnBalanceCurrency)
                        {
                            case "GBP": return Math.Round(returnDebt / 1e6, 2);
                            case "USD": return Math.Round((returnDebt / GBPUSD)/1e6, 2);
                            case "EUR": return Math.Round((returnDebt / GBPEUR)/1e6, 2);
                            default: return 123456789;
                        }
                    }
                    )();
                    if (DebtConv == 123456789)
                    {
                        Console.WriteLine("No Balance Sheet Currency Found (Debt)");
                    }
          //          Console.WriteLine("Debt (GBPm):, " + DebtConv);

                    // Calculate NetAssets for RONA calculation
                    double NetAssets = Math.Round(CurrAssConv - CurrLiaConv + NetFixAssConv, 2);
         //           Console.WriteLine("Net Assets (GBPm):, " + NetAssets);

                    //Calculate RONA (ReturnOnNetAssets) = Earnings/(Assets - Liabilities + Fixed)
                    double ReturnOnNetAssets = Math.Round(OperatingProfitConv / NetAssets, 2);

                    //EarningsToEV = Earnings/(MarketCap + Debt - Cash)
                    double EarningsToEV = Math.Round(OperatingProfitConv /
                                       (MarketCapConv + DebtConv - CashConv)*100, 2);
         //           Console.WriteLine(ReturnOnNetAssets);
         //           Console.WriteLine(EarningsToEV);

                
//Write values to CSV

               //    foreach (string Ticker2 in Tickers)
             {
                   try
                {
                   string profilefilepath = @"C:\Users\X1 Carbon\OneDrive\Documents\FMPAPIReturn.csv";
                   using (StreamWriter file = new StreamWriter(profilefilepath, true))
                    {
                    file.WriteLine($"Data Return from FMP API at {DateTime.Now}" + "," +
                    "Company:," + returnCoName + "," +
                    "Symbol:," + returnProfSymbol + "," +
                    "MarketCap(GBPm):," + MarketCapConv + "," +
                    "Income Statement Date:," + returnIncomeDate + "," +
                    "Income Statement Period:," + returnIncomePeriod + "," +
                    "Operating Profit(GBPm):," + OperatingProfitConv + "," +
                    "Balance Sheet Date:," + returnDate + "," +
                    "Balance Sheet Period:," + returnBalancePeriod + "," +
                    "Net Fixed Assets:," + NetFixAssConv + "," +
                    "Total Cash:," + CashConv + "," +
                    "Total Current Assets:," + CurrAssConv + "," +
                    "Total Current Liabilities:," + CurrLiaConv + "," +
                    "Total Debt:," + DebtConv + "," +                     
                    "ReturnOnNetAssets:," + ReturnOnNetAssets + "," +
                    "EarningsToEV:," + EarningsToEV);
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