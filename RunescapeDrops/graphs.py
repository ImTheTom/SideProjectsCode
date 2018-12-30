import pickle
import matplotlib.pyplot as plt
import csv
import re
import decimal
import numpy as np

def StripCharacters(msg):
    msg = msg.replace("'","")
    msg = msg.replace("[","")
    msg = msg.replace("]","")
    return msg

def ResetCounter():
    count = 0
    print("Reset Counter!")
    pickle.dump(count, open("/home/pi/Desktop/Code/RunescapeDrops/storage/current.pkl","wb"))

def myround(x, base=25):
    return int(base * round(float(x)/base))

def SetPercentages():
    with open('textFiles/stats.txt', newline = '') as inputfile:
        stats = list(csv.reader(inputfile))
    i=0
    percentages = []
    for a in stats:
        current = stats[i]
        current = StripCharacters(str(current))
        numbers = re.findall('\d+',current)
        if(len(numbers)==2):
            percent = int(numbers[0])/int(numbers[1])
            percent = decimal.Decimal(percent)
            percent=percent*100
            percent = round(percent,2)
            percent = float(percent)
            percent = myround(percent)
            if percent>250:
                percent=250
            percentages.append(percent)
        else:
            try:
                percent = int(numbers[1])/int(numbers[2])
                percent = decimal.Decimal(percent)
                percent=percent*100
                percent = round(percent,2)
                percent = float(percent)
                percent = myround(percent)
                if percent>250:
                    percent=250
                percentages.append(percent)
            except:
                print('failed')
        i+=1
    return percentages

def UpdateXValues(emptyX, percentages):
    i=0
    for a in percentages:
        current = percentages[i]
        if current == 0:
            emptyX[0]+=1
        elif current == 25:
            emptyX[1]+=1
        elif current == 50:
            emptyX[2]+=1
        elif current == 75:
            emptyX[3]+=1
        elif current == 100:
            emptyX[4]+=1
        elif current == 125:
            emptyX[5]+=1
        elif current == 150:
            emptyX[6]+=1
        elif current == 175:
            emptyX[7]+=1
        elif current == 200:
            emptyX[8]+=1
        elif current == 225:
            emptyX[9]+=1
        elif current == 250:
            emptyX[10]+=1
        i+=1

def UpdateGraph(x):
    y = [0, 25, 50, 75, 100, 125, 150, 175, 200, 225, 250]
    width = 12.5
    fig = plt.figure()
    fig.suptitle('Number A Drop Occured Verus The Percentage Of The Drop Rate', fontsize=12, fontweight='normal')
    ax = fig.add_subplot(111)
    ax.bar(y, x, width, color="blue")
    ax.set_xlabel('Percentage the Drop Occured for the Item')
    ax.set_ylabel('Number of Occurrence')
    plt.savefig('storage/graph.png')
    ResetCounter()
