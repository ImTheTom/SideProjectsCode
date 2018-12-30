#import list
import asyncio
import discord
from discord.ext import commands
import importlib.machinery
from datetime import datetime
import datetime
from random import *
from re import findall, match, search
import requests
import json
import codecs
import pickle

TooBig = ["You know you make a cool bot and all people want to do is kill it :(", "Dude that is wayyyyy too much for me to handle",
          "Nobody thinks you are cool", "fark", "yes", "No", "I don't like people like you",
          "Ehh", "stop it, you are hurting me" , "hackerman", "Dude stop it.", "I can't believe you requested that", "meh","maybe",
          "How about you do that one hey", "Hang on", "nahh maybe I don't", "you can do that one", "You take over now", "you can handle the requests from now on, I'm done"]

def under(content):
    if(len(content)<50):
        return True
    return False

def whatMonth(month):
    if(month == 1):
        return "January"
    elif(month == 2):
        return "February"
    elif(month == 3):
        return "March"
    elif(month == 4):
        return "April"
    elif(month == 5):
        return "May"
    elif(month == 6):
        return "June"
    elif(month == 7):
        return "July"
    elif(month == 8):
        return "August"
    elif(month == 9):
        return "September"
    elif(month == 10):
        return "October"
    elif(month == 11):
        return "November"
    elif(month == 12):
        return "December"
    else:
        return month

def whatDaySuffix(day):
    if(day==1):
        return "st"
    elif(day == 2):
        return "nd"
    elif(day == 3):
        return "rd"
    elif(day == 21):
        return "st"
    elif(day == 22):
        return "nd"
    elif(day == 23):
        return "rd"
    elif(day == 31):
        return "st"
    else:
        return "th"
    

#looks up if a streamer is online and returns yes if s/he is live or no if s/he isn't
def livegen(msg):
    loader = importlib.machinery.SourceFileLoader('token','/home/pi/Desktop/Code/token.py')
    handle = loader.load_module('token')
    client_id = handle.twitchClientId()
    twitch_api_stream_url = "https://api.twitch.tv/kraken/streams/" \
                    + msg + "?client_id=" + client_id
    streamer_html = requests.get(twitch_api_stream_url)
    streamer = json.loads(streamer_html.content.decode('utf-8'))
    if (streamer["stream"] == None):
        live = "no"
        return live
    else:
        live = "yes"
        return live

#takes the rest of the message and adds stabs to the front of it
def stab(msg):
    try:
        msg = "*stabs "+msg+"*"
        return msg
    except:
        msg = "Uhh something went wrong. More infomation about this command can be found by typing '!help stab'."
        return msg
    
#magic 8Ball function, returns one of the arraries
def magic8Ball():
    results =["It is certain","Without a doubt", "Yeah cunt maybe", "Yeah the boys it is","Ask again later","Yeah...Nahh...","Very doubtful","I must say no dude"]
    randomInt = randint(0,7)
    return results[randomInt]

#Flips a coin and returns which side it landed on
def flipCoin():
    results =["Coin landed on Heads.", "Coin landed on Tails."]
    randomInt = randint(0,1)
    return results[randomInt]

def daysDifference(d1,d2):
    return abs((d2 - d1).days)

gitText = ["Yo, ready to see my cogs?", "My nudes can be found here.",
               "Was waiting for when you would ask.",
               "Couldn't be happier."]

helloText = ["G'day", "It’s nice to meet you,", "Bonjour", "Ciao", "Guten Tag",
             "OLÀ", "Konnichiwa", "It’s a pleasure to meet you,","Howdy,",
             "What's new,", "What's up,"]

louisStatements = ["Trying to solo carry <:haHAA:322557800729411595>",
                   "I carried <:haHAA:322557800729411595>",
                   "Give me my blue buff <:FeelsPepoMan:322306831961817090>",
                   "Hobday is so retarded",
                   "Dont ks my penta <:FeelsPepoMan:322306831961817090>",
                   "Honour me <:FeelsPepoMan:322306831961817090>",
                   "I solo carried btw",
                   "Fucking homosexuals",
                   "I would carry this game so hard if only I wasn't lagging.",
                   "That champion is so lame",
                   "Stop taking my cs",
                   "What the fuck, I didn't crit?",
                   "All of you faggots better honour me, I carried so hard.",
                   "I don't care about honours btw.",
                   "My emotes are so good",
                   "My emotes are bad? Look at Toms",
                   "I didn't just fill the entire servers emotes with trash emotes at all"]
                   
                   

class Messages:

    """Fun interaction messages"""
    
    def __init__(self, bot):
        self.bot = bot

    @commands.command(pass_context = True, description = "Responds with messages about louis")
    async def louis(self, ctx):
        """messages about louis"""
        channel = ctx.message.channel
        await self.bot.send_typing(channel)
        string = choice(louisStatements)
        await self.bot.say(string)

    @commands.command(pass_context=True,description = "Responds with the current count tally")
    async def count(self,ctx):
        """Responds with the current count tally"""
        count = pickle.load(open("/home/pi/Desktop/Code/discordBot/storage/count.pkl","rb"))
        authorID = ctx.message.author.id
        if (authorID not in count):
            await self.bot.say("This was your very first message, say this command again and I will say 1(If I'm working correctly).")
        else:
            await self.bot.say("I have seen you message this server a total of "+count[authorID]+" times.")

    @commands.command(pass_context=True, description = "Responds with a hello message")
    async def hello(self, ctx):
        """Blitzcrank says Hello"""
        channel = ctx.message.channel
        await self.bot.send_typing(channel)
        author = ctx.message.author.name
        string = choice(helloText)
        string = string +" "+author
        await self.bot.say(string)

    @commands.command(pass_context=True, description = "Requires a name that the user wants stabed example '!stab Tom' for the command to work. Responds with a message that is fun for everyone involved.")
    async def stab(self, ctx, victim : str):
        """Stab your friends"""
        try:
            channel = ctx.message.channel
            await self.bot.send_typing(channel)
            msg = stab(victim)
            await self.bot.say(msg)
        except:
            await self.bot.say("Something went wrong there matey. Suh dude.")

    @commands.command(pass_context=True, description = "Requires a the twitch name that the user wants to query if s/he is online, example '!live imaqtpie'. Responds with a message whether or not s/he is live")
    async def live(self, ctx, streamer : str):
        """See if a twitch streamer is live"""
        channel = ctx.message.channel
        await self.bot.send_typing(channel)
        test = livegen(streamer)
        if(test == "yes"):
            msg = streamer + ' is currently live at : https://www.twitch.tv/'+streamer
            await self.bot.say(msg)
        else:
            msg = streamer + ' is currently not live <:FeelsBadMan:314700212104986624>'
            await self.bot.say(msg)

    @commands.command(pass_context=True, description = "Responds with a message the date Blitzcrank was created")
    async def birth(self, ctx):
        """Message about Blitzcranks birth"""
        channel = ctx.message.channel
        await self.bot.send_typing(channel)
        today = datetime.date.today()
        birth = datetime.date(2017,5,18)
        days = daysDifference(today,birth)
        await self.bot.say("I was born on the 18th of May 2017. That makes me {} days old.".format(days))

    @commands.command(pass_context=True, description = "Responds with a meme that has to do with Imaqtpie")
    async def doot(self,ctx):
        """Imaqtpie meme"""
        channel = ctx.message.channel
        await self.bot.send_typing(channel)
        await self.bot.say("Doot Diddly Donger Cuckerino HAHA.")

    @commands.command(pass_context=True, description = "Updates the game the Bot is playing")
    async def game(self,ctx):
        """Sets the game of the bot is playing"""
        channel = ctx.message.channel
        await self.bot.send_typing(channel)
        content = ctx.message.content
        content = content[6:]
        if under(content):
            await self.bot.change_presence(game=discord.Game(name=content, type = 1))
            await self.bot.say("Did it change? I mean I can't tell I'm just a robot.")
        else:
            msg = choice(TooBig)
            await self.bot.say(msg)

    #@commands.command(description = "Sings Happy Birthday")
    #async def hobday(self):
        #"""Sings Happy Birthday"""
        #await self.bot.say("Happy birthday to you. Happy birthday to you. Happy birthday dear hobday. Happy birthday to you. Hip hip horay.", tts = True)

    @commands.command(pass_context=True, description = "Responds with a fire bar")
    async def rap(self,ctx):
        """Bars"""
        channel = ctx.message.channel
        await self.bot.send_typing(channel)
        await self.bot.say("Got rocks on my wrist, that shit you can't resist.")

    @commands.command(pass_context=True, no_pm=True, description = "Tells the user when they joined the server")
    async def joined(self, ctx):
        """Says when a member joined."""
        channel = ctx.message.channel
        await self.bot.send_typing(channel)
        member = ctx.message.author
        joinedAt = member.joined_at
        joinedAt=str(joinedAt)
        joinedAt = joinedAt[:10]
        year = joinedAt[:4]
        month = joinedAt[5:7]
        day = joinedAt[8:]
        joinedDate = datetime.date(int(year),int(month),int(day))
        today = datetime.date.today()
        days = daysDifference(today,joinedDate)
        month = whatMonth(int(month))
        daySuffix = whatDaySuffix(int(day))
        if(days > 100):
            string = "You joined on the " + str(day)+daySuffix +" of " + month +" " + str(year)+ ". This means you have been a part of Tom's server for a total of " + str(days)+" days straight!"
        else:
            string = "You joined on the " + str(day)+daySuffix +" of " + month +" " + str(year)+ ". This means you have been a part of Tom's server for a total of " + str(days)+" days straight."
        await self.bot.say(string)

    @commands.command(pass_context=True, description = "Flips a coin and returns the result")
    async def flip(self,ctx):
        """Flips a coin"""
        channel = ctx.message.channel
        await self.bot.send_typing(channel)
        msg = flipCoin()
        await self.bot.say(msg)

    @commands.command(pass_context=True, no_pm=True, description = "Creates invite")
    async def invite(self,ctx):
        """Creates invite"""
        channel = ctx.message.channel
        await self.bot.send_typing(channel)
        invite = await self.bot.create_invite(channel, max_age=600)
        await self.bot.say("Here is your invite buddy: " + invite.url+"\nNote it only lasts for 10 minutes.")

    @commands.command(pass_context=True,description = "Basic 8ball command")
    async def ball8(self,ctx):
        """Magical 8ball command"""
        channel = ctx.message.channel
        await self.bot.send_typing(channel)
        msg = magic8Ball()
        await self.bot.say(msg)

    @commands.command(pass_context=True, description = "Chooses a word from a random string. Example '!choose Tom Harry'.")
    async def choose(self,ctx, *choices : str):
        """Blitzcrank has the choice"""
        channel = ctx.message.channel
        await self.bot.send_typing(channel)
        msg = choice(choices)
        await self.bot.say(msg)

    @commands.command(pass_context=True, description = "Selects a random number from the queries, must be 1 or 2 numbers though.")
    async def random(self,ctx, *numbers : int):
        """Random number between"""
        channel = ctx.message.channel
        await self.bot.send_typing(channel)
        if(len(numbers)>2):
            await self.bot.say('Please provide me with one or two numbers.')
        elif(len(numbers)<=0):
            await self.bot.say('Please provide me with one or two numbers.')
        elif(len(numbers)==1):
            randomNumber = randint(0,numbers[0])
            await self.bot.say(randomNumber+1)
        else:
            randomNumber = randint(numbers[0],numbers[1])
            await self.bot.say(randomNumber)

    @commands.command(pass_context=True,no_pm=True, description = "Responds stating if the member queried is cool or not.")
    async def cool(self, ctx):
        """Blitzcrank determines if you are cool"""
        try:
            channel = ctx.message.channel
            await self.bot.send_typing(channel)
            members = self.bot.get_all_members()
            memberList = list(members)
            names = [0] * len(memberList)
            nick = [0] * len(memberList)
            i=0
            while(i<len(memberList)):
                names[i]=memberList[i].name
                names[i]=names[i].lower()
                i+=1
            i=0
            while(i<len(memberList)):
                nick[i]=memberList[i].nick
                try:
                    nick[i]=nick[i].lower()
                except:
                    x=0
                i+=1
            content = ctx.message.content
            content = str(content)
            contentOG = content
            contentOG = contentOG[6:]
            content = content[6:]
            content = content.lower()
            if(content in names):
                randomNumber = randint(0,1)
                if(randomNumber==0):
                    await self.bot.say("Nahh mate, {0} is not cool.".format(contentOG))
                else:
                    await self.bot.say("Hell yeah mate, {0} is cool.".format(contentOG))
            elif(content in nick):
                randomNumber = randint(0,1)
                if(randomNumber==0):
                    await self.bot.say("Nahh mate, {0} is not cool.".format(contentOG))
                else:
                    await self.bot.say("Hell yeah mate, {0} is cool.".format(contentOG))
            else:
                await self.bot.say("Man, you can thank Sly for this. You now have to put in the name of someone within the server. This can be either their actual username or just their nickname.")
        except:
            await self.bot.say('Umm you stuffed up this command. If you need help just type !help cool for information about this command')
