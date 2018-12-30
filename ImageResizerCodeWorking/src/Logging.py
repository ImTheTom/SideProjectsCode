import datetime
from src import HelpMessages

def StartUpLog():
    try:
        logFile = open("src/logs.txt", "a")
        now = datetime.datetime.now()
        logFile.write(now.strftime("%Y-%m-%d %H:%M")+" Image Resizer started!\n")
        logFile.close()
    except:
        HelpMessages.CouldNotPerformLog()

def ThumbnailCreated(file):
    try:
        logFile = open("src/logs.txt", "a")
        now = datetime.datetime.now()
        logFile.write(now.strftime("%Y-%m-%d %H:%M")+" "+str(file)+" thumbnail was created\n")
        logFile.close()
    except:
        HelpMessages.CouldNotPerformLog()

def ShutDownLog():
    try:
        logFile = open("src/logs.txt", "a")
        now = datetime.datetime.now()
        logFile.write(now.strftime("%Y-%m-%d %H:%M")+" program shut down successfully\n")
        logFile.close()
    except:
        HelpMessages.CouldNotPerformLog()
