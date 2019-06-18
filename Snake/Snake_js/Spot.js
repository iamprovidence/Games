class Spot
{  
  // CONSTRUCTORS
  constructor(x, y)
  {
    this.x = x;
    this.y = y;
  }

  // METHODS
  collision(spot2)
  {
    // 2 squares collision
    return  this.x + gridSize > spot2.x            && // s1 right edge past s2 left
            this.x            < spot2.x + gridSize && // s1 left edge past s2 right
            this.y + gridSize > spot2.y            && // s1 top edge past s2 bottom
            this.y            < spot2.y + gridSize;   // s1 bottom edge past s2 top
  }
}
