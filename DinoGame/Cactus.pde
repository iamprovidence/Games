class Cactus extends Obstacle
{
  // CONSTS
  static final int typeAmount = 3;
  private final String smallCactus = "cactusSmall.png";
  private final String manySmallCactus = "cactusSmallMany.png";
  private final String bigCactus = "cactusBig.png";
  // FIELDS
  private PImage image;
  // CONSTRUCTORS
  Cactus(float posY, int oWidth, int oHeight, PImage image) 
  {
    super(posY, oWidth, oHeight);
    this.image = image;
  }
  Cactus(int cWidth, int cHeight, String imgName)
  {
    this(runningLineY, cWidth, cHeight, loadImage(imgName));
  }
  Cactus(){}// empty for getType call
  // METHODS
  void show() 
  {
    // collision zone
    //rect(super.posX, super.posY, super.oWidth, super.oHeight);
    image(image, super.posX, super.posY, super.oWidth, super.oHeight);
  }
  Obstacle getType(int type)
  {
    switch(type) 
    {
      default: 
      case 0: return new Cactus(20, 40, smallCactus);
      case 1: return new Cactus(80, 50, manySmallCactus);
      case 2: return new Cactus(40, 80, bigCactus);
    }
  }
}
