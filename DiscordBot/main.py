### Bot Main Script ###

#import list
import asyncio
import discord
from discord.ext import commands
import importlib.machinery
import logging
import pickle

logging.basicConfig(level=logging.INFO) #for general information about what is going on

from cogs.chat import Messages

from cogs.music import VoiceEntry, VoiceState, Music

from cogs.images import Images

from cogs.points import Points

from cogs.search import Search

from cogs.jokes import Jokes

from cogs.polling import Polls

from cogs.hangman import Hangman

from cogs.moderation import Moderator

bot = commands.Bot(command_prefix='!')
bot.add_cog(Messages(bot))
bot.add_cog(Music(bot))
bot.add_cog(Images(bot))
bot.add_cog(Points(bot))
bot.add_cog(Search(bot))
bot.add_cog(Jokes(bot))
bot.add_cog(Polls(bot))
bot.add_cog(Hangman(bot))
bot.add_cog(Moderator(bot))

@bot.event
async def on_ready():
    await bot.change_presence(game=discord.Game(name="On A Raspberry Pi", type = 1))
    print('I am Bot Logged in as:\n{0} (ID: {0.id})'.format(bot.user))
    members = bot.get_all_members()
    memberList = list(members)
    ids = [0]*len(memberList)
    i=0
    while(i<len(memberList)):
        if(hasRole(memberList[i])):
            ids[i]=memberList[i].id
        i+=1
    Moderator.moderatorIds = ids

def hasRole(member):
    roles = member.roles
    i=0
    while(i<len(roles)):
        roles[i]=roles[i].name
        i+=1
    if("Verified" in roles):
        return True
    return False

@bot.event
async def on_member_join(member):
    server = member.server
    string = "Welcome "+member.name+ " to "+server.name+"! How's It going mate.\n\nType '!help' to see all the commands I'm Tom has created. There are some commands that will work here, but all the commands will work in the server itself.\n\nThanks for joining."
    await bot.send_message(member, string)
    update()

async def my_background_task():
    await bot.wait_until_ready()
    while not bot.is_closed:
        members = bot.get_all_members()
        memberList = list(members)
        Points.memberListLength=len(memberList)
        ids = pickle.load(open("/home/pi/Desktop/Code/discordBot/storage/ids.pkl","rb"))
        Points.memberCooldownList=[0]*len(ids)
        i=0
        while(i<len(memberList)):
            member = memberList[i]
            status = member.status
            if('offline' == str(status)):
                del memberList[i]
                i-=1
            i+=1
        i=0
        while(i<len(memberList)):
            memberList[i]=memberList[i].id
            i+=1
        onlineIndexs = []
        i=0
        while(i<len(memberList)):
            search = memberList[i]
            try:
                namesIndex = ids.index(search)
                onlineIndexs.append(namesIndex)
            except:
                x=0
            i+=1
        await asyncio.sleep(60) # task runs every 60 seconds
        Music.skipAble = True
        points = pickle.load(open("/home/pi/Desktop/Code/discordBot/storage/points.pkl","rb"))
        i=0
        while(i<len(onlineIndexs)):
            n = onlineIndexs[i]
            points[n]+=1
            i+=1
        i=0
        while(i<Points.memberListLength):
            if(Points.memberCooldownList[i]!=0):
                Points.memberCooldownList[i]=Points.memberCooldownList[i]-1
            i+=1
        pickle.dump(points, open("/home/pi/Desktop/Code/discordBot/storage/points.pkl","wb"))

def update():
    members = bot.get_all_members()
    memberList = list(members)
    Points.memberListLength=len(memberList)
    Points.memberCooldownList=[0]*Points.memberListLength
    names = pickle.load(open("/home/pi/Desktop/Code/discordBot/storage/names.pkl","rb"))
    points = pickle.load(open("/home/pi/Desktop/Code/discordBot/storage/points.pkl","rb"))
    ids = pickle.load(open("/home/pi/Desktop/Code/discordBot/storage/ids.pkl","rb"))
    i=0
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
    
@bot.event
async def on_message(message):
    await bot.process_commands(message)
    if message.content.startswith('!'):
        await asyncio.sleep(120)
        try:
            await bot.delete_message(message)
        except:
            print('something went wrong')
    elif message.author.id == '316045989385601044':
        await asyncio.sleep(120)
        try:
            await bot.delete_message(message)
        except:
            print('something went wrong')
            

loader = importlib.machinery.SourceFileLoader('token','/home/pi/Desktop/Code/token.py')
handle = loader.load_module('token')

bot.loop.create_task(my_background_task())
bot.pm_help = True

bot.run(handle.botToken())
