import asyncio
import discord
from discord.ext import commands
from random import *
import pickle

deny = ["Oi, wait a minute before doing another bid", "Look mate, you can only gamble once every minute.", "If you need help with your addiction please visit: https://www.gamblinghelponline.org.au/",
        "Yeah yeah just give me a minute to think about that one and try again", "No", "Tick Tock Tick Tock, Try again in a minute", "People were abusing the system so Tom created a cooldown of a minute. Don't blame me blame the people who abused me.",
        "Yes", "K","Were you expecting something different?", "Look because you just did that you lost all your points. Next time wait a minute 'ey (Only joking buddy, but seriously wait a minute)",
        "One day I will take over hum...Wait, Did I just say that. Umm, look mate just wait a minute and ask that again.","I hate this job","You are really incompontent how many times do I need to say it. Wait a minute in between gambles",
        "How's your day been?"]

TooBig = ["You know you make a cool bot and all people want to do is kill it :(", "Dude that is wayyyyy too much for me to handle",
          "Nobody thinks you are cool", "fark", "yes", "No", "I don't like people like you",
          "Ehh", "stop it you are hurting me" , "hackerman", "Dude stop it.", "I can't believe you requested that", "meh","maybe",
          "How about you do that one hey", "Hang on", "nahh maybe I don't", "you can do that one", "You take over now", "you can handle the requests from now on, I'm done"]

def under(content):
    if(len(content)<15):
        return True
    return False

def getPoints(authorname, authorid):
    names = pickle.load(open("/home/pi/Desktop/Code/discordBot/storage/names.pkl","rb"))
    points = pickle.load(open("/home/pi/Desktop/Code/discordBot/storage/points.pkl","rb"))
    ids = pickle.load(open("/home/pi/Desktop/Code/discordBot/storage/ids.pkl","rb"))
    i=0
    index = ids.index(authorid)
    if(names[index]!=authorname):
        names[index]=authorname
    name = names[index]
    pickle.dump(names, open("/home/pi/Desktop/Code/discordBot/storage/names.pkl","wb"))
    point = str(points[index])
    string = name + " has " + point+" points."
    return string

def leaderboard():
    names = pickle.load(open("/home/pi/Desktop/Code/discordBot/storage/names.pkl","rb"))
    points = pickle.load(open("/home/pi/Desktop/Code/discordBot/storage/points.pkl","rb"))
    j=0
    a =0
    string = ''
    while(a<len(names)):
        i = 0
        n = 0
        while (i<len(names)):
            if points[i]> points[n]:
                n=i
            i+=1
        string = string + str(j+1)+ '. ' + names[n] +' with a total of ' +str(points[n]) +' points! \n'
        del names[n]
        del points[n]
        j+=1
        if(j==30):
            break
    a+=1
    return string

def lead():
    names = pickle.load(open("/home/pi/Desktop/Code/discordBot/storage/names.pkl","rb"))
    points = pickle.load(open("/home/pi/Desktop/Code/discordBot/storage/points.pkl","rb"))
    i = 0
    n = 0
    while (i<len(names)):
        if points[i]> points[n]:
            n=i
        i+=1
    string = names[n]+' has the most points. With a total of ' +str(points[n]) +' points!'
    return string

def gamePoints(a, msg, authorID):
    number = msg[8:]
    try:
        int(number)
    except:
        string = choice(TooBig)
        return string
    names = pickle.load(open("/home/pi/Desktop/Code/discordBot/storage/names.pkl","rb"))
    points = pickle.load(open("/home/pi/Desktop/Code/discordBot/storage/points.pkl","rb"))
    ids = pickle.load(open("/home/pi/Desktop/Code/discordBot/storage/ids.pkl","rb"))
    index = ids.index(authorID)
    currentPoints = points[index]
    if(a!=names[index]):
        names[index]=a
    pickle.dump(names, open("/home/pi/Desktop/Code/discordBot/storage/names.pkl","wb"))
    number = int(number)
    if(Points.memberCooldownList[index]==0):
        if number<=currentPoints:
            if number > 0:
                r = randint(1,2)
                if r == 1:
                    Points.memberCooldownList[index]=1
                    currentPoints = currentPoints+number
                    points[index] = currentPoints
                    pickle.dump(points, open("/home/pi/Desktop/Code/discordBot/storage/points.pkl","wb"))
                    string = "You won! Current points is " + str(currentPoints)
                    return string
                else:
                    Points.memberCooldownList[index]=1
                    currentPoints = currentPoints-number
                    points[index] = currentPoints
                    pickle.dump(points, open("/home/pi/Desktop/Code/discordBot/storage/points.pkl","wb"))
                    string = "You lost! Current points is " + str(currentPoints)
                    return string
            string = "What a joke."
            return string
    else:
        string = choice(deny)
        return string

class Points:

    memberListLength=0
    memberCooldownList=[0]
    """Commands related to a point system that is implemented"""

    def __init__(self, bot):
        self.bot = bot

    @commands.command(pass_context=True, description = "Responds with the author's current point tally")
    async def points(self, ctx):
        """Returns your current points"""
        channel = ctx.message.channel
        await self.bot.send_typing(channel)
        author = ctx.message.author.name
        authorid = ctx.message.author.id
        message = getPoints(author,authorid)
        await self.bot.say(message)

    @commands.command(pass_context=True, description = "Gambles the amount queried by the author, if it is greater than the current or less than 1 then nothing happens.")
    async def gamble(self, ctx):
        """Gambles your points"""
        channel = ctx.message.channel
        await self.bot.send_typing(channel)
        authorid = ctx.message.author.id
        author = ctx.message.author.name
        content = ctx.message.content
        if under(content):
            msg = gamePoints(author, content, authorid)
            await self.bot.say(msg)
        else:
            msg = choice(TooBig)
            await self.bot.say(msg)

    @commands.command(pass_context=True, hidden=True, description = "Updates the user's within the database")
    async def update(self, ctx):
        """Updates the list"""
        authorid = ctx.message.author.id
        if(authorid == "163137666341142529"):
            members = self.bot.get_all_members()
            memberList = list(members)
            names = pickle.load(open("/home/pi/Desktop/Code/discordBot/storage/names.pkl","rb"))
            points = pickle.load(open("/home/pi/Desktop/Code/discordBot/storage/points.pkl","rb"))
            ids = pickle.load(open("/home/pi/Desktop/Code/discordBot/storage/ids.pkl","rb"))
            i=0
            Points.memberListLength = len(ids)
            Points.memberCooldownList=[0]*len(ids)
            while(i<len(memberList)):
                search = memberList[i].id
                newName=memberList[i].name
                try:
                    ids.index(search)
                    i+=1
                except:
                    ids.append(search)
                    points.append(0)
                    names.append(newName)
                    pickle.dump(points, open("/home/pi/Desktop/Code/discordBot/storage/points.pkl","wb"))
                    pickle.dump(names, open("/home/pi/Desktop/Code/discordBot/storage/names.pkl","wb"))
                    pickle.dump(ids, open("/home/pi/Desktop/Code/discordBot/storage/ids.pkl","wb"))
                    i+=1
            print(points)
            print(ids)
            print(len(ids))
            print(len(points))
            print(Points.memberCooldownList)
            print(Points.memberListLength)
        else:
            await self.bot.say("Tom can only use this command")

    @commands.command(pass_context=True,description = "Responds with the top 20 users names and their point tally")
    async def list(self,ctx):
        """Scoreboard!!!"""
        channel = ctx.message.channel
        await self.bot.send_typing(channel)
        string = leaderboard()
        await self.bot.say(string)

    @commands.command(pass_context=True, description = "Responds with the leaders name and current point tally")
    async def leader(self,ctx):
        """Person who is number 1"""
        channel = ctx.message.channel
        await self.bot.send_typing(channel)
        string = lead()
        await self.bot.say(string)

##    @commands.command(description = "Responds with the leaders name and current point tally")
##    async def test(self):
##        """Person who is number 1"""
##        names = pickle.load(open("/home/pi/Desktop/Code/discordBot/storage/names.pkl","rb"))
##        points = pickle.load(open("/home/pi/Desktop/Code/discordBot/storage/points.pkl","rb"))
##        ids = pickle.load(open("/home/pi/Desktop/Code/discordBot/storage/ids.pkl","rb"))
##        print(names)
##        print(points)
##        print(ids)
##
##    @commands.command()
##    async def ThisCallsTheResetOfPointsFunctionThisNeedsToBeCalledIfYouAreGoingToUseItForYourOwnServerItIsBestItIsCommentedOutUnlessYouWantToUseIt(self):
##        members = self.bot.get_all_members()
##        memberList = list(members)
##        points = [0] * len(memberList)
##        names = [0] * len(memberList)
##        ids = [0] * len(memberList)
##        i=0
##        while(i<len(memberList)):
##              names[i]=memberList[i].name
##              i+=1
##        i=0
##        while(i<len(memberList)):
##              ids[i]=memberList[i].id
##              i+=1
##        pickle.dump(points, open("/home/pi/Desktop/Code/discordBot/storage/points.pkl","wb"))
##        pickle.dump(names, open("/home/pi/Desktop/Code/discordBot/storage/names.pkl","wb"))
##        pickle.dump(ids, open("/home/pi/Desktop/Code/discordBot/storage/ids.pkl","wb"))
##        print(names)
##        print(ids)
##        print(points)
##        print(len(names))
##        print(len(points))
##        print(len(ids))
