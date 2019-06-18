class Snake
{
  // CONSTRUCTORS
  constructor()
  {
    // spawn snake at the middle
    this.head = new Spot(floor(cols/2) * gridSize, floor(rows/2) * gridSize);
    
    this.xDirection = gridSize;
    this.yDirection = 0;

    this.colorValue = random(360);

    this.tail = [];
  }
  
  // METHODS
  update()
  {
    // shift tail
    this.tail.push(new Spot(this.head.x, this.head.y));
    this.tail.shift();

    // move head
    this.head.x += this.xDirection;
    this.head.y += this.yDirection;

    // out screen, spawn snake to another side
    if      (this.head.x >= width)  this.head.x = 1;
    else if (this.head.x < 0)       this.head.x = width;
    else if (this.head.y >= height) this.head.y = 1;
    else if (this.head.y < 0)       this.head.y = height;

    // user interaction
    this.key();
    
    // eat own tail, tail bacame shorter, lose score
    
    // calc tail eaten length
    let tailEaten = 0;
    for (let t of this.tail)
    {
      if (t.collision(this.head)) break;
      ++ tailEaten;
    }
    tailEaten = tailEaten == this.tail.length ? 0 : tailEaten;// go to end with collizion or without
    // remove eaten tail
    for(let i = 0; i < tailEaten; ++i) this.tail.shift();
    
    return tailEaten;
  }
  show()
  {
    // head
    fill(this.colorValue, 255, 100);// 70 stands for green
    rect(this.head.x, this.head.y, gridSize, gridSize);

    // tail
    let saturation = 0;
    for (let t of this.tail)
    {
      fill (this.colorValue, 75 + saturation, 100);
      rect (t.x, t.y, gridSize, gridSize);
      saturation += 5;
    }
  }
  key()
  {
    if (isKeyPressed)
    {
      if      (keyCode == UP_ARROW)    this.changeDirection(        0,  -gridSize);
      else if (keyCode == DOWN_ARROW)  this.changeDirection(        0,  +gridSize);
      else if (keyCode == LEFT_ARROW)  this.changeDirection(-gridSize,          0);
      else if (keyCode == RIGHT_ARROW) this.changeDirection(+gridSize,          0);
    }
  }
  changeDirection(xDirection, yDirection)
  {
    this.xDirection = xDirection;
    this.yDirection = yDirection;
  }
  eat(fruit)
  {
    return this.head.collision(fruit.position);
  }
  grow()
  {
    this.tail.push(new Spot(this.head.x, this.head.y));
  }
}
