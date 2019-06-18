class Dinosaur
{
  // CONST
  private final int highestJump = 12;
  private final float gravity = 0.5;
  private final int lowestJump = 9;
  // FIELDS
  private float posX;
  private float posY;
  private float pWidth;
  private float pHeight;
  
  private float runPosY;
  
  private float jumpEffort;
  private float jumpPower;
  private boolean isDucking;
  
  private ImageAnimator imagesRun;
  private ImageAnimator imagesDuck;
  private PImage imageJump;

  // CONSTRUCTORS
  Dinosaur(float posX, float posY, float pWidth, float pHeight, 
    ImageAnimator imagesRun, ImageAnimator imagesDuck, PImage imageJump)
  {
    this.posX = posX;
    this.posY = height - posY - pHeight;// invert
    this.runPosY = this.posY;
    
    this.pWidth = pWidth;
    this.pHeight = pHeight;
    
    this.jumpEffort = 0;
    this.jumpPower = 0;
    
    this.imagesRun = imagesRun;
    this.imagesDuck = imagesDuck;
    this.imageJump = imageJump;
  }
  // METHODS
  void show()
  {
    // collision zone
    /* 
    stroke(255, 0, 0);
    noFill();
    rect(X(), Y(), W(), H());*/
    
    // image is bigger than rect area of collision
    if(inTheAir())
    {
      image(imageJump, posX - 10, posY - 10, pWidth + 20, pHeight + 20);
    }
    else if(isDucking)
    {
      image(imagesDuck.nextImage(), posX - 10, posY + 15, pWidth + 40, pHeight );
    }
    else // everyday normal guy
    {
      image(imagesRun.nextImage(), posX - 10, posY - 10, pWidth + 20, pHeight + 20);
    }
  }
  
  void move() 
  {
    // inverted cus draw in the bottom
    posY -= jumpPower;
    // if higher than it should ...
    if (inTheAir())
    {
      // ... firstly slow down on jump and then began to fall
      jumpPower -= gravity;
    } 
    else // if lower than it should ...
    {
      // ... no jump power for you, stay in running line
      jumpPower = 0;
      posY = runPosY;
    }
  }
  void checkState()
  {
    jump();
    duck();
  }
  void jump()
  {
    // if in air jump doesn work
    if(!inTheAir())
    {
      if(keyPressed && (key == ' ' || keyCode == UP))
      {
        ++ jumpEffort;
      }
      else// if key is not pressed, allmost all the time
      {
        // if no effort was done, do nothing
        if(jumpEffort == 0) return;
        
        // if key realesed calculate jump power
        if(jumpEffort > highestJump) jumpPower = highestJump;
        else if(jumpEffort < lowestJump)jumpPower = lowestJump;
        else jumpPower = jumpEffort;
        
        jumpEffort = 0;  
      }
    }
  }
  void duck()
  {
    // if in air dock doesn work
    if(!inTheAir())
    {
      if(keyPressed && keyCode == DOWN)
      {
        isDucking = true;
      }
      else// if key is not pressed, allmost all the time
      {
        isDucking = false;
      }
    }
  }
  void update()
  {
    move();
    checkState();
  }

  private boolean inTheAir()
  {
    return posY < runPosY; // inverted
  }
  // GET
  float X()
  {
    if(isDucking) return posX - 10;
    return posX;
  }
  float Y()
  {
    if(isDucking) return posY + 15;
    return posY;
  }
  float W()
  {
    if(isDucking) return pWidth + 40;
    return pWidth;
  }
  float H()
  {
    return pHeight;
  }  
}
