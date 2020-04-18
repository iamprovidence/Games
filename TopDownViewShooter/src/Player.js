class Player {
    // FIELDS
    position;
    size;
    moveSpeed;
    health;
    image;

    // CONSTRUCTORS
    constructor(playerImage) {
        this.position = new Point(width/2, height/2);
        this.size = 40;
        this.moveSpeed = 2;
        this.health = 100;
        this.image = playerImage;
    }
    
    // PROPERTIES
    get position() {
        return this.position;
    }
    
    get isAlive() {
        return this.health > 0;
    }

    // METHODS
    update(obstacles, zombies) {
        const nextPosition = this.move();
        const isMoving = this.isMoving(nextPosition);
        const hasCollision = this.hasCollision(obstacles, nextPosition);

        if (!hasCollision) {
            this.position = nextPosition;
        }
        
        // shoot if not moving
        if (!isMoving && frameCount % 60 === 0) return this.shoot();
        
        const isBitten = this.isBittenByZombie(zombies, this.position);
        if (isBitten) this.health -= 0.1;
        
    }
    
    isMoving(nextPosition) {
        return nextPosition !== this.position;
    }

    move() {
        if (isKeyPressed) {
            if      (key.toLowerCase() === 'w' || keyCode == UP_ARROW)      return this.nextPosition(0, -this.moveSpeed);
            else if (key.toLowerCase() === 's' || keyCode == DOWN_ARROW)    return this.nextPosition(0, +this.moveSpeed);
            else if (key.toLowerCase() === 'a' || keyCode == LEFT_ARROW)    return this.nextPosition(-this.moveSpeed, 0);
            else if (key.toLowerCase() === 'd' || keyCode == RIGHT_ARROW)   return this.nextPosition(+this.moveSpeed, 0);
        }

        return this.position;
    }

    nextPosition(xOffset, yOffset) {
        const { x, y } = this.position;

        return new Point(x + xOffset, y + yOffset);
    }
    
    isBittenByZombie(zombies, position) {
        for (let zombie of zombies) {
            if (this.bitten(zombie, position)) {
                return true;
            }
        }
        return false;
    }
    
    bitten(zombie, position) {
        return Geometry
            .circleCircleCollision(
                position.x, position.y, this.size / 2,
                zombie.position.x, zombie.position.y, zombie.size / 2)
    }
    
    hasCollision(obstacles, position) {
        for (let obstacle of obstacles) {
            if (this.collided(obstacle, position)) {
                return true;
            }
        }
        return false;
    }

    collided(obstacle, position) {
        return Geometry
            .polygonCircleCollision(
                obstacle.vertices, 
                position.x, position.y, this.size / 2)
    }
    
    shoot() {
        const angle = Geometry.getAngleBetweenPoints(mouseX, mouseY, this.position.x, this.position.y);
        const spread = random(-0.1, 0.1);

        return new Bullet(this.position.clone(), angle + spread);
    }


    show() {
        const angle = Geometry.getAngleBetweenPoints(mouseX, mouseY, this.position.x, this.position.y);
        
        push()
        translate(this.position.x, this.position.y);
        rotate(angle)
        
        imageMode(CENTER);
        image(this.image, 0, 0, this.size, this.size);
        
        pop()    
    }
    
    showLaser() {

        const angle = Geometry.getAngleBetweenPoints(mouseX, mouseY, this.position.x, this.position.y); 

        const lineDistance = 1000;
        const xGunOffset = 20;
        const yGunOffset = 10;
        
        push()
        translate(this.position.x, this.position.y);
        rotate(angle)
        
        stroke("red");
        Geometry.drawLineByAngle(xGunOffset, yGunOffset, 0, lineDistance);

        pop()
    }
    
    
    showHealth(){
        noStroke();
        
        const maxHealthBarWidths =  width - 40;
        fill('#C70039');
        rect(20, 10, maxHealthBarWidths, 10);
        
        const healthBarWidth = lerp(0, maxHealthBarWidths, this.health / 100)
        fill('#28B873');
        rect(20, 10, healthBarWidth, 10);
    }

}
