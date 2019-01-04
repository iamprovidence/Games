class Fruit
{
  // FIELDS
  private Spot position;
  private float colorValue;
  // CONSTRUCTORS
  Fruit()
  {
    position = new Spot(0, 0);
    spawn();
  }
  // METHODS
  void show()
  {    
    fill(colorValue, 255, 255);
    rect(position.x, position.y, gridSize, gridSize);
  }
  void spawn()
  {
    colorValue = random(360);
    position.x = floor(random(cols)) * gridSize;
    position.y = floor(random(rows)) * gridSize;
  }
}
