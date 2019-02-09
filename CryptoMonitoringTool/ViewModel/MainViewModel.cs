using CryptoMonitoringTool.Business.API;
using CryptoMonitoringTool.Business.Models;
using CryptoMonitoringTool.Business.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CryptoMonitoringTool.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Bittrex bittrex = new Bittrex();
            MarketService = new MarketService(bittrex);
            CryptoCollection = new ObservableCollection<Ticker>();
            CryptoMarketNames = new List<string>();
            BackgroundChecking();
            Title = "NASZA APKA";

            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        public MarketService MarketService { get; set; }

        #region Properties
        private string _title;
        public string Title
        {

            get
            {
                return _title;
            }
            set
            {
                if (value != _title)
                {
                    _title = value;
                    RaisePropertyChanged("Title");
                }
            }
        }

        private string _currentCryptoCurrency;
        public string CurrentCryptoCurrency
        {

            get
            {
                return _currentCryptoCurrency;
            }
            set
            {
                if (value != _currentCryptoCurrency)
                {
                    _currentCryptoCurrency = value;
                    RaisePropertyChanged("CurrentCryptoCurrency");
                }
            }
        }

        private List<string> _cryptoMarketNames;
        public List<string> CryptoMarketNames
        {

            get
            {
                return _cryptoMarketNames;
            }
            set
            {
                if (value != _cryptoMarketNames)
                {
                    _cryptoMarketNames = value;
                    RaisePropertyChanged("CryptoMarketNames");
                }
            }
        }

        private ObservableCollection<Ticker> _cryptoCollection;
        public ObservableCollection<Ticker> CryptoCollection
        {

            get
            {
                return _cryptoCollection;
            }
            set
            {
                if (value != _cryptoCollection)
                {
                    _cryptoCollection = value;
                    RaisePropertyChanged("CryptoCollection");
                }
            }
        }

        #endregion

        #region COMMANDS

        private RelayCommand _addCryptoCurrencyCommand;

        public RelayCommand AddCryptoCurrencyCommand
        {
            get
            {

                return _addCryptoCurrencyCommand
                    ?? (_addCryptoCurrencyCommand = new RelayCommand(
                    async () =>
                    {
                        CryptoMarketNames.Add(CurrentCryptoCurrency);
                    }
                    ));
            }

        }
        #endregion

        #region Methods

        public async void BackgroundChecking()
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    var collection =  MarketService.GetTickersForMarketNames(CryptoMarketNames);
                    CryptoCollection.Clear();
                    foreach (var ticker in collection)
                    {
                        CryptoCollection.Add(ticker);
                    }
                    await Task.Delay(60000);
                }
            });

        }
        #endregion
    }
}