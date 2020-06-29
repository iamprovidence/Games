document.getElementById("restart").onclick = initGameObjects;

const SCALE_FACTOR = 25;


const wheelsTraceArr = [];
let b;
let hint;

let throttle = 0;

let wheelAngle = 0;
let isBraking;

// FIELDS
let car;
let canvas;
// INITIALIZE
function setup() {
    scale(20)
    createCanvas(600, 600);
    b = new Flag();
    
    carImage = loadImage("sprites/car2.png");

    initGameObjects();
        hint = new Hints(b, car);

}

function initGameObjects(){
    car = new Car(carImage);
}


// RUN
function draw() {
    background("#eee")
    
    push()
    const carPosition = car.position; 
    const carRotate = car.lookAt.angle;

    //console.log(carPosition)
   
    hint.show()
    translate(width/2, height/2)
    scale(+1, -1)
    //rotate(carRotate)
    //rotate(car.lookAt.angle * 180 /PI);
    translate(-carPosition.X*SCALE_FACTOR, -carPosition.Y*SCALE_FACTOR)
    isBraking = false;
    
    
    b.show();
    b.update(car)
            const a = 40 * PI / 180;
        if (isKeyPressed) {
            if      (key.toLowerCase() === 'w' || keyCode == UP_ARROW)     
                {
                    
                throttle += 0.2;
                }
            else if (key.toLowerCase() === 's' || keyCode == DOWN_ARROW)    
                {
                    
                throttle -= 0.2;
                }
            else{ throttle = 0}
            
            if (key.toLowerCase() === 'a' || keyCode == LEFT_ARROW)   
                {
                    wheelAngle = +a;
                }
            else if (key.toLowerCase() === 'd' || keyCode == RIGHT_ARROW)   
                {
                    wheelAngle = -a;
                }
            else 
            {
                wheelAngle = 0;
            }
            
            if (key.toLowerCase() === '')   
                {
                    isBraking = true;
                }
        }
    
            throttle = constrain(throttle, -1 ,1);
    const trace = car.update({ throttle: 0.3, steering: wheelAngle, dt: 0.01, 
                              isBraking: isBraking,
                             scaleFactor: SCALE_FACTOR});    
    wheelsTraceArr.push(...trace)
    for (const t of wheelsTraceArr)
        {
            t.show(SCALE_FACTOR)
        }
        car.show(SCALE_FACTOR);

    
pop()
}
