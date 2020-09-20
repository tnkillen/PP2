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
using System.Globalization;

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
                    .PostAsync(new item() { ItemName = itemName, DesiredPrice = desiredPrice, ItemURL = itemURL, PriceAchived = false, BackgroundColor = "FloralWhite" });
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
                    ItemURL = item.Object.ItemURL,
                    PriceAchived = item.Object.PriceAchived,
                    BackgroundColor = "FloralWhite"
                    
                }).ToList();
                foreach (item listing in itemList)
                {
                    if (listing.PriceAchived == true)
                        listing.BackgroundColor = "LightGreen";
                }
                return itemList;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        public static async Task<bool> DeleteItem(item toDelete)
        {
            try
            {
                var SingleItemObject =
               (await firebase
                 .Child(Application.Current.Properties["PlayerId"].ToString())
                 .OnceAsync<item>()).Where(a => a.Object.ItemName == toDelete.ItemName)
                 .Where(a => a.Object.ItemURL == toDelete.ItemURL).FirstOrDefault(); ;

                await firebase
               .Child(Application.Current.Properties["PlayerId"].ToString())
               .Child(SingleItemObject.Key)
               .DeleteAsync();

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

    }
}
