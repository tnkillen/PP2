using amazonpt.Helpers;
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
        public MainPage()
        {
            InitializeComponent();
        }

        private async void confirm_Clicked(object sender, EventArgs e)
        {
            if (itemName.Text != string.Empty && price.Text != string.Empty && url.Text != string.Empty)
            {
                await FirebaseHelper.AddItem(itemName.Text, Convert.ToDouble(price.Text), url.Text);
                itemName.Text = string.Empty;
                price.Text = string.Empty;
                url.Text = string.Empty;
            }
        }
    }
}
