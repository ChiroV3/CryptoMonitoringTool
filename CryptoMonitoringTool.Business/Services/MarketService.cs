using CryptoMonitoringTool.Business.API;
using CryptoMonitoringTool.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMonitoringTool.Business.Services
{
    public class MarketService
    {
        private readonly IExchange exchange;

        public MarketService(IExchange exchange)
        {
            this.exchange = exchange;
        }
        
        /// <summary>
        /// returns list of tickers for given market names in supported format for example "LTC-BTC"
        /// </summary>
        public List<Ticker> GetTickersForMarketNames(List<string> marketNames)
        {
            List<Ticker> tickers = new List<Ticker>();
            foreach(string market in marketNames)
            {
                tickers.Add(exchange.GetTicker(market).Result);
            }

            return tickers;
        }
    }

}
