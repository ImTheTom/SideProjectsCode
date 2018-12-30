from PIL import Image
import os
from src import ImageResizerFunctions
from src import HelpMessages
from src import Logging

thumbnailSize = ImageResizerFunctions.GetThumbnailSize()

HelpMessages.PrintStartMessages()

directoryPath = os.getcwd()
directoyContents = os.listdir(directoryPath)

HelpMessages.PrintThumbnailExtension()
HelpMessages.PrintThumbnailDimensions()

imagesInsideDirectory = ImageResizerFunctions.GetImagesInsideDirectoy(directoyContents)
ImageResizerFunctions.CheckIfListIsEmpty(directoyContents)
imagesThatNeedThumbnail = ImageResizerFunctions.GetImagesNeedingThumbnail(imagesInsideDirectory)
ImageResizerFunctions.CheckIfListIsEmpty(imagesThatNeedThumbnail)


for imageFile in imagesThatNeedThumbnail:
    imageWithoutThumbnail = Image.open(imageFile)
    newThumbnailImageFileName = ImageResizerFunctions.CreateNewFileName(imageFile)
    imageWithoutThumbnail.thumbnail(thumbnailSize, resample=0)
    imageWithoutThumbnail.save(newThumbnailImageFileName, "JPEG")
    HelpMessages.PrintThumbnailSaved(imageFile)
    

HelpMessages.PrintFinalMessage()
os.system("pause")
