import sys
import os

def ShowHelp():
    return "To set key type 'set [key]' and to get the key type: 'get'"

def SetKey(key):
    with open(os.path.join(os.path.dirname(__file__),'key.txt'), 'w') as keyFile:
        keyFile.write(key)
        keyFile.close()
    return "Key updated"

def GetKey():
    coinApiKey = ""
    with open(os.path.join(os.path.dirname(__file__),'key.txt'),'r') as keyFile:
        coinApiKey = keyFile.readline()
        keyFile.close()
    return coinApiKey

def CheckUserInput(input):
    if(input[0]=="set"):
        return SetKey(input[1])
    elif(input[0]=="get"):
        return GetKey()
    else:
        return ShowHelp()

def run():
    userInput = sys.argv[1:]
    print(CheckUserInput(userInput))
