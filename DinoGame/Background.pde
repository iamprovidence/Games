class Background
{
  // FIELDS
  private float groundHeight;
  private ArrayList<BackgroundDot> dots;
  private ArrayList<BackgroundCloud> clouds;

  // CONSTRUCTORS
  Background(float groundHeight, int dotAmount, int cloudAmount)
  {
    this.groundHeight = height - groundHeight;// invert
    
    dots = new ArrayList<BackgroundDot>(dotAmount);
    clouds = new ArrayList<BackgroundCloud>(cloudAmount);
    for (int i = 0; i < dotAmount; ++ i)
    {
      dots.add(new BackgroundDot(this.groundHeight));
    }
    for(int i = 0; i < cloudAmount; ++i)
    {
      clouds.add(new BackgroundCloud(this.groundHeight));
    }
  }

  // METHODS
  void show()
  {
    // ground
    stroke(0);// line get black color
    strokeWeight(3);
    line(0, groundHeight, width, groundHeight);
    // dots
    for(int i = 0; i < dots.size(); ++i)
    {
      dots.get(i).show();
    }
    // clouds
    for(int i = 0; i < clouds.size(); ++i)
    {
      clouds.get(i).show();
    }
  }
  void move() 
  {
    for(int i = 0; i < dots.size(); ++i)
    {
      dots.get(i).update();
    }
    for(int i = 0; i < clouds.size(); ++i)
    {
      clouds.get(i).update();
    }
  }
  void update()
  {
    move();
  }
}
