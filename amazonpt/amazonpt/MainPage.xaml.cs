using amazonpt.Helpers;
using amazonpt.Views;
using amazonpt.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace amazonpt
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        item selectedItem;
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new ItemView();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as ItemView).RefreshItems();
            selectedItem = null;
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = (e.CurrentSelection.FirstOrDefault() as item);
        }


        private async void GoToAddItem()
        { 
            var newItemPage = new AddItemPage();
            await Navigation.PushModalAsync(newItemPage);
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            GoToAddItem();
        }
    }
}
