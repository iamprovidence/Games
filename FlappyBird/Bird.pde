class Bird
{
  // CONST
  private final float birdSize = 30;
  
  // FILEDS
  private float x;
  private float y;
  private float gravity;
  private float velocity;
  private float jump;
  private ImageAnimator images;
  // CONSTRUCTORS
  Bird(ImageAnimator img)
  {
    x = 25;
    y = height/2;
    gravity = 0.9;
    velocity = 0;
    jump = 4;
    images = img;
  }
  // METHODS
  void reset()
  {
    x = 25;
    y = height/2; 
  }
  void show()
  {
    /* 
    collision zone
    fill(255);
    ellipse(this.x, this.y, birdSize, birdSize);
    */
    
    image(images.nextImage(), x-birdSize/2, y-birdSize/2, birdSize, birdSize);
  }
  void update()
  {
    this.velocity += this.gravity; 
    this.velocity *= 0.9;
    this.y += this.velocity;
    
    this.y = constrain(this.y, 10, height);
    // jump
    if (keyPressed && key == ' ')
    {
      this.velocity -= this.jump; 
    }
  }
}
