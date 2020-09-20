using amazonpt.Models;
using amazonpt.Services;
using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Text;
using amazonpt.Helpers;
using System.Windows.Input;

namespace amazonpt.Views
{
    public class ItemView : BaseFodyObservable
    {
        // Main page.xaml's Collection View Template Items
        public ObservableCollection<item> WatchList { get; set; }
        public string ItemName { get; set;}
        public double DesiredPrice { get; set; }
        public string ItemUrl { get; set; }
        public ICommand RefreshCommand { get; }
        public bool IsRefreshing { get; set; }
        //Constructor
        public ItemView()
        {
            GetWatchItems().ContinueWith(t => { WatchList = new ObservableCollection<item>(t.Result); });
            RefreshCommand = new Command(async () => await ExecuteRefreshCommand());
        }
        public async Task RefreshItems()
        {
            await GetWatchItems().ContinueWith(t => { WatchList = new ObservableCollection<item>(t.Result); });
        }

        private async Task<List<item>> GetWatchItems()
        {
            return (await FirebaseHelper.GetWatchItems());
        }

        async Task ExecuteRefreshCommand()
        {
            await RefreshItems();
            IsRefreshing = false;
        }
       
    }
}
