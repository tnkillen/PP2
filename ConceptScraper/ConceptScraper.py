import requests
from requests.exceptions import MissingSchema, InvalidURL, ChunkedEncodingError, ConnectionError, ConnectTimeout
from bs4 import BeautifulSoup
import time
import six
import re
import firebase_admin
from firebase_admin import credentials
from firebase_admin import firestore
from firebase_admin import db

# Firebase Setup
cred = credentials.Certificate('./ServiceKeys.json')
default_app = firebase_admin.initialize_app(cred, {'databaseURL': 'https://tracking-51514.firebaseio.com/'})
root = db.reference()

# Webpage Setup
headers = {
    "User-Agent": 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.89 Safari/537.36'}
# Class setup
class Listing:
    def __init__(self, listingKey, userid, url, desiredPrice, itemName, priceAchived):
        self.listingKey = listingKey
        self.userid = userid
        self.url = url
        self.desiredPrice = desiredPrice
        self.itemName = itemName
        self.tryCount = 0
        self.priceAchived = priceAchived

def check_price(listingKey, userid, url, desiredPrice, itemName, priceAchived):
    try:
        try:
            page = requests.get(url, headers=headers)
        except (MissingSchema, InvalidURL):
            send_mail(userid,"Your Watch Listing for " + itemName + " Has an Invalid URL")
            return 0
        soup = BeautifulSoup(page.content, 'html.parser')
        #try:
        #     title = soup.find(id="productTitle").get_text()
        #except TypeError:
        #    try:
        #        title = soup.find(id="productTitle").get_text()
        try:
            price = soup.find(id="priceblock_ourprice").get_text()
        except AttributeError:
            try:
                price = soup.find(id="newBuyBoxPrice").get_text()
            except AttributeError:
                try:
                    price = soup.find(id="price_inside_buybox").get_text()
                except AttributeError:
                    try:
                        price = soup.find(id="price").get_text()
                    except AttributeError:
                        return Listing(listingKey, userid, url, desiredPrice, itemName, priceAchived)
    except (ChunkedEncodingError, ConnectionError, ConnectTimeout) as e:
        print("ERROR: " + str(e))
        return 1

    if isinstance(price, six.string_types):
        price = float(re.sub(r'[^0-9\.]','', price))

    converted_price = float(price)
    
    if converted_price <= desiredPrice and priceAchived == False:
        send_mail(userid, itemName + " Has Dropped in Price on Amazon!")
        listingRef = db.reference(userid).child(listingKey)
        listingRef.update({'priceAchived' : True})
    
    if converted_price > desiredPrice and priceAchived == True:
        send_mail(userid, itemName + " Has Increased in Price on Amazon!")
        listingRef = db.reference(userid).child(listingKey)
        listingRef.update({'priceAchived' : False})
   

    return 0

def send_mail(userid, message):
    data = {"app_id": "02fed716-2742-4d0e-b796-4d9b86c3dafb",
        "include_player_ids": [userid], "contents": {"en":message}}
    requests.post("https://onesignal.com/api/v1/notifications",    headers={
              "Authorization": "Basic MzFkMzQ3ZDAtZGUyZC00OTUxLWI0ZTUtYzJmNjE2NGMxNWFk"},    json=data)

def run_listings():
   failedListings = []
   firebaseFetch = root.get()
   for userid,itemInfo in firebaseFetch.items():
      for key in itemInfo:
          hasReturned = check_price(key, userid, itemInfo[key]['itemURL'], itemInfo[key]['desiredPrice'], itemInfo[key]['itemName'], itemInfo[key]['priceAchived'])
          if type(hasReturned) is Listing:
              failedListings.append(hasReturned)
         
              

   while(len(failedListings) > 0):
       for x in failedListings:
           hasReturned = check_price(x.listingKey, x.userid, x.url, x.desiredPrice, x.itemName, x.priceAchived)
           if hasReturned == 0:
               failedListings.remove(x)
           elif x.tryCount >= 3:
                print("Check Price Scrape Exceptions on : " + x.url)
                failedListings.remove(x)
           else:
               x.tryCount += 1



while(True):
    print("\nChecking Prices for all Users: \n")
    run_listings()
    print("\nPrice Check Completed \n")
    time.sleep(60 * 60)  # runs check every hour


        
    