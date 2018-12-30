import importlib.machinery
from twitter import *
from dropStats import *
import time
from records import *
from graphs import *
from upload import *

item = ""
dropRate = ""
statsString = ""

string = RunFunctions()

loader = importlib.machinery.SourceFileLoader('twittertokens','/home/pi/Desktop/Code/twittertokens.py')
handle = loader.load_module('twittertokens')

consumerKey = handle.consumerKey();

consumerSecretKey = handle.consumerSecretKey();

accessToken = handle.accessToken();

accessSecretToken = handle.accessSecretToken();

bot = Twitter(auth=OAuth(accessToken, accessSecretToken, consumerKey, consumerSecretKey))

def UpdateStatus():
    current = pickle.load(open("/home/pi/Desktop/Code/RunescapeDrops/storage/current.pkl","rb"))
    print('Bot Ready To Post in 1 Hour')
    string = RunFunctions()
    newlineStr = ".\n"
    index = string.find(newlineStr)
    index = index
    index = index+2
    stats = string [:index]
    twitterStatus = string[index:]
    textfile = open("textFiles/stats.txt", "a")
    stats = '\n' +stats
    textfile.write(stats)
    textfile.close
    bot.statuses.update(status=twitterStatus)
    print(twitterStatus)
    print(stats)
    x = CheckStatString(stats)
    if(x==1):
        TweetLow()
    elif (x==0):
        TweetHigh()
    if current > 50:
        RedoGraph()
        UploadGraph()
        current = 0
    current +=1
    pickle.dump(current, open("/home/pi/Desktop/Code/RunescapeDrops/storage/current.pkl","wb"))
    print(current)
    time.sleep(3600)

def TweetLow():
    time.sleep(60)
    lowString = ReturnLowString()
    print("New Lowest!")
    print(lowString)
    print("\n")
    bot.statuses.update(status=lowString)

def TweetHigh():
    time.sleep(60)
    highString = ReturnHighString()
    print("New Highest!")
    print(highString)
    print("\n")
    bot.statuses.update(status=highString)

def RedoGraph():
    time.sleep(6)
    ResetCounter()
    percentages = SetPercentages()
    x = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ]
    UpdateXValues(x, percentages)
    UpdateGraph(x)

def UploadGraph():
    time.sleep(6)
    client = SetUp()
    image = Upload(client)
    link = GetLink(image)
    bot.statuses.update(status="Link to a graph I made can be found here: " + link)
    print("Tweeted out a link: " + link)

while True:
    UpdateStatus()
