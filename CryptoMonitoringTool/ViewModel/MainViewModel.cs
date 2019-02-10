using CryptoMonitoringTool.Business.Models;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
            Title = "NASZA APKA";

            CryptoCollection = new ObservableCollection<Ticker>();
            
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        public ObservableCollection<Ticker> GetCryptoCollection(List<string> marketNames)
        {
            //return apiservice.Getcollection(marketnames)
        }

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
    }
}