using CryptoMonitoringTool.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMonitoringTool.Business.API
{
    public class Bittrex : ApiCall, IExchange
    {
        public const string Version = "v1.1";
        public const string BaseUrl = "https://bittrex.com/api/" + Version + "/";

        public Bittrex()
        {
        }

        public Bittrex(string apiKey, string apiSecret) : base(apiKey, apiSecret)
        {

        }

        #region Public Api
        public virtual async Task<IEnumerable<Market>> GetMarkets()
        {
            var uri = BaseUrl + "public/getmarkets";
            var jsonResponse = await Request(HttpMethod.Get, uri, false);
            var markets = jsonResponse.ToObject<IEnumerable<Market>>();
            return markets;
        }

        public virtual async Task<IEnumerable<SupportedCurrency>> GetSupportedCurrencies()
        {
            var uri = BaseUrl + "public/getcurrencies";
            var jsonResponse = await Request(HttpMethod.Get, uri, false);
            var supportedCurrencies = jsonResponse.ToObject<IEnumerable<SupportedCurrency>>();
            return supportedCurrencies;
        }

        public virtual async Task<Ticker> GetTicker(string marketName)
        {
            var uri = BaseUrl + "public/getticker";
            var parameters = new Dictionary<string, string>
            {
                { "market", marketName }
            };
            var jsonResponse = await Request(HttpMethod.Get, uri, parameters, false);
            var ticker = jsonResponse.ToObject<Ticker>();
            if (ticker == null) return null;
            ticker.MarketName = marketName;
            return ticker;
        }

        public virtual async Task<IEnumerable<MarketSummary>> GetMarketSummaries()
        {
            var uri = BaseUrl + "public/getmarketsummaries";
            var jsonResponse = await Request(HttpMethod.Get, uri, false);
            var marketSummaries = jsonResponse.ToObject<IEnumerable<MarketSummary>>();
            return marketSummaries;
        }

        public virtual async Task<MarketSummary> GetMarketSummary(string marketName)
        {
            var uri = BaseUrl + "public/getmarketsummary";
            var parameters = new Dictionary<string, string>
            {
                { "market", marketName }
            };
            var jsonResponse = await Request(HttpMethod.Get, uri, parameters, false);
            var marketSummary = jsonResponse.ToObject<MarketSummary>();
            return marketSummary;
        }

        #endregion

    }
}
