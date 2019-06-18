class BackgroundDot
{
  // FIELDS
  private float dotPosX;
  private float dotPosY;
  private int dotWidth;
  private float dotHeight;
  private float upperLimit;
  // CONSTRUCTORS
  BackgroundDot(float upperLimit)
  {
    this.upperLimit = upperLimit;
    // dot apear on random height between bootom and ground, with little offset
    dotPosY = random(this.upperLimit +10, height - 10);
    dotPosX = width + random(width);// dot appear on right side behind the screen    
    dotWidth = round(random(1, 15));
    dotHeight = random(2, 3);
  }
  // METHODS
  void show()
  {
    stroke(0);// dot get black color
    strokeWeight(dotHeight);
    line(dotPosX, dotPosY, dotPosX + dotWidth, dotPosY);
  }
  void move()
  {
    dotPosX -= speed;
  }
  void update()
  {
     move();
     // if dot behind the screen in left side
     if(dotPosX + dotWidth < 0)
     {
       // dot appear on right side behind the screen
       // basically show it again
       dotPosX = width + random(width); 
     }
  }
}
