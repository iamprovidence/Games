class Game
{
  // FIELDS
  private Background background;
  private Dinosaur dinosaur;
  private Obstacles obstacles;
  private boolean isGameOver;
  private PFont font;
  private int score;
  private float gameSpeedBoosted;
  
  // CONSTRUCTORS
  Game(Background background, Dinosaur dinosaur, Obstacles obstacles)
  {
    this.background = background;
    this.dinosaur = dinosaur;
    this.obstacles = obstacles;
    this.isGameOver = false;
    this.font = createFont("dinoFont.vlw", 32);
    this.score = 0;
    this.gameSpeedBoosted = 0;
  }
  void show()
  {
    fill(0);
    textAlign(RIGHT);
    textFont(font);
    text(score, width , 25);// right upper corner
  }
  // METHODS
  void run()
  {
    if(!isGameOver)
    {
      background.show();
      background.update();
      
      dinosaur.show();
      dinosaur.update();
      
      obstacles.update();
      checkState();
      
      ++score;
      // every 100 score
      if(score % 100 == 0)
      {
        // speed up game
        speed += 0.1;
        // remember how much boost we get, so when game is over, set speed to normal
        gameSpeedBoosted += 0.1;
      }
      show();
    }
    else
    {
      fill(0);
      textAlign(CENTER, CENTER);
      textFont(font);
      text("game over", width/2, height/3);
      text(score, width/2, height/3 + 50);
      image(loadImage("playAgain.png"), width /2 - 25, height/3 + 100, 50, 50);
    }
  }
  void checkState()
  {
    if(obstacles.collided(dinosaur.X(), dinosaur.Y(),dinosaur.W(),dinosaur.H()))
    {
      speed -= gameSpeedBoosted;
      gameSpeedBoosted = 0;
      isGameOver = true;
    }
  }
  boolean isGameOver()
  {
    return isGameOver;
  }
}
