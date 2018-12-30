#import list
import asyncio
import discord
from discord.ext import commands
from random import *
from re import findall, match, search
import datetime
import requests
import json
from bs4 import BeautifulSoup as BS
from requests import get as re_get

skipsAndStops = ['As you wish.', 'k', 'I mean sure if you want','Do you want fries with that?',
                 'This is my jam, but sure.', "I can't believe you wanted this to begin with.",
                 'You guys wanted this not me.', "If you want anything else type '!help' to see what I can do.",
                 'The time of this song has come to an end.', "Precisely.", "Other golems found that song to be annoying too.",
                 'Thank god my face was leaking from hearing that.', "No one ever asks how I am.",
                 "Damn I was REALLY enjoying that one.", "I'm guessing that this request was more a meme to begin with"]

TooBig = ["You know you make a cool bot and all people want to do is kill it :(", "Dude that is wayyyyy too much for me to handle",
          "Nobody thinks you are cool", "fark", "yes", "No", "I don't like people like you",
          "Ehh", "stop it you are hurting me" , "hackerman", "Dude stop it.", "I can't believe you requested that", "meh","maybe",
          "How about you do that one hey", "Hang on", "nahh maybe I don't", "you can do that one", "You take over now", "you can handle the requests from now on, I'm done"]

wait = ["lamo dude I can only listen to one skip a minute","no","I actually like this, so no",
        "okay sure, lmao jk", "ok sure, jk", "nahh maybe not", "Huh I didn't understand that request",
        "Don't you wish you were Tom right now, He has unlimited skips","I do what Tom says not scum like you",
        "I don't listen to plebs like you", "No you", "Lol", "I sure won't do your request"]


def under(content):
    if(len(content)<70):
        return True
    return False

def stripSearch(content):
    content = content.replace(':', '')
    content = content.replace('.', '')
    content = content.replace(',', '')
    content = content.replace('>', '')
    content = content.replace('<', '')
    content = content.replace('?', '')
    content = content.replace('"', '')
    content = content.replace("'", '')
    content = content.replace(';', '')
    content = content.replace('{', '')
    content = content.replace('[', '')
    content = content.replace('}', '')
    content = content.replace(']', '')
    content = content.replace('|', '')
    content = content.replace("+", '')
    content = content.replace('=', '')
    content = content.replace('_', '')
    content = content.replace('-', '')
    content = content.replace(')', '')
    content = content.replace('(', '')
    content = content.replace('*', '')
    content = content.replace('&', '')
    content = content.replace('^', '')
    content = content.replace('%', '')
    content = content.replace("$", '')
    content = content.replace('#', '')
    content = content.replace('@', '')
    content = content.replace('!', '')
    content = content.replace('~', '')
    content = content.replace('`', '')
    content = content.replace('/', '')
    content = content.replace('\\', '')
    return content
    

#need to test if discord opus is loaded for voice
if not discord.opus.is_loaded():
    discord.opus.load_opus('opus')

#creating a class voice entry and __init__ and __str__
class VoiceEntry:
    def __init__(self, message, player):
        self.requester = message.author
        self.channel = message.channel
        self.player = player

    def __str__(self):
        fmt = '*{0.title}* requested by {1.display_name}'
        duration = self.player.duration
        return fmt.format(self.player, self.requester)

#creating a class voice state and __init__ and is playing with properties 
class VoiceState:
    def __init__(self, bot):
        self.current = None
        self.voice = None
        self.bot = bot
        self.play_next_song = asyncio.Event()
        self.songs = asyncio.Queue()
        self.audio_player = self.bot.loop.create_task(self.audio_player_task())

    def is_playing(self):
        if self.voice is None or self.current is None:
            return False

        player = self.current.player
        return not player.is_done()

    @property
    def player(self):
        return self.current.player

    def skip(self):
        if self.is_playing():
            self.player.stop()

    def toggle_next(self):
        self.bot.loop.call_soon_threadsafe(self.play_next_song.set)

    async def audio_player_task(self):
        while True:
            self.play_next_song.clear()
            self.current = await self.songs.get()
            await self.bot.send_message(self.current.channel, 'Now playing ' + str(self.current))
            self.current.player.start()
            await self.play_next_song.wait()

#creating class music and __init__ and also making the commands
class Music:
    skipAble = True;
    """Commands related to playing music from youtube"""
    
    def __init__(self, bot):
        self.bot = bot
        self.voice_states = {}

    def get_voice_state(self, server):
        state = self.voice_states.get(server.id)
        if state is None:
            state = VoiceState(self.bot)
            self.voice_states[server.id] = state

        return state

    async def create_voice_client(self, channel):
        voice = await self.bot.join_voice_channel(channel)
        state = self.get_voice_state(channel.server)
        state.voice = voice

    def unload(self):
        for state in self.voice_states.values():
            try:
                state.audio_player.cancel()
                if state.voice:
                    self.bot.loop.create_task(state.voice.disconnect())
            except:
                pass

        

    @commands.command(pass_context=True, no_pm=True, description = "Makes Blitzcrank join whatever voice channel the author is in")
    async def summon(self, ctx):
        """The bot joins the voice channel the author is in"""
        summoned_channel = ctx.message.author.voice_channel
        if summoned_channel is None:
            await self.bot.say('You are not in a voice channel.')
            return False

        state = self.get_voice_state(ctx.message.server)
        if state.voice is None:
            state.voice = await self.bot.join_voice_channel(summoned_channel)
        else:
            await state.voice.move_to(summoned_channel)

        return True

    @commands.command(pass_context=True, no_pm=True, description = "Adds whatever song is queried into the list and plays it if nothing else is playing. Example '!play fireflies'.")
    async def play(self, ctx):
        """Adds whatever song is queried into the list"""
        channel = ctx.message.channel
        await self.bot.send_typing(channel)
        song = ctx.message.content
        song = song[6:]
        if under(song):
            if "https://www.youtube.com/watch" not in song:
                song = stripSearch(song)
            state = self.get_voice_state(ctx.message.server)
            opts = {
                'default_search': 'auto',
                'quiet': True,
            }
            if state.voice is None:
                success = await ctx.invoke(self.summon)
                if not success:
                    return
            try:
                player = await state.voice.create_ytdl_player(song, ytdl_options=opts, after=state.toggle_next)
            except Exception as e:
                fmt = 'Something went wrong. Best bet is to stop me and try again later in 10 minutes. If this error continues to occur contact your admin to reset the script.'
                await self.bot.say(fmt)
            else:
                player.volume = 0.25
                entry = VoiceEntry(ctx.message, player)
                await self.bot.say('Yeah I added: ' + str(entry))
                await state.songs.put(entry)
        else:
            msg = choice(TooBig)
            await self.bot.say(msg)

    @commands.command(pass_context=True, no_pm=True, description = "Sets the volume to whatever is queried. Example '!volume 25'.")
    async def volume(self, ctx):
        """Sets the volume to whatever is queried"""
        channel = ctx.message.channel
        await self.bot.send_typing(channel)
        volume = ctx.message.content
        volume = volume[8:]
        if under(volume):
            try:
                volume = int(volume)
                integer= True
            except:
                integer=False
            state = self.get_voice_state(ctx.message.server)
            if state.is_playing():
                try:
                    if integer:
                        if volume <= 50:
                            player = state.player
                            player.volume = volume / 100
                            await self.bot.say('Set the volume to {:.0%}'.format(player.volume))
                        else:
                            player = state.player
                            player.volume = 50 / 100
                            await self.bot.say('Set the volume to {:.0%}'.format(volume/100))
                    elif integer == False:
                        msg = choice(TooBig)
                        await self.bot.say(msg)
                except:
                    await self.bot.say("TODO Fix the volume chaning function")
            else:
                await self.bot.say("I can't change the volume if I'm not playing anything.")
        else:
            msg = choice(TooBig)
            await self.bot.say(msg)

    @commands.command(pass_context=True, no_pm=True, description = "Stops the current song playing and any other song that is queued")
    async def stop(self, ctx):
        """Stops the current song and playing music all together"""
        if(Music.skipAble):
            Music.skipAble=False
            server = ctx.message.server
            state = self.get_voice_state(server)
            if state.is_playing():
                player = state.player
                player.stop()
                string = choice(skipsAndStops)
                await self.bot.say(string)
            try:
                state.audio_player.cancel()
                del self.voice_states[server.id]
                await state.voice.disconnect()
            except:
                pass
        elif (ctx.message.author.id == "163137666341142529"):
            Music.skipAble=False
            server = ctx.message.server
            state = self.get_voice_state(server)
            if state.is_playing():
                player = state.player
                player.stop()
                string = choice(skipsAndStops)
                await self.bot.say(string)
            try:
                state.audio_player.cancel()
                del self.voice_states[server.id]
                await state.voice.disconnect()
            except:
                pass
        else:
            string = choice(wait)
            await self.bot.say(string)

    @commands.command(pass_context=True, no_pm=True, description = "Skips current song that is playing")
    async def skip(self, ctx):
        """skips the current song"""
        if(Music.skipAble):
            channel = ctx.message.channel
            await self.bot.send_typing(channel)
            state = self.get_voice_state(ctx.message.server)
            if not state.is_playing():
                await self.bot.say('Not playing any music right now...')
                return
            string = choice(skipsAndStops)
            await self.bot.say(string)
            state.skip()
            Music.skipAble=False
        elif(ctx.message.author.id == "163137666341142529"):
            channel = ctx.message.channel
            await self.bot.send_typing(channel)
            state = self.get_voice_state(ctx.message.server)
            if not state.is_playing():
                await self.bot.say('Not playing any music right now...')
                return
            string = choice(skipsAndStops)
            await self.bot.say(string)
            state.skip()
            Music.skipAble=False
        else:
            string = choice(wait)
            await self.bot.say(string)
            
#code mainly from https://github.com/Rapptz/discord.py/blob/async/examples/playlist.py
