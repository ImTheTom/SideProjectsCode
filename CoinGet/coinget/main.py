from coinget.coinapi_v1 import CoinAPIv1
import os
import pickle
from datetime import datetime, timedelta
import sys

coinApiKey = ""

with open(os.path.join(os.path.dirname(__file__),'key.txt'),'r') as keyFile:
    coinApiKey = keyFile.readline()
    keyFile.close()

stringOfFollowedCoins = ""

with open(os.path.join(os.path.dirname(__file__),'coins.txt'), 'r') as coinFile:
    stringOfFollowedCoins = coinFile.readline()
    coinFile.close()

listOfFollowedCoins = stringOfFollowedCoins.split(' ')

coinAPI = CoinAPIv1(coinApiKey)
coinAssets = pickle.load(open(os.path.join(os.path.dirname(__file__),"assets.pkl"),"rb"))

def ShowHelp():
    return ("Current commands are:\n"
          "\tPrice [cryptocurrency] used to get price of a cryptocurrency. Example: Price Bitcoin\n"
          "\tDifference [cryptocurrency] [days] used to get the price change of N of days ago. Exmaple: Difference Bitcoin 7\n"
          "\tMovement used to get the price change of the coins listed in txt file 'coins.txt'. Example: Movement\n"
          "\tAdd [cryptocurrency] used to add a cryptocurrency to your followed txt file 'coins.txt'. Example: Add Bitcoin\n"
          "\tRemove [cryptocurrency] used to remove a cryptocurrency to your followed txt file 'coins.txt'. Example: Remove Bitcoin\n"
          "\tUpdate used to update the current list of coinAssets on your hard drive. Example: Update\n"
          "\tList used to get all the cryptocurrencies that can be followed. Example: List\n"
          "\tFollowing used to get all the cryptocurrencies that you are following. Example: Following\n"
          "\tReset used to remove all the cyrptocurrencies you are following. Example: Reset\n"
          "\tHelp used to show this message. Example: Help")

def UpdateAssets():
    newAssets = coinAPI.metadata_list_assets()
    pickle.dump(newAssets, open(os.path.join(os.path.dirname(__file__),"assets.pkl"),"wb"))

def CheckCoinExists(coinQuery):
    for asset in coinAssets:
        if(coinQuery==asset['name'].lower() and asset['type_is_crypto'] == 1):
            return asset
        elif(coinQuery==asset['asset_id'].lower() and asset['type_is_crypto'] == 1):
            return asset
    return "No"

def CheckPrice(coin):
    exchange_rate = coinAPI.exchange_rates_get_specific_rate(coin['asset_id'], 'USD')
    return exchange_rate['rate']

def CheckPriceChangeInLastHour(coin):
    query = 'BITSTAMP_SPOT_{0}_USD'.format(coin['asset_id'])
    changeData = coinAPI.ohlcv_latest_data(query, {'period_id': '1HRS'})
    return changeData[0]

def CheckChangeOfFollowedCoinsInPastDay():
    totalCoinChangeList = []
    for coin in listOfFollowedCoins:
        if(coin != ""):
            currentCoinList = []
            currentCoinList.append(coin)
            query = 'BITSTAMP_SPOT_{0}_USD'.format(coin)
            changeData = coinAPI.ohlcv_latest_data(query, {'period_id': '1DAY'})
            latestData = changeData[0]
            currentCoinList.append(latestData['price_open'])
            currentCoinList.append(latestData['price_close'])
            currentCoinList.append(round(((currentCoinList[2]/currentCoinList[1])-1)*100,2))
            totalCoinChangeList.append(currentCoinList)
    return totalCoinChangeList

def AddCoinToFollowedList(coin):
    global listOfFollowedCoins
    global stringOfFollowedCoins
    if(coin['asset_id'] not in listOfFollowedCoins):
        with open(os.path.join(os.path.dirname(__file__),'coins.txt'), 'a') as coinFile:
            coinFile.write(coin['asset_id']+ " ")
            coinFile.close()
        with open(os.path.join(os.path.dirname(__file__),'coins.txt'), 'r') as coinFile:
            stringOfFollowedCoins = coinFile.readline()
            coinFile.close()
        listOfFollowedCoins = stringOfFollowedCoins.split(' ')
        return "{0} was added to your followed coins".format(coin['name'])
    else:
        return "{0} was already in your followed coins".format(coin['name'])

def RemoveCoinFromFollowedList(coin):
    global listOfFollowedCoins
    global stringOfFollowedCoins
    if(coin['asset_id'] in listOfFollowedCoins):
        listOfFollowedCoins.remove(coin['asset_id'])
        stringOfFollowedCoins = " ".join(listOfFollowedCoins)
        with open(os.path.join(os.path.dirname(__file__),'coins.txt'), 'w') as coinFile:
            coinFile.write(stringOfFollowedCoins)
            coinFile.close()
        with open(os.path.join(os.path.dirname(__file__),'coins.txt'), 'r') as coinFile:
            stringOfFollowedCoins = coinFile.readline()
            coinFile.close()
        listOfFollowedCoins = stringOfFollowedCoins.split(' ')
        return "{0} was removed to your followed coins".format(coin['name'])
    else:
        return "{0} wasn't in your followed coins".format(coin['name'])

def RemoveAllFollowedCoins():
    global listOfFollowedCoins
    global stringOfFollowedCoins
    stringOfFollowedCoins = ""
    with open(os.path.join(os.path.dirname(__file__),'coins.txt'), 'w') as coinFile:
        coinFile.write(stringOfFollowedCoins)
        coinFile.close()
    listOfFollowedCoins = stringOfFollowedCoins.split(' ')
    return "Success"

def GetAllFollowedCoins():
    global stringOfFollowedCoins
    return stringOfFollowedCoins

def CheckPriceChange(coin, numberOfDays):
    currentPrice = CheckPrice(coin)
    today = datetime.now().isoformat()
    dateToCompare = (datetime.now().date()-timedelta(days = numberOfDays)).isoformat()
    previousPrice = coinAPI.exchange_rates_get_specific_rate(coin['asset_id'], 'USD', {'time': dateToCompare})
    if(currentPrice > previousPrice['rate']):
        return "{0} is up since {1} days ago. From {2} to {3} currently.".format(coin['name'], numberOfDays,str(previousPrice['rate']),str(currentPrice))
    elif(currentPrice < previousPrice['rate']):
        return "{0} is down since {1} days ago. From {2} to {3} USD currently.".format(coin['name'], numberOfDays, str(previousPrice['rate']),str(currentPrice))
    else:
        return "I couldn't find if it is up or down"

def GetCurrentCurriencies():
    string = "Current cryptocurrencies are: "
    for asset in coinAssets:
        if(asset['type_is_crypto'] == 1):
            string += "{0}. ".format(asset['name'])
    return string

def CheckUserInput(input):
    command = input[0].lower()
    if(len(input)==2):
        if(command == "price"):
            asset = CheckCoinExists(input[1])
            if(asset!="No"):
                price = CheckPrice(asset)
                return "{0} is currently trading at {1} USD.".format(asset["name"], price)
            else:
                return "Couldn't find the cryptocurrency."
        elif(command == "change"):
            asset = CheckCoinExists(input[1])
            if(asset!="No"):
                changeInfo = CheckPriceChangeInLastHour(asset)
                return "In the last hour {0} opened at {1} and closed at {2} USD.".format(asset["name"],changeInfo['price_open'],changeInfo['price_close'])
            else:
                return "Couldn't find the cryptocurrency."
        elif(command == "add"):
            asset = CheckCoinExists(input[1])
            if(asset!="No"):
                return  AddCoinToFollowedList(asset)
            else:
                return "Couldn't find the cryptocurrency."
        elif(command == "remove"):
            asset = CheckCoinExists(input[1])
            if(asset!="No"):
                return RemoveCoinFromFollowedList(asset)
            else:
                return "Couldn't find the cryptocurrency."
        else:
            return "Use command 'Help' for any help you may need"
    elif(len(input)==3):
        if(command == "difference"):
            asset = CheckCoinExists(input[1])
            if(asset != "No"):
                days = input[2]
                return CheckPriceChange(asset,int(days))
            else:
                return "Couldn't find your cryptocurrency"
        else:
            return "Use command 'Help' for any help you may need"
    elif(len(input) == 1):
        if(command == "update"):
            UpdateAssets()
            return "Updated the asset list."
        elif(command == "movement"):
            array = CheckChangeOfFollowedCoinsInPastDay()
            string = "Movement of followed coins is:"
            for coinResult in array:
                string += "\n{0} was at {1} yesterday and it is now at {2} USD which is a movement of {3}%.".format(coinResult[0],coinResult[1],coinResult[2],coinResult[3])
            return string
        elif(command == "list"):
            return GetCurrentCurriencies()
        elif(command == "reset"):
            return RemoveAllFollowedCoins()
        elif(command == "following"):
            return GetAllFollowedCoins()
        elif(command == "help"):
            return ShowHelp()
        else:
            return "Use command 'Help' for any help you may need"
    else:
        return "Use command 'Help' for any help you may need"

def run():
    userInput = sys.argv[1:]
    print(CheckUserInput(userInput))
