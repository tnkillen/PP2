import requests
from bs4 import BeautifulSoup
import time


url = 'https://www.amazon.com/S8IN4O/dp/B07L5GDTYY/ref=sr_1_3?crid=1N6LKU38UPP19&dchild=1&keywords=kindle+oasis&qid=1595706601&sprefix=kindle+o%2Caps%2C172&sr=8-3'
headers = {
    "User-Agent": 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.89 Safari/537.36'}


def check_price():
    page = requests.get(url, headers=headers)

    soup = BeautifulSoup(page.content, 'html.parser')

    title = soup.find(id="productTitle").get_text()
    price = soup.find(id="priceblock_ourprice").get_text()
    converted_price = float(price[1:])
    if(converted_price < 360):
        send_mail()

    print(title.strip())
    print(converted_price)


def send_mail():
    data = {"app_id": "02fed716-2742-4d0e-b796-4d9b86c3dafb",
        "included_segments": ["All"], "contents": {"en": "An Item You're Watching Has Reached Your Desired Price!"}}
    requests.post("https://onesignal.com/api/v1/notifications",    headers={
              "Authorization": "Basic MzFkMzQ3ZDAtZGUyZC00OTUxLWI0ZTUtYzJmNjE2NGMxNWFk"},    json=data)


while(True):
    check_price()
    time.sleep(60 * 60)  # runs check every hour
