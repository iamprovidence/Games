// IMPORTS
import java.util.Queue; 
import java.util.ArrayDeque;
// CONSTANTS
final float gridSize = 20;
float cols;
float rows;
// VALUES
Snake snake;
Fruit fruit;
int score;
float gameSpeed;

void setup()
{
  size(600, 600);
  
  score = 0;
  gameSpeed = 10;
  
  noStroke();
  frameRate(gameSpeed);
  colorMode(HSB);
  
  cols = floor(width/gridSize);
  rows = floor(height/gridSize);
  
  snake = new Snake();
  fruit = new Fruit();
}
void draw()
{
  background(51);
  
  score -= snake.update();
  snake.show();
  
  if(snake.eat(fruit))
  {
    fruit.spawn();
    snake.grow();
    
    ++ score;
    gameSpeed +=0.5;
    frameRate(gameSpeed);
  }
  
  fruit.show();
  
  textSize(15);
  fill(255);
  text("score = " + score, 10, 25);
  text("speed = " + gameSpeed, 10, 45);
}
