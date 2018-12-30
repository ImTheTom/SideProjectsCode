from src import HelpMessages
from src import Logging
import json

def GetThumbnailExtension():
    try:
        settingsFromJsonFile = json.load(open('src/Settings.json'))
        return settingsFromJsonFile["Settings"]["name"]
    except:
        return "thumb"

def GetThumbnailSize():
    dimensions = [0,0]
    try:
        settingsFromJsonFile = json.load(open('src/Settings.json'))
        dimensions[0] = settingsFromJsonFile["Settings"]["width"]
        dimensions[1] = settingsFromJsonFile["Settings"]["height"]
        return dimensions
    except:
        dimensions[0] = 350
        dimensions[1] = 225
        return dimensions

def GetImagesInsideDirectoy(listOfFiles):
    imagesInsideDirectory = []
    for directoyFile in listOfFiles:
        if '.jpg' == directoyFile[-4:]:
            imagesInsideDirectory.append(directoyFile)
    return imagesInsideDirectory

def CheckIfListIsEmpty(listofFilesOrImages):
    if len(listofFilesOrImages) == 0:
        HelpMessages.PrintThatListIsEmpty()
    
def GetImagesNeedingThumbnail(listOfImages):
    imagesNeedingThumbnail=[]
    thumbnailExtension= GetThumbnailExtension()
    for imageFile in listOfImages:
        currentImageWithoutJpg = imageFile[:-4]
        if(thumbnailExtension not in currentImageWithoutJpg):
            if(currentImageWithoutJpg+thumbnailExtension+".jpg" not in listOfImages):
                imagesNeedingThumbnail.append(imageFile)
                HelpMessages.PrintImageRequiringThumbnail(imageFile)
    return imagesNeedingThumbnail

def CreateNewFileName(imageFileName):
    thumbnailExtension = GetThumbnailExtension()
    imageFileWithoutjpg = imageFileName[:-4]
    newFileNameWithThumbAndJpg = imageFileWithoutjpg+thumbnailExtension+".jpg"
    return newFileNameWithThumbAndJpg