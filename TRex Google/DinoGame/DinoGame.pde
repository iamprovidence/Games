// CONFIGURATION
static float speed = 5;// determine not only obstacles speed but also how often they spawn
float fps = 60; // also increment speed;
float groundHeight = 80;// ground height relative to the bottom
int dotAmount = 20;
int cloudAmount = 2;
float dinoPosX = 50;
float runningLineY = 60;
float dinoWidth = 40;
float dinoHeight = 50;
int animationSpeedPerFrame = 5;

Game game;

void setup()
{
  size(800, 300);
  frameRate(fps);
  setupNewGame();    
}
void draw()
{
  background(255);
  
  game.run();
  if(game.isGameOver() && keyPressed)
  {
    setupNewGame();
  }
}
void setupNewGame()
{
  game = new Game(
    new Background(groundHeight, dotAmount, cloudAmount), 
    
    new Dinosaur(dinoPosX, runningLineY, dinoWidth, dinoHeight,
      new ImageAnimator(animationSpeedPerFrame).add(loadImage("dinoRun1.png")).add(loadImage("dinoRun2.png")),
      new ImageAnimator(10).add(loadImage("dinoDuck1.png")).add(loadImage("dinoDuck2.png")),
      loadImage("dinoJump.png")), 
    
    new Obstacles());
}
