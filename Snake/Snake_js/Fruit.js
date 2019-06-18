class Fruit
{
  // CONSTRUCTORS
  constructor()
  {
    this.position = new Spot(0, 0);
    this.colorValue = null;
    this.spawn();
  }
  // METHODS
  show()
  {    
    fill(this.colorValue, 255, 255);
    rect(this.position.x, this.position.y, gridSize, gridSize);
  }
  spawn()
  {
    this.colorValue = random(360);
    this.position.x = floor(random(cols)) * gridSize;
    this.position.y = floor(random(rows)) * gridSize;
  }
}
