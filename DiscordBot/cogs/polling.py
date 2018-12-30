#import list
import asyncio
import discord
from random import *
from discord.ext import commands

TooBig = ["You know you make a cool bot and all people want to do is kill it :(", "Dude that is wayyyyy too much for me to handle",
          "Nobody thinks you are cool", "fark", "yes", "No", "I don't like people like you",
          "Ehh", "stop it you are hurting me" , "hackerman", "Dude stop it.", "I can't believe you requested that", "meh","maybe",
          "How about you do that one hey", "Hang on", "nahh maybe I don't", "you can do that one", "You take over now", "you can handle the requests from now on, I'm done"]

def under(content):
    if(len(content)<2000):
        return True
    return False

class Polls:
    """Create a poll"""
    def __init__(self,bot):
        self.bot = bot

    @commands.command(pass_context=True, no_pm=True, description = "Starts a poll")
    async def poll(self, ctx):
        """Starts a poll"""
        message = ctx.message
        author = message.author.name
        content = message.content
        content = content[6:]
        if under(content):
            if(content!=""):
                for reaction in ('\U0001F44D', u"\U0001F44E"):
                    await self.bot.add_reaction(message, reaction)
                string = "It looks like "+author+" has started a poll.\n\nTo agree with the poll click on the thumbs up, to disagree with the poll click on the thumbs down.\n\nYou have one minute to cast your vote."
                await self.bot.say(string)
                await asyncio.sleep(60)
                reactions = message.reactions
                likes = reactions[0].count-1
                dislikes = reactions[1].count-1
                if(likes>dislikes):
                    string = "It looks like we have an agreement in the poll: " + content+ ".\n\nWith a total of "+str(likes)+" people in agreement to "+str(dislikes)+" people in disagreement. Thank-you to all that participated."
                elif(dislikes>likes):
                    string = "It looks like we have a disagreement in the poll: " + content+ ".\n\nWith a total of "+str(dislikes)+" people in disagreement to "+str(likes)+"people in agreement. Thank-you to all that participated."
                else:
                    string = "It looks like we have a tie in the poll: " + content+ ".\n\nWith a total of "+str(likes)+" people in agreement and "+str(dislikes)+" people in disagreement. Thank-you to all that participated."
                await self.bot.say(string)
            else:
                string = "Hmmm I wonder what you are trying to poll "+u"\U0001F914"
                await self.bot.say(string)
        else:
            msg = choice(TooBig)
            await self.bot.say(msg)
