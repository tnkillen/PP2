using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Query;
using System.Diagnostics;
using Xamarin.Forms;
using amazonpt.Models;

namespace amazonpt.Helpers
{
    public class FirebaseHelper
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

    }
}
