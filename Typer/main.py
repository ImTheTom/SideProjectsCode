import random
import time

sessionWpm = []
sessionAccuracy = []

def GetLine():
    f = open("lines.txt","r")
    line = random.randint(0,40)
    return f.readlines()[line]

def Countdown():
    for x in range (0,5):  
        print ("Time will start in "+str(5-x), end="\r")
        time.sleep(1)
    print ("                     ", end="\r") #Clears the line

def GetAccuracy(line, readLine):
    total = 0
    lengthOfLine = len(line)
    for index in range(lengthOfLine):
        try:
            if(line[index]!=readLine[index]):
                total +=1
        except IndexError:
            total +=1
    numberCorrect = lengthOfLine-total
    return (numberCorrect/lengthOfLine)*100

def Speed(readLine, totalTime):
    minutes  = totalTime/60
    words = readLine.split()
    words = len(words)
    wpm = words/minutes
    return minutes,words,wpm

def PrintSessionScores():
    averageAccuracy = sum(sessionAccuracy)/len(sessionAccuracy)
    averageWPM = sum(sessionWpm)/len(sessionWpm)
    print("Session Accuracy: {0:.2f}%. Session Speed: {1:.2f} WPM.".format(averageAccuracy,averageWPM))

def RunThrough():
    line = GetLine()[:-2]
    print(line)
    Countdown()
    startTime = time.time()
    readLine = input()
    totalTime = time.time()-startTime
    accuracy = GetAccuracy(line, readLine)
    minutes,words,wpm = Speed(readLine, totalTime)
    print("You wrote {0} words in {1:.2f} seconds. Which is a speed of {2:.2f} per minute. With an accuracy of {3:.2f}%".format(words,totalTime,wpm,accuracy))
    sessionAccuracy.append(accuracy)
    sessionWpm.append(wpm)
    PrintSessionScores()


if __name__ == '__main__':
    running = True
    while(True):
        RunThrough()
        print('Another? Enter "n" to stop.')
        userInput = input()
        if(userInput=="n"):
            running = False
