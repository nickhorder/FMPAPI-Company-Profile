using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace FMPAPI
{
    public class APIListRepository
    {
        public string ProfileSymbol { get; set; }
        public string IncomeSymbol { get; set; }
        public string BalanceSymbol { get; set; }

        public string JoinedSymbol { get; set; }
        public string CompanyName { get; set; }
        public double MarketCap { get; set; }
        public string ProfileCurrency { get; set; }
        public string Exchange { get; set; }
        //     public bool IsActivelyTrading { get; set; }

        public string IncomeDate { get; set; }
        public string IncomeCurrency { get; set; }
        public string IncomePeriod { get; set; }
        public double Income { get; set; }
        public double Revenue { get; set; }
        public double ProfitVariation { get; set; }

        public double ProfitVariationScore { get; set; }
        public double RecentRevenueScore { get; set; }
        public double RecentProfitScore { get; set; }
        public double ProfitableYearsScore { get; set; }
        public double NetRevenueChangeScore { get; set; }
        public double NetProfitChangeScore { get; set; }
        public double TotalScore { get; set; }
        public string BalanceSheetDate { get; set; }
        public string BalanceCurrency { get; set; }
        public string BalancePeriod { get; set; }
        public double CurrentLiabilities { get; set; }
        public double CurrentAssets { get; set; }
        public double NetFixedAssets { get; set; }
        public double Cash { get; set; }
        public double Debt { get; set; }
        public double NetAssets { get; set; }
        public double ReturnOnNetAssets { get; set; }
        public double EarningsToEV { get; set; }
        public int RONASequence { get; set; }
        public int EYSequence { get; set; }
        public double TotalRank { get; set; }
        /*
       [JsonPropertyName("reportedCurrency")]
       public string ReportedCurrency { get; set; }

       [JsonPropertyName("fillingDate")]
       public string FilingDate { get; set; }

       [JsonPropertyName("operatingIncome")]
       public double OperatingProfit { get; set; }
       [JsonPropertyName("symbol")]
       public string Symbol { get; set; }
       [JsonPropertyName("revenue")]
       public double Revenue { get; set; }
*/
    }
}
