class Bird extends Obstacle
{
  // CONSTS
  static final int typeAmount = 3;
  private final int lowFlying = 60;
  private final int middleFlying = 100;
  private final int highFlying = 120;
  // FIELDS
  private ImageAnimator images;
  // CONSTRUCTORS
  Bird(float posY, int oWidth, int oHeight, ImageAnimator images) 
  {
    super(posY, oWidth, oHeight);
    this.images = images;
  }
  Bird(float posY)
  {
    this(posY, 80, 40, new ImageAnimator(10).add(loadImage("bird1.png")).add(loadImage("bird2.png")));
  }
  Bird(){}// empty for getType call
  // METHODS
  void show() 
  {
    // collision zone
    //rect(super.posX, super.posY, super.oWidth, super.oHeight);
    image(images.nextImage(), super.posX, super.posY, super.oWidth, super.oHeight);
  }
  Obstacle getType(int type)
  {
    switch(type) 
    {
      default: 
      case 0: return new Bird(lowFlying);
      case 1: return new Bird(middleFlying);
      case 2: return new Bird(highFlying);
    }
  }
}
