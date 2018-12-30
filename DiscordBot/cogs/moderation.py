#import list
import asyncio
import discord
from discord.ext import commands
from requests import get as re_get
from random import *
import re

ownerId = '163137666341142529'


TooBig = ["You know you make a cool bot and all people want to do is kill it :(", "Dude that is wayyyyy too much for me to handle",
          "Nobody thinks you are cool", "fark", "yes", "No", "I don't like people like you",
          "Ehh", "stop it you are hurting me" , "hackerman", "Dude stop it.", "I can't believe you requested that", "meh","maybe",
          "How about you do that one hey", "Hang on", "nahh maybe I don't", "you can do that one", "You take over now", "you can handle the requests from now on, I'm done"]

def under(content):
    if(len(content)<5):
        return True
    return False

class Moderator:
    moderatorIds=[0]
    """The bot's moderation side"""
    def __init__(self, bot):
        self.bot=bot
    
    @commands.command(pass_context=True, no_pm=True, description="Kicks a member")
    async def kick(self,ctx, member: discord.Member):
        """Kicks a member"""
        author = ctx.message.author
        author = author.id
        if(author == ownerId):
            await self.bot.kick(member)
            string = "Member: "+member.name+"\n\nHas been kicked!"
            await self.bot.say(string)
        elif(author in Moderator.moderatorIds):
            if(member.id != ownerId):
                if(member.id in moderatorIds):
                    string = "You can't kick other verified members my dude."
                    await self.bot.say(string)
                else:
                    await self.bot.kick(member)
                    string = "Member: "+member.name+"\n\nHas been kicked!"
                    await self.bot.say(string)
            else:
                await self.bot.say('Fuck off cunt. Just because you did that you lost your verified status.')
        else:
            await self.bot.say('You do not have the permissions required to use this command. Contact someone who is verified or an admin if you need help.')

    @commands.command(pass_context=True, no_pm=True, description="deletes messages")
    async def delete(self,ctx):
        """deletes messages"""
        channel = ctx.message.channel
        role=[]
        content = ctx.message.content
        content = content[8:]
        if under(content):
            a = re.search('[a-zA-Z]', content)
            if(a==None):
                author = ctx.message.author
                role = author.roles
                i=0
                while i<len(role):
                    try:
                        role[i]=role[i].name
                    except:
                        x=0
                    i+=1
                if('Admin' in role):
                    async for message in self.bot.logs_from(channel,limit=int(content)):
                        await self.bot.delete_message(message)
                    await self.bot.say('Messages deleted')
                elif('Verified' in role):
                    async for message in self.bot.logs_from(channel,limit=int(content)):
                        await self.bot.delete_message(message)
                    await self.bot.say('Messages deleted')
                else:
                    await self.bot.say('You do not have the permissions required to use this command. Contact someone who is verified or an admin if you need help.')
            else:
                await self.bot.say('Stop trying to break me :(')
        else:
            msg = choice(TooBig)
            await self.bot.say(msg)


    @commands.command(pass_context=True, no_pm=True, description="temp mutes messages")
    async def mute(self,ctx, member: discord.Member):
        """temp mutes a member"""
        author = ctx.message.author.id
        if(author == ownerId):
            server = ctx.message.server
            role = discord.utils.get(server.roles, name="Timed Out")
            await self.bot.add_roles(member,role)
            string = "Member: "+member.name+"\n\nHas been muted!"
            await self.bot.say(string)
        elif(author in Moderator.moderatorIds):
            server = ctx.message.server
            role = discord.utils.get(server.roles, name="Timed Out")
            await self.bot.add_roles(member, role)
            string = "Member: "+member.name+"\n\nHas been temporarily muted!"
            await self.bot.say(string)
            await asyncio.sleep(60)
            await self.bot.remove_roles(member, roles)
        else:
            await self.bot.say('Either two things happened mate.\n1. You do not have permissions to use this command.\n2. I fucked up.')


    @commands.command(pass_context=True, no_pm=True, description="unmutes messages")
    async def unmute(self,ctx, member: discord.Member):
        """unmutes a member"""
        try:
            author = ctx.message.author
            author = author.id
            if(author in Moderator.moderatorIds):
                server = ctx.message.server
                role = discord.utils.get(server.roles, name="Timed Out")
                await self.bot.remove_roles(member, role)
                string = "Member: "+member.name+"\n\nHas been unmuted!"
                await self.bot.say(string)
            else:
                await self.bot.say('Either two things happened mate.\n1. You do not have permissions to use this command.\n2. I fucked up.')
        except:
            await self.bot.say('Errr, just get Tom to do it manually')

    @commands.command(pass_context=True, description="gets the ban list")
    async def bans(self,ctx):
        """gets the ban list"""
        server = ctx.message.server
        bans = await self.bot.get_bans(server)
        i = 0
        string = "\n\n"
        while(i<len(bans)):
            string = string+str(i+1)+". "+bans[i].name+".\n"
            i+=1
        string = "The people who are banned from entering this server are: "+string
        await self.bot.say(string)

    @commands.command(pass_context=True, hidden = True, no_pm=True, description="bans members")
    async def ban(self,ctx, user: discord.Member):
        """bans a members"""
        try:
            author = ctx.message.author
            author = author.id
            if(author == ownerId):
                await self.bot.ban(user, delete_message_days=7)
                string = "Member: "+user.name+"\n\nHas been banned!"
                await self.bot.say(string)
            else:
                await self.bot.say('Either two things happened mate.\n1. You do not have permissions to use this command.\n2. I fucked up.')
        except:
            await self.bot.say('Errr, just get Tom to do it manually')
