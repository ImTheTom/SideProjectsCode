import importlib.machinery
import csv
from imgurpython import ImgurClient
from datetime import datetime

def SetUp():
    loader = importlib.machinery.SourceFileLoader('imgurtokens','/home/pi/Desktop/Code/imgurtokens.py')
    handle = loader.load_module('imgurtokens')

    clientID = handle.ClientID()

    ClientSecretID = handle.ClientSecret()

    client = ImgurClient(clientID, ClientSecretID)
    return client

def Upload(client):

    album = None
    image_path = 'storage/graph.png'
	# Here's the metadata for the upload. All of these are optional, including
	# this config dict itself.
    config = {
		'album': album,
		'name':  'Runesacpe Drops Graph',
		'title': 'Runesacpe Drops Graph',
		'description': 'This was an automated upload for the twitter account: https://twitter.com/OSRS_Drop_Stats?lang=en' 
	}
    print("Uploading image... ")
    image = client.upload_from_path(image_path, config=config, anon=False)
    print("Done")
    print()
    return image

def GetLink(image):
    return image['link']
