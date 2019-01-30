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
        public MarketService()
        {
            exchange = new Bittrex();
        }

        public Task<IEnumerable<Market>> GetMarkets()
        {
            return exchange.GetMarkets();
        }

        public Task<IEnumerable<Market>> GetUserMarkets(string UserId)
        {
            return exchange.GetMarkets(); // for user 

        }

        




    }
}
