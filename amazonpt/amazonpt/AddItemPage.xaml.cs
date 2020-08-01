using amazonpt.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace amazonpt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddItemPage : ContentPage
    {
        public AddItemPage()
        {
            InitializeComponent();
        }

        private async void confirm_Clicked(object sender, EventArgs e)
        {
            if (itemName.Text != string.Empty && price.Text != string.Empty && url.Text != string.Empty)
            {
                await FirebaseHelper.AddItem(itemName.Text, Convert.ToDouble(price.Text), url.Text);
                await Navigation.PopModalAsync();
            }
        }
    }
}