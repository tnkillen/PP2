﻿using amazonpt.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public AddItemPage(string link)
        {
            InitializeComponent();
            url.Text = link;
        }
        private async void confirm_Clicked(object sender, EventArgs e)
        {
            if (itemName.Text != string.Empty && price.Text != string.Empty && url.Text != string.Empty)
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                itemName.Text = textInfo.ToTitleCase(itemName.Text);
                
                await FirebaseHelper.AddItem(itemName.Text, Convert.ToDouble(price.Text), url.Text);
                await Navigation.PopModalAsync();
            }
        }
    }
}