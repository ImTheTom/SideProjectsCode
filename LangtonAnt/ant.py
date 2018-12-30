class Ant:

    def __init__(self,facing,x,y,size,colour,maxX,maxY):
        self.facing = facing
        self.x = x
        self.y = y
        self.size = size
        self.colour = colour
        self.maxX = maxX
        self.maxY = maxY

    def Move(self, blockStatus):
        '''Moves ant determined on blockStatus'''
        if(blockStatus==0):
            self.facing = (self.facing-1)%self.size
        else:
            self.facing = (self.facing+1)%self.size
        
        if(self.facing==0):
            self.x -= 1
        elif(self.facing==1):
            self.y -= 1
        elif(self.facing==2):
            self.x += 1
        else:
            self.y+=1

        if(self.x>=self.maxX):
            self.x = 0
        elif(self.x<0):
            self.x=self.maxX-1

        if(self.y>=self.maxY):
            self.y=0
        elif(self.y<0):
            self.y = self.maxY-1
        
    def GetLocation(self):
        '''Returns the ant location in vector'''
        return [self.x,self.y]

    def GetColour(self):
        '''Returns the ant colour'''
        return self.colour