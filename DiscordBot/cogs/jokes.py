import asyncio
import discord
from discord.ext import commands
from random import *

yoMumma = ["Yo momma is so fat, I took a picture of her last Christmas and it's still printing.",
           'Yo momma is so fat when she got on the scale it said, "I need your weight not your phone number."',
           "Yo momma's so fat, that when she fell, no one was laughing but the ground was cracking up.",
           'Yo mamma is so ugly when she tried to join an ugly contest they said, "Sorry, no professionals."',
           "Yo momma's so fat and old when God said, 'Let there be light,' he asked your mother to move out of the way.",
           "Yo momma is so fat that when she went to the beach a whale swam up and sang, 'We are family, even though you're fatter than me.'",
           "Yo momma is so fat when she sat on WalMart, she lowered the prices.",
           "Your momma is so ugly she made One Direction go another direction.",
           "Yo momma is so fat that Dora can't even explore her!",
           "Yo momma is so stupid when an intruder broke into her house, she ran downstairs, dialed 9-1-1 on the microwave, and couldn't find the 'CALL' button.",
           "Yo momma is so fat her bellybutton gets home 15 minutes before she does.",
           "Yo momma's so stupid, she put two quarters in her ears and thought she was listening to 50 Cent.",
           "Yo mamma is so fat she doesn't need the internet, because she's already world wide.",
           "Yo momma's so dumb, when y'all were driving to Disneyland, she saw a sign that said 'Disneyland left' so she went home.",
           "Yo momma is so fat, when she sat on an iPod, she made the iPad!",
           'Yo momma so stupid she stuck a battery up her ass and said, "I GOT THE POWER!"',
           'Yo Momma is so fat when I told her to touch her toes she said, "What are those"?',
           'Yo momma is so stupid she climbed over a glass wall to see what was on the other side.',
           'Yo momma is so hairy, when she went to the movie theater to see Star Wars, everybody screamed and said, "IT IS CHEWBACCA!"',
           'Yo momma is so stupid she brought a spoon to the super bowl.']

dadJokes = ["What's the difference between a good joke and a bad joke timing.",
            "I met my wife on the net; we were both bad trapeze artists.",
            "What's the difference between in-laws and outlaws? Outlaws are wanted.",
            "Went to KFC the other day, didn't know Kentucky had a football club.",
            "What's the leading cause of dry skin? Towels",
            "Do I enjoy making courthouse puns? Guilty.",
            "I tell you what often gets overlooked - garden fences.",
            "I saw an ad in a shop window that said “Television for Sale – £1- Volume Stuck On Full”. I thought: “I can’t turn that down”.",
            "My dog used to chase people on a bike a lot. It got so bad, I eventually had to take his bike off him.",
            "What's a marsupial's favourite cocktail? A piña koala.",
            "Shout out to my grandma, that's the only way she can hear.",
            "Today's top fact: 50% of Canada is A",
            "3.14% of sailors are pi-rates."]

mixJokes = ["Q: What do you get when you cross a cow with a trampoline? A: A milkshake!",
            "Q: What do you get when you cross a ghost and a cat ? A: A scaredy cat!",
            "Q: What do you get when you cross a karate expert with a pig? A: A porkchop.",
            "Q: What do you get when you cross a tiger and a blizzard? A: Frostbite!",
            "Q: What do you get when you cross a cow and a lawnmower? A: A lawnmooer.",
            "Q: What do you get if you cross a kangaroo and a elephant? A: Big holes all over Australia!",
            "Q: What do you get if you cross cat with an elephant? A: A flat cat."]

knockknock = ["Knock, knock. Who’s there? Canoe. Canoe who? Canoe help me with my homework?",
              "Knock, knock. Who’s there? Orange. Orange who? Orange you going to let me in?",
              "Knock, knock. Who’s there? Dozen.Dozen who? Dozen anybody want to let me in?",
              "Knock, knock. Who’s there? Avenue.Avenue who? Avenue knocked on this door before?",
              "Knock, knock. Who’s there? A herd. A herd who? A herd you were home, so I came over!",
              "Knock, knock. Who’s there? Lettuce. Lettuce who? Lettuce in it’s cold out here.",
              "Knock, knock. Who’s there? Dwayne. Dwayne who? Dwayne the bathtub, It’s overflowing!",
              "Knock, knock. Who’s there? Boo. Boo who? Gosh, don’t cry it’s just a knock knock joke.",
              "Knock, knock. Who’s there? Justin. Justin who? Justin time for dinner.",
              "Knock, knock. Who’s there? Luke. Luke who? Luke through the the peep hole and find out."]

otherJokes = ["Muh vagina"]
              
              
class Jokes:
    """The bot's funny side"""

    def __init__(self, bot):
        self.bot=bot

    @commands.command(pass_context=True, description="Tells a joke")
    async def joke(self, ctx):
        """Tells a joke"""
        channel = ctx.message.channel
        await self.bot.send_typing(channel)
        jokes = dadJokes+mixJokes+knockknock+yoMumma+otherJokes
        joke = choice(jokes)
        await self.bot.say(joke)
        
