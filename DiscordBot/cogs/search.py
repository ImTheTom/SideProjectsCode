#import list
import asyncio
import discord
from discord.ext import commands
import importlib.machinery
import datetime
import requests
import json
from bs4 import BeautifulSoup as BS
from requests import get as re_get
from random import *
from re import findall, match, search

TooBig = ["You know you make a cool bot and all people want to do is kill it :(", "Dude that is wayyyyy too much for me to handle",
          "Nobody thinks you are cool", "fark", "yes", "No", "I don't like people like you",
          "Ehh", "stop it you are hurting me" , "hackerman", "Dude stop it.", "I can't believe you requested that", "meh","maybe",
          "How about you do that one hey", "Hang on", "nahh maybe I don't", "you can do that one", "You take over now", "you can handle the requests from now on, I'm done"]

def under(content):
    if(len(content)<3000):
        return True
    return False


#Strips the message to find what the actual content that needs to be searched
def youtube(msg):
    url = "https://www.youtube.com/results?search_query="+msg
    bs = BS(re_get(url).text, "html.parser")
    items = bs.find("div", id="results").find_all("div", class_="yt-lockup-content")
    i= 0
    found = False
    while not found and i < 20:
        href = items[i].find("a", class_="yt-uix-sessionlink")["href"]
        if href.startswith("/watch"):
                found = True
                return href
        if not found:
            i+=1
    return href

def youtubeDetails(msg):
    url = msg
    bs = BS(re_get(url).text, "html.parser")
    title = bs.find(class_="watch-title")
    title = title.contents[0]
    title = title[5:-3]
    views = bs.find(class_="watch-view-count")
    views = views.contents[0]
    views = views[:-6]
    like = bs.find_all(class_="yt-uix-button-content")
    likes = like[19]
    likes = likes.contents[0]
    dislikes = like[20]
    dislikes = dislikes.contents[0]
    date = bs.find(class_="watch-time-text")
    date = date.contents[0]
    date = date[13:]
    subCount = bs.find(class_="yt-subscription-button-subscriber-count-branded-horizontal yt-subscriber-count")
    subCount = subCount.contents[0]
    uploader = bs.find(class_="yt-user-info")
    uploader = uploader.find('a').contents[0]
    string = "Name: "+title+".\n\nViews: "+views+". Uploaded: "+date+"\n\nUploader: "+uploader+". Subscribers: "+subCount+".\n\nLikes: "+likes+". Dislikes: "+dislikes+".\n\nURL: "+url
    return string

def googleSearch(url):
    try:
        bs = BS(re_get(url).text, "html.parser")
        links = bs.find(class_="r")
        links = links.find('a', href=True)
        first = links.contents
        i =0
        length = len(first)
        string = ""
        while(i<length):
            string = string +str(first[i])
            i+=1
        string = stripOfSpecialCharacters(string)
        link = links['href']
        link = link[7:]
        indexofAnd = link.index('&')
        link = link [:indexofAnd]
        results = bs.find(id="resultStats")
        results = results.contents[0]
        results = results[:-8]
        description = bs.find(class_="st")
        description = description.contents
        i=0
        length=len(description)
        d=""
        while(i<length):
            d = d +str(description[i])
            i+=1
        d = stripOfSpecialCharacters(d)
        message= "Search URL: "+url+"\n\nResults: "+results+".\n\nFirst URL: "+link+"\n\nTitle: "+string+".\n\nDescription: "+d
        return message
    except:
        message = "Something went wrong here. Either I fucked up or you fucked up. My money is on you that fucked it up."
        return message

def urbanSearch(url):
    try:
        bs = BS(re_get(url).text, "html.parser")
        links = bs.find(class_="def-header")
        links = links.find('a', href=True)
        links = links.contents[0]
        for a in bs.findAll('a'):
            del a['href']
        meaning = bs.find(class_="meaning")
        meaning = meaning.contents
        string = ""
        i=0
        length = len(meaning)
        while(i<length):
            string = string+str(meaning[i])
            i+=1
        meaning = stripOfSpecialCharacters(string)
        example = bs.find(class_="example")
        example = example.contents
        string = ""
        i=0
        length = len(example)
        while(i<length):
            string = string+str(example[i])
            i+=1
        example = stripOfSpecialCharacters(string)
        contribuation = bs.find(class_="contributor")
        contribuation = contribuation.contents
        author = contribuation[1]
        author = stripOfSpecialCharacters(str(author))
        date= contribuation[2]
        date = str(date)
        date = date[1:]
        tags = bs.find(class_="tags")
        tags = tags.contents
        length = len(tags)
        i=0
        string =""
        while(i<length):
            string = string+str(tags[i])+' '
            i+=1
        tags = stripOfSpecialCharacters(string)
        counts = bs.find_all(class_="count")
        likes = counts[0]
        dislikes = counts[1]
        likes = likes.contents[0]
        dislikes = dislikes.contents[0]
        string = links+"\n\nMeaning: \n" + meaning+"\n\nExamples: \n"+example+"\n\nTags: "+tags+"\n\nAuthor: "+author+".\nDate: "+date+".\nLikes: "+str(likes)+ ". Dislikes: "+str(dislikes)+".\n\nURL: "+url
        return string
    except:
        string = "Something went wrong here. Either I fucked up or you fucked up. My money is on you that fucked it up."
        return string

def opSearch(url):
    try:
        bs = BS(re_get(url).text, "html.parser")
        name = bs.find(class_="Name")
        name = name.contents[0]
        try:
            rank = bs.find(class_="TierRank")
            rank = rank.contents
            rank = rank[1]
            rankSolo = rank.contents[0]
            rank = bs.find_all(class_="TierRank")
            rank =rank[1]
            flex = rank.contents
            flex = flex[1]
            flex = flex.contents[0]
        except:
            rank ="unranked"
            flex = "unranked"
        try:
            wins = bs.find(class_="win")
            wins = wins.contents[0]
            loss = bs.find(class_="lose")
            loss = loss.contents[0]
        except:
            wins ="little or no games played"
            loss = "little or no games played"
        try:
            champ = bs.find_all(class_="ChampionName")
            first = champ[0]
            first = first.contents[1]
            first = str(first)
            first = stripOfSpecialCharacters(first)
            first=stripSpaces(first)
            first = first.strip()
        except:
            first =""
        try:
            champ = bs.find_all(class_="ChampionName")
            second = champ[1]
            second = second.contents[1]
            second = str(second)
            second = stripOfSpecialCharacters(second)
            second=stripSpaces(second)
            second = second.strip()
        except:
            second =""
        try:
            champ = bs.find_all(class_="ChampionName")
            third = champ[2]
            third = third.contents[1]
            third = str(third)
            third = stripOfSpecialCharacters(third)
            third=stripSpaces(third)
            third = third.strip()
        except:
            third = ""
        string = name+"\n\nRank Solo: "+rankSolo+". Rank Flex: "+flex+".\nWins: "+wins+". Losses: "+loss+".\n\nChampions: "+first+", "+second+", "+third+".\n\nURL: "+url
        return string
    except:
        string = "Something went wrong here. Either I fucked up or you fucked up. My money is on you that fucked it up."
        return string

def stripSpaces(string):
    string = string.replace(' ', '')
    string = string.replace('   ', '')
    return string

def stripOfSpecialCharacters(string):
    string = string.replace('<b>', '')
    string = string.replace('</b>', '')
    string = string.replace('<i>', '')
    string = string.replace('</i>', '')
    string = string.replace('<a>', '')
    string = string.replace('</a>', '')
    string = string.replace('<br/>', '')
    string = string.replace('<hr/>', '')
    string = string.replace('<em>', '')
    string = string.replace('</em>', '')
    string = string.replace('<strong>', '')
    string = string.replace('</strong>', '')
    string = string.replace('<code>', '')
    string = string.replace('</code>', '')
    string = string.replace('<sub>', '')
    string = string.replace('</sub>', '')
    string = string.replace('<sup>', '')
    string = string.replace('</sup>', '')
    string = string.replace('<div>', '')
    string = string.replace('</div>', '')
    return string
    
class Search:
    """Commands about searching the web"""
    def __init__(self,bot):
        self.bot = bot

    @commands.command(pass_context = True, description = "Responds with the first link from a youtube search and information about it. Example '!yt league of legends montage'.")
    async def yt(self, ctx):
        """Search youtube from discord"""
        try:
            channel = ctx.message.channel
            await self.bot.send_typing(channel)
            content = ctx.message.content
            content = content[4:]
            if under(content):
                content = content.replace(" ","+")
                search = youtube(content)
                search='https://www.youtube.com'+search
                information = youtubeDetails(search)
                await self.bot.say(information)
            else:
                msg = choice(TooBig)
                await self.bot.say(msg)
        except:
            await self.bot.say("82 million videos on youtube and I couldn't find one <:FeelsBadMan:314700212104986624> (either that or the video is a livestream)")

    @commands.command(pass_context = True, description = "Responds with the first link from a google search. Example '!google league of legends montage'.")
    async def google(self, ctx):
        """Search google from discord"""
        try:
            channel = ctx.message.channel
            await self.bot.send_typing(channel)
            content = ctx.message.content
            content = content[8:]
            if under(content):
                content = content.replace(" ","+")
                content= "https://www.google.com.au/search?q="+content
                content = googleSearch(content)
                await self.bot.say(content)
            else:
                msg = choice(TooBig)
                await self.bot.say(msg)
        except:
            await self.bot.say("Try Yahoo")

    @commands.command(pass_context = True, description = "Responds with infomation about an urban dictionary search. Example '!urban pineapples'.")
    async def urban(self, ctx):
        """Search urban dictionary from discord"""
        try:
            channel = ctx.message.channel
            await self.bot.send_typing(channel)
            content = ctx.message.content
            content = content[7:]
            if under(content):
                content = content.replace(" ","+")
                content= "http://www.urbandictionary.com/define.php?term="+content
                content = urbanSearch(content)
                await self.bot.say(content)
            else:
                msg = choice(TooBig)
                await self.bot.say(msg)
        except:
            await self.bot.say("Try an actual dictionary")

    @commands.command(pass_context = True, description = "Responds with information about op.gg. Example '!op X Kustoms X'.")
    async def op(self, ctx):
        """Search op.gg from discord"""
        try:
            channel = ctx.message.channel
            await self.bot.send_typing(channel)
            content = ctx.message.content
            content = content[4:]
            if under(content):
                content = content.replace(" ","%20")
                content= "https://oce.op.gg/summoner/userName="+content
                content = opSearch(content)
                await self.bot.say(content)
            else:
                msg = choice(TooBig)
                await self.bot.say(msg)
        except:
            await self.bot.say("Maybe he/she is from a different server? Idk.")

