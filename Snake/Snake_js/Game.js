// SIZES
let gridSize;
let cols;
let rows;

// VALUES
let snake;
let fruit;
let score;
let gameSpeed;

function setup()
{
  createCanvas(600, 600);
  setCanvas();
}
function setCanvas()
{
  score = 0;
  gameSpeed = 10;
  
  noStroke();
  frameRate(gameSpeed);
  colorMode(HSB);
  
  gridSize = getGridSize();
  cols = floor(width/gridSize);
  rows = floor(height/gridSize);
  
  snake = new Snake();
  fruit = new Fruit();
}
function draw()
{
  background(220, 14, 23);
  
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
  document.getElementById("score").innerText = "Score = " + score;
  document.getElementById("speed").innerText = "Speed = " + gameSpeed;
}

document.getElementById("reset-btn").onclick = setCanvas;

function getGridSize(defaultValue = 20)
{
  // gather data
  let input = document.getElementById("grid-size-input");
  let value = Number.parseFloat(input.value);

  let min = Number.parseFloat(input.getAttribute("min"));
  let max = Number.parseFloat(input.getAttribute("max"));

  // validate
  let returnValue = (function ()
  {
    if (Number.isNaN(value)) return defaultValue;

    if (value < min) return min;
    else if (value > max) return max;
    else return value;
  })();

  // update view, and return proper value
  input.value = returnValue;
  return returnValue;
}