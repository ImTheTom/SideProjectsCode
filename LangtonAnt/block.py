import pygame

class Block:
    
    BLACK = (0, 0, 0)

    def __init__(self,x,y,width,height):
        self.rect = pygame.Rect(x, y, width, height)
        self.activated = False

    def UpdateBlock(self,screen,colour):
        '''Updates the block and paints block'''
        if(self.activated):
            pygame.draw.rect(screen, self.BLACK, self.rect)
            pygame.display.flip()
            self.activated = False
            return 0
        else:
            pygame.draw.rect(screen, colour, self.rect)
            pygame.display.flip()
            self.activated = True
            return 1