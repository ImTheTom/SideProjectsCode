import json
from src import Logging
from src import ImageResizerFunctions

def PrintStartMessages():
    print("Thumbnail Creator\n")
    print("Starting Program\n")
    Logging.StartUpLog()

#Note by extension it means if you have a file called website.jpg it will create a thumbnail called website + your 
#extension + .jpg
def PrintThumbnailExtension():
    thumbnailExtension= ImageResizerFunctions.GetThumbnailExtension()
    print("Looking and savings thumbnails with the "+thumbnailExtension+" extension. You can edit this in the"+
           ".json file located in src. If this wasn't what you set it to be. The program is having difficulty"+
           " in locating the json file\n")

def PrintThumbnailDimensions():
    thumbnailDimensions = ImageResizerFunctions.GetThumbnailSize()
    print("Thumbnails will be created at "+ str(thumbnailDimensions[0]) +" and " +str(thumbnailDimensions[1])+"."+
           "If you did not set these values the program is having trouble in the json file\n")

def PrintImageRequiringThumbnail(file):
    print("A thumbnail for " +file+" is required. Creation will occur later.")

def PrintThumbnailSaved(file):
    print("A thumbnail for "+ file +" has been created and saved!")
    Logging.ThumbnailCreated(file)

def PrintThatListIsEmpty():
    print("An array found was empty please check that I'm in the right directory and start again!")

def CouldNotPerformLog():
    print("Could not perform logs please check im set up correctly")

def PrintFinalMessage():
    print("\nPressing Space will now close the program.")
    Logging.ShutDownLog()