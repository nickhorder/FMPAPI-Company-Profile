using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMPAPI
{
    class Exchanges
    {
        // ExchangeMethod - filter symbols from StockList API based on Exchanges we are interested in results from.
        public static string ExchangeMethod(StockListRepository obj, 
                                            out string SelectedCompanies)
        {

                SelectedCompanies = obj.ExchangeShortName switch
                {
                    "TSX" => obj.Symbol,
                    "JPX" => obj.Symbol,
                    "CPH" => obj.Symbol,
                    "SEK" => obj.Symbol,
                    "OSE" => obj.Symbol,
                    "TLV" => obj.Symbol,
                    "PRA" => obj.Symbol,
                    "LIS" => obj.Symbol,
                    "ATH" => obj.Symbol,
                    "BUD" => obj.Symbol,
                    "SIX" => obj.Symbol,
                    "VIE" => obj.Symbol,
                    "WSE" => obj.Symbol,
                    "LSE" => obj.Symbol,
                    "EURONEXT" => obj.Symbol,
                    "XETRA" => obj.Symbol,
                    "MIL" => obj.Symbol,
                    "NYSE" => obj.Symbol,
                    "NASDAQ" => obj.Symbol,
                    _ => "Other Exchange"
                };
                return (SelectedCompanies);
            }
        }
    }


