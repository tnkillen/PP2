using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Query;
using System.Diagnostics;
using Xamarin.Forms;
using amazonpt.Models;
using System.Linq;

namespace amazonpt.Helpers
{
    public class FirebaseHelper : ContentPage
    {
        // Connects to the Firebase DataBase
        public static FirebaseClient firebase = new FirebaseClient("https://tracking-51514.firebaseio.com");
        private static string storage = "tracking-51514.appspot.com";

        // add new item to firebase
        public static async Task<bool> AddItem(string itemName, double desiredPrice, string itemURL)
        {

            try
            {
                await firebase
                    .Child(Application.Current.Properties["PlayerId"].ToString())
                    .PostAsync(new item() { ItemName = itemName, DesiredPrice = desiredPrice, ItemURL = itemURL });
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        public static async Task<List<item>> GetWatchItems()
        {
            try
            {
                var itemList = (await firebase
                .Child(Application.Current.Properties["PlayerId"].ToString())
                .OnceAsync<item>()).Select(item =>
                new item
                {
                    ItemName = item.Object.ItemName,
                    DesiredPrice = item.Object.DesiredPrice,
                    ItemURL = item.Object.ItemURL
                }).ToList();
                return itemList;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

    }
}
