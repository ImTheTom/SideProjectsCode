from bs4 import BeautifulSoup
from urllib.request import urlopen
import urllib
import requests
import re
import asyncio
import discord
import time
import pickle
import os
import sys

message = ("Hey my dude. This is Tom and not your bot. I found your private token on Github. Don't do that. "+
           "It's bad. Go to https://discordapp.com/developers/applications/me and generate a new token for your bot. " +
           "Before you go and upload your token to Github try to figure out a way to prevent this from happening. Use a combo of "+
           ".gitignore and having the code be read into from a text file. You can figure it out.")

class WebSearcher:

    def __init__(self):
        self.repositoriesURLList = []
        self.scriptURLSFromRepositories = []
        self.foundKeys = []
        self.searchURL = "https://github.com/search?l=Python&o=desc&p=1&q=discord+bot&s=updated&type=Repositories&utf8=%E2%9C%93"
        self.commonURL = "https://raw.githubusercontent.com"
        self.searchTerms = ['code', 'Code', 'token', 'Token', 'key', 'Key', 'privatekey', 'PrivateKey', 'botKey', 'BotKey', 'botToken', 'BotToken', 'creds', 'Creds']

    def FormatToken(self, inputString):
        inputString = inputString.replace(' ','')
        inputString = inputString.replace('"','')
        inputString = inputString.replace('=','')
        inputString = inputString.replace('(','')
        inputString = inputString.replace(')','')
        inputString = inputString.replace('#','')
        inputString = inputString.replace(',','')
        inputString = inputString.replace("'",'')
        inputString = re.sub(r'\n','',inputString)
        inputString = re.sub(r'\t','',inputString)
        inputString = inputString.strip("\t")
        return inputString

    def GetRepositoriesURLS(self):
        page = urlopen(self.searchURL)
        pageSource = BeautifulSoup(page,'html.parser')
        divsContainingRepository = pageSource.findAll("a", {"class":"v-align-middle"})
        for div in divsContainingRepository:
            startOfURL = str.find(str(div), '"url":"')
            endOfURL = str.find(str(div),'"},"')
            repositoryURL = str(div)[startOfURL+7:endOfURL]
            self.repositoriesURLList.append(repositoryURL)

    def GetScriptURLS(self):
        for repositoryURL in self.repositoriesURLList:
            print(repositoryURL)
            page = urlopen(repositoryURL)
            pageSource = BeautifulSoup(page,'html.parser')
            tableOfScripts = pageSource.find("table", {"class":"files js-navigation-container js-active-navigation-container"})
            for aDiv in tableOfScripts.find_all('a', href=True):
                if(str(aDiv['href'])[-2:] == 'py'):
                    rawScriptSite = self.commonURL + aDiv['href']
                    rawScriptSite = rawScriptSite.replace('/blob','')
                    self.scriptURLSFromRepositories.append(rawScriptSite)
            time.sleep(10)    
            print(self.scriptURLSFromRepositories)

    def FindTokensFromScripts(self):
        for scriptURL in self.scriptURLSFromRepositories:
            print(scriptURL)
            page = urlopen(scriptURL)
            pageSource = BeautifulSoup(page,'html.parser')
            possibleTokens = []
            for variablename in self.searchTerms:
                try:
                    indexs = []
                    for match in re.finditer(variablename, str(pageSource)):
                        indexs.append(match.end())
                    for index in indexs:
                        possibleTokens.append(str(pageSource)[index:index+64])
                except:
                    print("something went wrong")
            try:
                index = str(pageSource).find('.run(')
                if(index != -1):
                    possibleTokens.append(str(pageSource)[index+5:index+67])
            except:
                print("something went wrong")
            for possibleToken in possibleTokens:
                possibleToken = self.FormatToken(possibleToken)
                if(len(possibleToken)==59):
                    self.foundKeys.append(possibleToken)
            time.sleep(10)
            print(self.foundKeys)
        return self.foundKeys

def SendMessagesToServers(keys):
    for key in keys:
        print("Using this key "+str(key))
        client = discord.Client()
        loop = asyncio.get_event_loop()
        @client.event
        async def on_ready():
            print ("Logged in")
            for server in client.servers: 
                for channel in server.channels: 
                    if channel.permissions_for(server.me).send_messages:
                        try:
                            await client.send_message(channel, message)
                            print ("Sent message")
                            loop.stop()
                            loop.close()
                            print ("Logged out")
                            break
                        except:
                            print ("Couldn't send message")

        loop.create_task(client.start(key))
        loop.run_forever()

if __name__ == '__main__':
    numberOfPeople = pickle.load(open(os.path.join(os.path.dirname(__file__),"number.pkl"),"rb"))
    print(numberOfPeople)
    webSearcher = WebSearcher()
    webSearcher.GetRepositoriesURLS()
    webSearcher.GetScriptURLS()
    keys = webSearcher.FindTokensFromScripts()
    SendMessagesToServers(keys)
    print('done')
    numberOfPeople +=len(keys)
    pickle.dump(numberOfPeople, open(os.path.join(os.path.dirname(__file__),"number.pkl"),"wb"))