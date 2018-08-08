abstract class Obstacle
{
  // FIELDS
  private float posX;
  private float posY;
  private int oWidth;
  private int oHeight;
  
  // CONSTRUCTORS
  Obstacle(float posY, int oWidth, int oHeight) 
  {
    this.posX = width;
    this.oWidth = oWidth;
    this.oHeight = oHeight;
    this.posY = height - posY - oHeight;
  }
  Obstacle() {}// empty for getType call

  // ABSTRACT METHODS
  abstract void show();
  abstract Obstacle getType(int type);
  // METHODS
  void move() 
  {
    posX -= speed;
  }
  void update()
  {
     move();
  }
  boolean outOfScreen()
  {
     // if obstacle behind the screen in left side
     return posX + oWidth < 0;
  }
  boolean collided(float playerX, float playerY, float playerWidth, float playerHeight) 
  {
    return
      playerX + playerWidth >= posX &&      // player right edge past obstacle left
      playerX <= posX + oWidth &&           // player left edge past obstacle right
      playerY + playerHeight >= posY &&     // player top edge past obstacle bottom
      playerY <= posY + oHeight;            // player bottom edge past obstacle top
  }
}
