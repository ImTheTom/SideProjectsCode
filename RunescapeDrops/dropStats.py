"""This is the main script that is used for dropStats"""
from random import randint
import csv

item = ""
dropRate = ""

def StripCharacters(msg):
    msg = msg.replace("'","")
    msg = msg.replace("[","")
    msg = msg.replace("]","")
    return msg

def selectMonster():
    with open('textFiles/monsters.txt', newline = '') as inputfile:
        monsters = list(csv.reader(inputfile))

    totalLength = len(monsters)
    index = randint(0, totalLength-1)
    monster = monsters[index]
    monster = str(monster)
    monster = StripCharacters(monster)
    monster = monster+'.txt'
    return monster

def selectDrop(monster):
    global dropRate
    global item
    with open('textFiles/'+monster, newline = '') as inputfile:
        drops = list(csv.reader(inputfile))

    totalLength = len(drops)/2
    index = randint(0, totalLength-1) * 2

    item = drops[index]
    item = str(item)
    item = StripCharacters(item)

    dropRate = drops[index+1]
    dropRate = str(dropRate)
    dropRate = StripCharacters(dropRate)

def GetDrop(monster):
    global dropRate
    global item
    dropRate=eval(dropRate)

    numberOfKills = 0
    gotReward = False

    while(gotReward == False):
        numberOfKills+=1
        itemDrop = randint(1,dropRate)
        rewardDrop = 1
        if(rewardDrop == itemDrop):
            gotReward=True

    monster = monster[:-4]
    string = "Monster: " + monster + ". Item: " + item + ". Killcount: " + str(numberOfKills) + ". Drop Rate: " + str(dropRate) + ".\nWent to kill " + monster + '. For the drop of ' + item + '. Which has a drop rate of '+ str(dropRate) +'. I got it after ' + str(numberOfKills) +' kills.'
    return string

def RunFunctions():
    monster = selectMonster()
    selectDrop(monster)
    string = GetDrop(monster)
    return string
