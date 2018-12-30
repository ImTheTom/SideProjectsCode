import os
import pygame
from pygame.locals import *
import random
from time import time, sleep
from ant import Ant
from block import Block

BLOCKSIZE = 4
WIDTH = 600
HEIGHT = 480
ROWS = round(WIDTH/BLOCKSIZE)
COLUMNS = round(HEIGHT/BLOCKSIZE)

WHITE = (255, 255, 255)
BLACK = (0, 0, 0)
RED = (255, 0, 0)
GREEN = (0,255,0)
BLUE = (0,0,255)
PURPLE = (150,0,255)
PINK = (255,0,255)
YELLOW = (255,255,0)
LIME = (150,255,0)
LIGHTBLUE = (0,255,255)
CYAN = (0,255,150)

COLOURS = [WHITE,RED,GREEN,BLUE,PURPLE,PINK,YELLOW,LIME,LIGHTBLUE,CYAN]

def setup():
    '''Sets up the game and blocks'''
    global clock, blocks, screen
    os.environ["SDL_VIDEO_CENTERED"] = "1"
    pygame.init()

    pygame.display.set_caption("Langton's Ant")
    screen = pygame.display.set_mode((WIDTH, HEIGHT))
    screen.fill(BLACK)
    pygame.display.flip()

    clock = pygame.time.Clock()

    blocks = []
    for x in range(ROWS):
        for y in range(COLUMNS):
            blocks.append(Block(x*BLOCKSIZE,y*BLOCKSIZE,BLOCKSIZE,BLOCKSIZE))

def getIndex(coordinates):
    '''Gets the index of the block determined by ant coordinates'''
    return coordinates[0]*COLUMNS+coordinates[1]

def game():
    '''Runs the Langton's Ant game'''
    setup()
    running = True
    ants = []
    generation = 0

    while running:
        
        clock.tick(200)

        for event in pygame.event.get():

            if(event.type == QUIT):
                return

            if(event.type == MOUSEBUTTONDOWN):
                x,y = event.pos
                x = round(x/BLOCKSIZE)
                y = round(y/BLOCKSIZE)
                ants.append(Ant(0,x,y,BLOCKSIZE,random.choice(COLOURS),ROWS,COLUMNS))

            if(event.type == KEYDOWN):

                if event.key == K_SPACE:
                    screen.fill(BLACK)
                    pygame.display.flip()
                    generation = 0
                    del ants[:]

        if(len(ants)!=0):
            for ant in ants:
                antLocationIndex = getIndex(ant.GetLocation())
                response = blocks[antLocationIndex].UpdateBlock(screen, ant.GetColour())
                ant.Move(response)
            generation += 1
        
        title = "Langton's Ant Generation - %d"%generation
        
        pygame.display.set_caption(title)
        pygame.display.update()

if __name__ == '__main__':
    game()