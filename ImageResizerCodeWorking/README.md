# Image Resizer

This program is a small python program which automatically creates thumbnail images for my website.

## What it does
When placed inside a directoy and ran it will look for .jpg images and see's if there is already a thumbnail created for that image, if not it creates a new image to the size set in the settings json file in src with the name of its original name plus the extension given in the same json file. A log file is also created in src.

### Requirements
* Python 3+
* Pillow
* Json

For installation guides just google it.

#### Reason for creation
I run my own website because I can http://www.tombowyer.me/ One page I display my side projects that I create. On that page I have images of the project in which I capture one full size image and then resize it to a smaller size. Sometimes I forget to the dimensions. This removes that trouble.