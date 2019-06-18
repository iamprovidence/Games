class BackgroundCloud
{
  // FIELDS
  private float cPosX;
  private float cPosY;
  private int cWidth;
  private float cHeight;
  private float lowerLimit;
  private PImage image;
  // CONSTRUCTORS
  BackgroundCloud(float lowerLimit)
  {  
    cWidth = 80;
    cHeight = 40;
    
    this.lowerLimit = lowerLimit - (cHeight + 10);
    // cloud apear on random height between top and ground, with little offset
    cPosY = random(0, this.lowerLimit);
    cPosX = width + random(width / 2);// cloud appear on right side behind the screen
    image = loadImage("cloud.png");
  }
  // METHODS
  void show()
  {
    image(image, cPosX, cPosY, cWidth, cHeight);
  }
  void move()
  {
    cPosX -= speed / 3;
  }
  void update()
  {
     move();
     // if cloud behind the screen in left side
     if(cPosX + cWidth < 0)
     {
       // cloud appear on right side behind the screen
       // basically show it again
       cPosX = width + random(width); 
       cPosY = random(0, this.lowerLimit);
     }
  }
}
