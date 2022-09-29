using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMPAPI
{
    class ScoringCalculations
    {
        //Profit Variation. This is the current Income divided by the average of the last 4 years Income. It's designed
        // To identify "shocks" in profit, which can explain why certain companies come out top for bumper, one-off
        // years that probably won't be repeated. Absolute value (percentage).
        public static double ProfitVariation(double[] IncomeOpProfitArray, 
                                             double RecentOpProfit,
                                             out double ProfVariation)
        {
            double returnIncome1PYR = IncomeOpProfitArray.ElementAt(1); //1st Previous Year Income
            double returnIncome2PYR = IncomeOpProfitArray.ElementAt(2); //2nd Previous Year Income
            double returnIncome3PYR = IncomeOpProfitArray.ElementAt(3); //3rd Previous Year Income
            double returnIncome4PYR = IncomeOpProfitArray.ElementAt(4); //4th Previous Year Income
            var arr = new double[]  { returnIncome1PYR,
                                      returnIncome2PYR,
                                      returnIncome3PYR,
                                      returnIncome4PYR };        //Create array of prev yrs income

            double Inc4YRAverage = (Queryable.Average(arr.AsQueryable())); //Average the array
            double Inc4YRAverage2 = Math.Abs(Inc4YRAverage) / 1e6; //Convert 4yr av earnings to abs value



            double ProfVarCalc =
            Math.Round(RecentOpProfit / 1e6, 2) / (Inc4YRAverage2); //Divide current profit with 4yr av
            
           ProfVariation = ProfVarCalc * 100;
 
                return (ProfVariation);
        }
        
        //Profit Variation Score. Bracketed scoring for result from Profit Variation.
        public static double ProfitVariationScore(double ProfVariation, 
                                                  out double ProfitVariationScore)
        {
            ProfitVariationScore = ProfVariation switch
            {
                > 160 => -3,
                < 160 and >= 150 => -1,
                < 150 and >= 140 => 0,
                < 140 and >= 130 => 1,
                < 130 and >= 120 => 2,
                < 120 and >= 110 => 4,
                < 110 and >= 100 => 5,
                < 100 and >= 90 => 5,
                < 90 and >= 80 => 4,
                < 80 and >= 70 => 2,
                < 70 and >= 60 => 1,
                < 60 and >= 50 => 0,
                < 50 and >= 40 => -1,
                < 40 and >= 30 => -1,
                < 30 => -3,
                _ => 999 // default value
            };
            return (ProfitVariationScore);
        }

        //Revenue Score. Divide recent revenue by the maximum of the last 5 years (including current). This will 
        // be 100% if the most recent revenue is highest
        public static double RevenueScore(double[] RevenueArray, 
                                          out double RecentRevenueScore, 
                                          out double[] RecentRevenueArray)
        {
            double returnRevenue0PYR = RevenueArray.ElementAt(0); //Current Revenue
            double returnRevenue1PYR = RevenueArray.ElementAt(1); //1st Previous Year Revenue
            double returnRevenue2PYR = RevenueArray.ElementAt(2); //2nd Previous Year Revenue
            double returnRevenue3PYR = RevenueArray.ElementAt(3); //3rd Previous Year Revenue
            double returnRevenue4PYR = RevenueArray.ElementAt(4); //4th Previous Year Revenue
            RecentRevenueArray = new double[] { returnRevenue0PYR,
                                                returnRevenue1PYR,
                                                returnRevenue2PYR,
                                                returnRevenue3PYR,
                                                returnRevenue4PYR }; //Create array of prev yrs Revenue

            double MaxRevenue = RecentRevenueArray.Max();
            double RecentRevenueToMaxRevenue = (RevenueArray.ElementAt(0) / MaxRevenue) * 100;

            RecentRevenueScore = RecentRevenueToMaxRevenue switch
            {
                >= 100 => 6,
                < 100 and >= 90 => 5.0,
                < 90 and >= 80 => 4.5,
                < 80 and >= 70 => 3.5,
                < 70 and >= 60 => 2.5,
                < 60 and >= 50 => 1.0,
                < 50 and >= 40 => 0.5,
                < 40 and >= 30 => 0,
                < 30 and >= 20 => -1,
                < 20 and >= 10 => -2,
                < 10 and >= 0 => -3,
                < 0 => -4,
                _ => 999 // default value
            };

            return (RecentRevenueScore);
        }

        // Profit Score. Divide recent profit by the maximum of the last 5 years (including current). This will 
        // be 100% if the most recent profit is highest. Scored more strongly than Revenue Score.
        public static double ProfitScore(double[] IncomeArray, 
                                         out double RecentProfitScore, 
                                         out double[] RecentProfitArray)
        {
            double returnProfit0PYR = IncomeArray.ElementAt(0); //Current Profit
            double returnProfit1PYR = IncomeArray.ElementAt(1); //1st Previous Year Profit
            double returnProfit2PYR = IncomeArray.ElementAt(2); //2nd Previous Year Profit
            double returnProfit3PYR = IncomeArray.ElementAt(3); //3rd Previous Year Profit
            double returnProfit4PYR = IncomeArray.ElementAt(4); //4th Previous Year Profit
            RecentProfitArray = new double[] { returnProfit0PYR,
                                               returnProfit1PYR,
                                               returnProfit2PYR,
                                               returnProfit3PYR,
                                               returnProfit4PYR }; //Create array of prev yrs Profit

            double MaxProfit = RecentProfitArray.Max();
            double RecentProfitToMaxProfit = (IncomeArray.ElementAt(0) / MaxProfit) * 100;
            
            RecentProfitScore = RecentProfitToMaxProfit switch
            {
                >= 100 => 12.0,
                < 100 and >= 90 => 10.0,
                < 90 and >= 80 => 9.0,
                < 80 and >= 70 => 7.0,
                < 70 and >= 60 => 5.0,
                < 60 and >= 50 => 2.0,
                < 50 and >= 40 => 1.0,
                < 40 and >= 30 => 0.5,
                < 30 and >= 20 => -2,
                < 20 and >= 10 => -4,
                < 10 and >= 0 => -6,
                < 0 => -8,
                _ => 999 // default value
            };

            return (RecentProfitScore);
        }

        //Profitable Years Score. Count profitable years in last 5, count losing years in last 5, and subtract.
        // So maximum score is 5, minimum is -5.
        public static double ProfitableYearsScore(double[] RecentProfitArray, 
                                                  out double ProfitableYearsScore)

        {
            var PositiveYears = RecentProfitArray.Where(n => n > 0).ToArray();
            var NegativeYears = RecentProfitArray.Where(n => n < 0).ToArray();
            ProfitableYearsScore = (PositiveYears.Length - NegativeYears.Length);
            return (ProfitableYearsScore);
        }

        // Net Revenue Change Score. Subtract recent revenue from oldest revenue. Score positively if higher, score
        // negatively if lower.
        public static double NetRevenueChangeScore(double[] RecentRevenueArray, 
                                                   out double NetRevenueChangeScore)
        
        {
            double RevenueDifference = RecentRevenueArray.ElementAt(0) - RecentRevenueArray.ElementAt(4);
            var RevenueChangeList = new List<double>();
            if (RevenueDifference > 0)
            {
                RevenueChangeList.Add(1);
            }
            else
            {
                RevenueChangeList.Add(-1);
            }
            double[] Revenue = RevenueChangeList.ToArray();
            NetRevenueChangeScore = Revenue.FirstOrDefault();
            return (NetRevenueChangeScore);
        }

        // Net Profit Change Score. Subtract recent profit from oldest profit. Score positively if higher, score
        // negatively if lower. Scored more strongly than Net Revenue Change Score.
        public static double NetProfitChangeScore(double[] RecentProfitArray, 
                                                  out double NetProfitChangeScore)

        {
            double ProfitDifference = RecentProfitArray.ElementAt(0) - RecentProfitArray.ElementAt(4);
            var ProfitChangeList = new List<double>();
            if (ProfitDifference > 0)
            {
                ProfitChangeList.Add(2);
            }
            else
            {
                ProfitChangeList.Add(-2);
            }
            double[] Profit = ProfitChangeList.ToArray();
            NetProfitChangeScore = Profit.FirstOrDefault();
            return (NetProfitChangeScore);
        }


    }
}
