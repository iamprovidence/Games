class Spot
{
  // FIELDS
  float x;
  float y;
  // CONSTRUCTORS
  Spot(float x, float y)
  {
    this.x = x;
    this.y = y;
  }
  // METHODS
  boolean collision(Spot s2)
  {
    // 2 square collision
    return x + gridSize > s2.x            && // s1 right edge past s2 left
           x            < s2.x + gridSize && // s1 left edge past s2 right
           y + gridSize > s2.y            && // s1 top edge past s2 bottom
           y            < s2.y + gridSize;   // s1 bottom edge past s2 top
  }
}
