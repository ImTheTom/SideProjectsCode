#import list
import asyncio
import discord
from discord.ext import commands
import os
from random import randint

class Images:
    """Commands related to uploading images"""
    def __init__(self, bot):
        self.bot=bot
    
    @commands.command(pass_context=True, description="Uploads a random image from my folder")
    async def image(self,ctx):
        """Uploads a random image"""
        channel = ctx.message.channel
        images = os.listdir("/home/pi/Desktop/Code/discordBot/images")
        randomNumber = randint(0,len(images)-1)
        await self.bot.send_file(channel, 'images/' + images[randomNumber])

    @commands.command(pass_context=True, description="Uploads the GachiBass gif")
    async def gachiBASS(self,ctx):
        """Uploads a gachibass gif"""
        channel = ctx.message.channel
        await self.bot.send_file(channel, 'images/gachiBass.gif')
