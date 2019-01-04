class Snake
{
  // FIELDS
  private Spot head;
  private float xDirection;
  private float yDirection;
  private Queue<Spot> tail;
  private float colorValue;
  // CONSTRUCTORS
  Snake()
  {
    // spawn snake at the middle
    head = new Spot(floor(cols/2) * gridSize, floor(rows/2) * gridSize);
    
    xDirection = gridSize;
    yDirection = 0;
    colorValue = random(360);

    tail = new ArrayDeque<Spot>();
  }
  // METHODS
  int update()
  {
    // shift tail
    tail.add(new Spot(head.x, head.y));
    tail.remove();

    // move head
    head.x += xDirection;
    head.y += yDirection;

    // out screen, spawn snake to another side
    if      (head.x >= width)  head.x = 1;
    else if (head.x < 0)       head.x = width;
    else if (head.y >= height) head.y = 1;
    else if (head.y < 0)       head.y = height;

    // user interaction
    key();
    
    // eat own tail, tail bacame shorter, lose score
    
    // calc tail eaten length
    int tailEaten = 0;
    for (Spot t : tail)
    {
      if (t.collision(head)) break;
      ++ tailEaten;
    }
    tailEaten = tailEaten == tail.size() ? 0 : tailEaten;// go to end with collizion or without
    // remove eaten tail
    for(int i = 0; i < tailEaten; ++i) tail.remove();
    
    return tailEaten;
  }
  void show()
  {
    // head
    fill(colorValue, 255, 255);// 70 stands for green
    rect(head.x, head.y, gridSize, gridSize);

    // tail
    int saturation = 0;
    for (Spot t : tail)
    {
      fill (colorValue, 150+saturation, 255);
      rect (t.x, t.y, gridSize, gridSize);
      saturation += 5;
    }
  }
  void key()
  {
    if (keyPressed && key == CODED)
    {
      if      (keyCode == UP)    changeDirection(        0,  -gridSize);
      else if (keyCode == DOWN)  changeDirection(        0,   gridSize);
      else if (keyCode == LEFT)  changeDirection(-gridSize,          0);
      else if (keyCode == RIGHT) changeDirection( gridSize,          0);
    }
  }
  private void changeDirection(float xDirection, float yDirection)
  {
    this.xDirection = xDirection;
    this.yDirection = yDirection;
  }
  boolean eat(Fruit fruit)
  {
    return head.collision(fruit.position);
  }
  void grow()
  {
    tail.add(new Spot(head.x, head.y));
  }
}
