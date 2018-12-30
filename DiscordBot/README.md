# Discord Bot

The bot that was created was mainly for my own server and for my own side project. 

This code is a prime example of
* Functions doing multiple things (which is bad)
* Names of variables being not that explicit (which is bad)
* Straight up ugly code

For those wanting to run it on your own server continue to read.

### Note: I'm not responsible for anything that happens if you host this bot on your server and will not be liable for anything that happens to your computer.

The bot can be summoned by the ! key.

Requirements: Python 3.4+, discord.py[voice], time, dateitime, os, sys, requests, json, beautifulsoup4, requests, random, re, logging,
discord.ext sys, importlib and FFmpeg.

Requirements installation: Python3.4+ - https://www.python.org/. FFmpeg can be downloaded from - https://ffmpeg.org/ and then follow an installation guide. For the rest just open cmd and type pip install (module)*Note you need to have python installed first.* If the console says the module can't be found open up google and search "(module) python installation guide" to get a detailed installation guide *hopefully.*

You will also have to uncomment the reset function in points.py and call it or call the update function or have someone join the server for the points functionality to work.

There are also some hard coded directories as well they may need to be changed.

the bot.run parameter will also have to be changed to your own token. The loader and handle variables can also be deleted since they are only there so people can't get my server's token id.

Big thanks to everyone that worked on the discord.py API reference: http://discordpy.readthedocs.io/en/latest/api.html and discord.py itself.