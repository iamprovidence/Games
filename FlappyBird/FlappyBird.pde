Bird bird;
Pipes pipes;
Background background;
boolean isGameOver;

void setup()
{
  size(288, 512);// the same as background image size
  bird = new Bird(new ImageAnimator(20)
    .add(loadImage("bird_up.png"))
    .add(loadImage("bird_middle.png"))
    .add(loadImage("bird_down.png")));
  pipes = new Pipes(loadImage("pipe_down.png"), loadImage("pipe_up.png"));
  background = new Background(loadImage("background.png"), loadImage("base.png"), loadFont("AgencyFB-Bold-48.vlw"));
  isGameOver = false;
}

void draw()
{
    background(background.getBackground());// back    
    
    // updating everything
    bird.update();
    bird.show();

    pipes.update(); 
    pipes.show();
    
    // grass are showing upon pipe    
    background.show();
    
    // check colision
    if(background.collided(bird) || pipes.collided(bird))
    {
      // end game logic      
      isGameOver = true;
      
      fill(0);
      textFont(background.getFont());
      textAlign(CENTER, CENTER);       
      text("game over", width/2, height/2);
      noLoop();
    }      
}

void keyPressed()
{
  if (isGameOver)
  {
    isGameOver = false;
    bird.reset();
    pipes.reset();
  
    loop();
  }
}
