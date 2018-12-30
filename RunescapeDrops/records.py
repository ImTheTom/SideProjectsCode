import pickle
import csv
import re
import decimal

def StripCharacters(msg):
    msg = msg.replace("'","")
    msg = msg.replace("[","")
    msg = msg.replace("]","")
    return msg

def FindHighest():
    with open('textFiles/stats.txt', newline = '') as inputfile:
        stats = list(csv.reader(inputfile))
    highestPercent=0
    highestPercentIndex = 0
    i=0
    for a in stats:
        current = stats[i]
        current = StripCharacters(str(current))
        numbers = re.findall('\d+',current)
        if(len(numbers)==2):
            percent = int(numbers[0])/int(numbers[1])
            percent = decimal.Decimal(percent)
            percent=percent*100
            percent = round(percent,2)
            if percent > highestPercent:
                highestPercent=percent
                highestPercentIndex = i
            i+=1
        else:
            try:
                percent = int(numbers[1])/int(numbers[2])
                percent = decimal.Decimal(percent)
                percent=percent*100
                percent = round(percent,2)
                if percent > highestPercent:
                    highestPercent=percent
                    highestPercentIndex = i
                i+=1
            except:
                i+=1
    pickle.dump(highestPercent, open("/home/pi/Desktop/Code/RunescapeDrops/storage/highest.pkl","wb"))
    string = stats[highestPercentIndex]
    string = StripCharacters(str(string))
    StatusMaker(string, 1)
    return stats[highestPercentIndex]

def FindLowest():
    with open('textFiles/stats.txt', newline = '') as inputfile:
        stats = list(csv.reader(inputfile))
    lowestPercent=100
    lowestPercentIndex = 0
    i=0
    for a in stats:
        current = stats[i]
        current = StripCharacters(str(current))
        numbers = re.findall('\d+',current)
        if(len(numbers)==2):
            percent = int(numbers[0])/int(numbers[1])
            percent = decimal.Decimal(percent)
            percent=percent*100
            percent = round(percent,2)
            if percent < lowestPercent:
                lowestPercent=percent
                lowestPercentIndex = i
            i+=1
        else:
            try:
                percent = int(numbers[1])/int(numbers[2])
                percent = decimal.Decimal(percent)
                percent=percent*100
                percent = round(percent,2)
                if percent < lowestPercent:
                    lowestPercent=percent
                    lowestPercentIndex = i
                i+=1
            except:
                i+=1
    pickle.dump(lowestPercent, open("/home/pi/Desktop/Code/RunescapeDrops/storage/lowest.pkl","wb"))
    string = stats[lowestPercentIndex]
    string = StripCharacters(str(string))
    StatusMaker(string, 0)
    return stats[lowestPercentIndex]

def StatusMaker(string, type):
    overall = ""
    itemIndex = ". Item"
    killCountIndex = ". Killcount"
    itemIndex = string.find(itemIndex)
    killCountIndex = string.find(killCountIndex)
    actualMonster = string[9:itemIndex]
    actualItem = string[itemIndex+8:killCountIndex]
    if type==0:
        overall = "The new lowest killcount percent is on " + actualMonster + " for " + actualItem + ". "
    elif type==1:
        overall = "The new highest killcount percent is on " + actualMonster + " for " + actualItem + ". "
    numbers = re.findall('\d+',string)
    if(len(numbers)==2):
        overall = overall +"Dropped on kill " + numbers[0] +". The drop rate is 1 in "+ numbers[1] + "."
    else:
        overall = overall +"Dropped on kill " + numbers[1] +". The drop rate is 1 in "+ numbers[2] + "."
    if type==0:
        pickle.dump(overall, open("/home/pi/Desktop/Code/RunescapeDrops/storage/lowestString.pkl","wb"))
    elif type==1:
        pickle.dump(overall, open("/home/pi/Desktop/Code/RunescapeDrops/storage/highestString.pkl","wb"))
    return overall

def test():
    highest = pickle.load(open("/home/pi/Desktop/Code/RunescapeDrops/storage/highest.pkl","rb"))
    highestString = pickle.load(open("/home/pi/Desktop/Code/RunescapeDrops/storage/highestString.pkl","rb"))
    lowest = pickle.load(open("/home/pi/Desktop/Code/RunescapeDrops/storage/lowest.pkl","rb"))
    lowestString = pickle.load(open("/home/pi/Desktop/Code/RunescapeDrops/storage/lowestString.pkl","rb"))
    print(highest)
    print(highestString)
    print(lowest)
    print(lowestString)

def ReturnHighString():
    return pickle.load(open("/home/pi/Desktop/Code/RunescapeDrops/storage/highestString.pkl","rb"))

def ReturnLowString():
    return pickle.load(open("/home/pi/Desktop/Code/RunescapeDrops/storage/lowestString.pkl","rb"))

def reset():
    highest = 1
    lowest = 100
    highestString = ""
    lowestString = ""
    pickle.dump(highest, open("/home/pi/Desktop/Code/RunescapeDrops/storage/highest.pkl","wb"))
    pickle.dump(lowest, open("/home/pi/Desktop/Code/RunescapeDrops/storage/lowest.pkl","wb"))
    pickle.dump(lowestString, open("/home/pi/Desktop/Code/RunescapeDrops/storage/lowestString.pkl","wb"))
    pickle.dump(highestString, open("/home/pi/Desktop/Code/RunescapeDrops/storage/highestString.pkl","wb"))
    print("Stats Reset!")

def CheckStatString(test):
    numbers = re.findall('\d+',test)
    if(len(numbers)==2):
        percent = int(numbers[0])/int(numbers[1])
    else:
        percent = int(numbers[1])/int(numbers[2])
    percent = decimal.Decimal(percent)
    percent=percent*100
    percent = round(percent,2)
    percent = float(percent)
    lowest = pickle.load(open("/home/pi/Desktop/Code/RunescapeDrops/storage/lowest.pkl","rb"))
    highest = pickle.load(open("/home/pi/Desktop/Code/RunescapeDrops/storage/highest.pkl","rb"))
    if percent > highest:
        StatusMaker(test,1)
        pickle.dump(percent, open("/home/pi/Desktop/Code/RunescapeDrops/storage/highest.pkl","wb"))
        return 0
    elif percent < lowest:
        StatusMaker(test,0)
        pickle.dump(percent, open("/home/pi/Desktop/Code/RunescapeDrops/storage/lowest.pkl","wb"))
        return 1
    else:
        return 2
    