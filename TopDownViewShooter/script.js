document.getElementById("restart").onclick = initGameObjects;

// CONFIG
const VIEW_ZONE_COLOR               = "#cecece";
const CREATE_ZOMBIES_ON_EVERY_FRAME = 100;

// FIELDS
let playerImage;
let zombieImage;

let score;
let player;
let zombies;
let fogOfWar;
let bullets;
let obstacles;

// INITIALIZE
function setup() {
    cursor(CROSS);
    createCanvas(600, 600);
    
    playerImage = loadImage("sprites/player.png");
    zombieImage = loadImage("sprites/zombie.png");
    fogOfWar = new FogOfWar(color(VIEW_ZONE_COLOR));

    obstacles = getObstacles();

    initGameObjects();
}

function initGameObjects(){
    player = new Player(playerImage);
    zombies = [];
    bullets = [];
    score = 0;
}

function getObstacles() {
    return [
        new Obstacle(
        [ 
            { x: 0,     y: 0      },
            { x: 0,     y: height },
            { x: width, y: height },
            { x: width, y: 0      }
        ]),
        new Obstacle(
        [
            { x: 50,    y: 50   },
            { x: 200,   y: 50   },
            { x: 200,   y: 100  },
            { x: 50,    y: 100  }
        ], "#816E60"),
        new Obstacle(
        [
            { x: 400, y: 50  },
            { x: 450, y: 50  },
            { x: 500, y: 100 },
            { x: 500, y: 150 },
            { x: 450, y: 200 },
            { x: 400, y: 200 },
            { x: 350, y: 150 },
            { x: 350, y: 100 }
        ], "#900C3E"),
        new Obstacle(
        [
            { x: 400, y: 350 },
            { x: 500, y: 400 },
            { x: 350, y: 450 },
        ], "#32324E"),
        new Obstacle(
        [
            { x: 50,  y: 300 },
            { x: 175, y: 300 },
            { x: 250, y: 400 },
            { x: 250, y: 500 },
            { x: 200, y: 500 },
            { x: 200, y: 425 },
            { x: 150, y: 350 },
            { x: 50,  y: 350 },
        ], "#51A380")
    ]
}

// RUN
function draw() {
    clear();
    createNewZombie(CREATE_ZOMBIES_ON_EVERY_FRAME);
    
    fogOfWar.show(obstacles, player.position)

	fogOfWar.showOnlyObservedObjects(true);
    showZombies();
    showBullets();
	fogOfWar.showOnlyObservedObjects(false);

    for (const obstacle of obstacles) {
        obstacle.show();
    }
    
    player.show();
    
    showScore();
    
    if (player.isAlive) {
        const bullet = player.update(obstacles, zombies);
        if (bullet) bullets.push(bullet);
        shootZombies();
        
        player.showHealth()     
        player.showLaser()
    }
    else {
        showGameOverScreen();
    }
}

/*
function mouseClicked() {
    if (!player.isAlive) return;
    
    const bullet = player.shoot()
    bullets.push(bullet);
}
*/

function createNewZombie(everyFrame) {
    if (frameCount % everyFrame !== 0) return;
    
    const zombie = Zombie.generateNewZombieAtRandomPosition(zombieImage, obstacles);
    zombies.push(zombie);
}


function showZombies() {
    for (const zombie of zombies) {
        zombie.update(obstacles, player);
        zombie.show();
    }
}

function showGameOverScreen() {
    fill("#C70039")
    textSize(40)
    textAlign(CENTER);
    
    text("GAME OVER", width/2 , height/2)
}

function showScore() {
    document.getElementById("score").innerText = `Score ${score}`;
}

function showBullets() {
    for (let i = 0; i < bullets.length; ++i) {
        const bullet = bullets[i];

        if (bullet.offTheScreen() || bullet.collided(obstacles)) {
            bullets.splice(i, 1)
        }

        bullet.update();
        bullet.show();
    }
}

function shootZombies() {
    for (let i = 0; i < bullets.length; ++i) {
        const bullet = bullets[i];
        
        for (let j = 0; j < zombies.length; ++j) {

            const zombie = zombies[j];
            if (zombie.hitBy(bullet)) {
                bullets.splice(i, 1)
                zombies.splice(j, 1)
                
                score += 10;

            }
        }
    }
}