class Obstacles
{
  // CONST
  private final int minimumTimeBetweenObstacles = 370;
  // FIELDS
  private ArrayList<Obstacle> obstacleList;
  private int obstacleTimer;
  // CONSTRUCTORS
  Obstacles() 
  {
    obstacleList = new ArrayList<Obstacle>();
    obstacleTimer = 0;
  }
  // METHODS
  void show()
  {
    for(int i = 0; i < obstacleList.size(); ++i)
    {
      obstacleList.get(i).show();
    }
  }
  void move()
  {
    for(int i = 0; i < obstacleList.size(); ++i)
    {
      obstacleList.get(i).move();
    }
  }
  void update()
  {
    obstacleTimer += speed;
    // if the obstacle timer is high enough then add a new obstacle
    if (obstacleTimer > minimumTimeBetweenObstacles + random(30)) 
    { 
      addObstacle();
      obstacleTimer = 0;
      // cleaning time
      if(obstacleList.size() > 20)
      {
        cleanUp();
      }
    }
    
    for(int i = 0; i < obstacleList.size(); ++i)
    {
      obstacleList.get(i).show();
      obstacleList.get(i).move();
    }
  }
  // remove all obstacles which is not showed
  void cleanUp()
  {  
    for (int i = 0; i < obstacleList.size(); ++i) 
    {
     if (obstacleList.get(i).outOfScreen()) 
     {
        obstacleList.remove(i);
        i--;//decrease the counter by one
     }
    }
  }
  void addObstacle() 
  {
    if (random(1) < 0.15) // 15% of the time add a bird
    { 
      obstacleList.add(new Bird().getType(round(random(Bird.typeAmount))));
    } 
    else // ...otherwise add a cactus
    {
      obstacleList.add(new Cactus().getType(round(random(Cactus.typeAmount))));
    }
  }
  boolean collided(float playerX, float playerY, float playerWidth, float playerHeight) 
  {
    for (int i = 0; i < obstacleList.size(); ++i)
    {
      if (obstacleList.get(i).collided(playerX,playerY,playerWidth,playerHeight))
      {
        return true;
      }
    }
    return false;
  }
}
