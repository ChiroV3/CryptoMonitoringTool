using CryptoMonitoringTool.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMonitoringTool.Business.API
{
    public interface IExchange
    {
        #region Public Api
        /// <summary>
        /// returns list of markets 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Market>> GetMarkets();

        /// <summary>
        /// returns list of supported currencies in implemented exchange
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SupportedCurrency>> GetSupportedCurrencies();

        /// <summary>
        /// returns current bid, ask and last prices for marketname
        /// </summary>
        /// <param name="marketName"> for example: BTC-LSK</param>
        /// <returns></returns>
        Task<Ticker> GetTicker(string marketName);

        /// <summary>
        /// get summary of all markets within last 24 hours
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<MarketSummary>> GetMarketSummaries();

        /// <summary>
        /// get summary for marketname within last 24 hours
        /// </summary>
        /// <param name="marketName">for example: BTC-LSK</param>
        /// <returns></returns>
        Task<MarketSummary> GetMarketSummary(string marketName);

        #endregion
    }
}

