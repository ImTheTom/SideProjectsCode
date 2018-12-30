#import list
import asyncio
import discord
from discord.ext import commands
from random import *
from re import findall, match, search

wordList = ["atrox", 'ahri','akali','alistar','amumu','anivia','annie','ashe','aurelion sol','azir','bard','blitzcrank','brand','braum',
            'caitlyn','camile','cassiopeia','corki',"cho gath",'darius','diana','dr mundo','draven','elise','evelynn','ezreal','fiddlesticks','fiora',
            'galio','gangplank','garen','gnar','gragas','graves','hecarim','heimerdinger','illaoi','irelia','irelia','ivern','janna','jarvan iv','jax',
            'jayce','jhin','jinx','kalista','karma','karthus','kassadin','katarina','kayle','kennen','kha zix','kindred','kled','kog maw','leblanc',
            'lee sin','leona','lissandra','lucian','lulu','lux','malphite','malzahar','maokai','master yi','miss fortune','mordekaiser','morgana','nami',
            'nasus','nautilus','nidalee','nocturne','nunu','olaf','oriana','pantheon','poppy','quinn','rammus','rek sai','renketon','rengar','riven',
            'rumble','ryze','sejuani','shaco','shen','shyvana','singed','sion','sivir','skarner','sona','soraka','swain','syndra','tahm kench','taliyah',
            'talon','taric','teemo','thresh','tristana','trundle','tryndamere','twisted fate','twitch','udyr','urgot','varus','vayne','veigar','vel koz',
            'vi','vikotr','vladimir','volibear','warwick','wukong','xerath','xin zhao','yasuo','yorick','zac','zed','ziggs','zilea','zyra']

def find(ch,string1):
    pos =[]
    for i in range(len(string1)):
        if ch == string1[i]:
            pos.append(i)
    return pos

class Hangman:
    """Class that creates a hangman game"""
    word =""
    guess=""
    state=False
    number=0
    
    def __init__(self,bot):
        self.bot = bot

    @commands.command(pass_context=True, no_pm=True, description = "Starts a hangman game")
    async def newgame(self, ctx):
        """Starts a new game of hangman"""
        channel = ctx.message.channel
        await self.bot.send_typing(channel)
        word = choice(wordList)
        i=0
        underscore = ['-']*len(word)
        x=0
        try:
            indexOfSpace=word.index(" ")
            underscore[indexOfSpace]=' '
            x=1
        except:
            x=0
        underscoreString =""
        while(i<len(word)):
            underscoreString += underscore[i]
            i+=1
        Hangman.guess = underscore
        Hangman.word=word
        Hangman.state=True
        Hangman.number=10
        string = "New Hangman game created!\n\nYour word to guess is: "+underscoreString+". Which is "+ str(len(word)-x)+" letters long."
        await self.bot.say(string)

    @commands.command(pass_context=True, no_pm=True, description = "Creates a guess in hangman")
    async def l(self, ctx):
        """Used to guess a letter in hangman"""
        channel = ctx.message.channel
        await self.bot.send_typing(channel)
        if(Hangman.state):
            content = ctx.message.content
            content = content[3:]
            content =content[:1]
            content = content.lower()
            indexs =  find(content,Hangman.word)
            if(len(indexs)!=0):
                i=0
                while(i<len(indexs)):
                    index = indexs[i]
                    Hangman.guess[index] = content
                    i+=1
                guessWordInString=""
                i=0
                while(i<len(Hangman.guess)):
                    guessWordInString=guessWordInString+Hangman.guess[i]
                    i+=1
                string ="Correct guess!\n\nWord is: "+guessWordInString
                await self.bot.say(string)
                if(guessWordInString==Hangman.word):
                    string ="Nice work guys. You Won. The word was: "+Hangman.word
                    await self.bot.say(string)
                    Hangman.state=False
            else:
                Hangman.number-=1
                if(Hangman.number>0):
                    guessWordInString=""
                    i=0
                    while(i<len(Hangman.guess)):
                        guessWordInString=guessWordInString+Hangman.guess[i]
                        i+=1
                    string ="Incorrect guess! Number of remaining guesses are: "+str(Hangman.number)+"\n\nWord is: "+guessWordInString
                    await self.bot.say(string)
                else:
                    string ="Unlucky guys. You guys lost. The word was: "+Hangman.word
                    await self.bot.say(string)
                    Hangman.state=False
        else:
            await self.bot.say("No current hangman game.\n\nStart one with the command '!newgame'")

    @commands.command(pass_context=True, no_pm=True, description = "Creates a guess for a w hangman")
    async def w(self, ctx):
        channel = ctx.message.channel
        await self.bot.send_typing(channel)
        """Used to guess a word in hangman"""
        if(Hangman.state):
            content = ctx.message.content
            content = content[3:]
            content = content.lower()
            if(content==Hangman.word):
                string ="Nice work guys. You Won. The word was: "+Hangman.word
                await self.bot.say(string)
                Hangman.state=False
            else:
                i=0
                Hangman.number-=1
                guessWordInString=""
                while(i<len(Hangman.guess)):
                    guessWordInString=guessWordInString+Hangman.guess[i]
                    i+=1
                string ="Incorrect guess! Number of remaining guesses are: "+str(Hangman.number)+"\n\nWord is: "+guessWordInString
                await self.bot.say(string)
        else:
            await self.bot.say("No current hangman game.\n\nStart one with the command '!newgame'")
