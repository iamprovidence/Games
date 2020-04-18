class Zombie {
    // FIELDS
    movingAngle;
    position;
    size;
    speed;
    image;

    // CONSTRUCTORS
    constructor(image, position) {
        this.movingAngle = 0;
        this.position = position;
        this.size = random(25, 50);
        this.speed = random(0.5, 2);
        this.image = image;
    }

    static generateNewZombieAtRandomPosition(image, obstacles) {
        
        let position;
        let zombie;
        let hasCollision = true;

        do
        {
            position = new Point(random(0, width), random(0, height))
            zombie = new Zombie(image, position);

            hasCollision = zombie.hasCollision(obstacles, position);
        } while(hasCollision);

        return zombie;
    }
    
    // METHODS
    show() {
        push()
        translate(this.position.x, this.position.y);
        rotate(this.movingAngle)
        
        imageMode(CENTER);
        image(this.image, 0, 0, this.size, this.size);

        pop()
    }

    update(obstacles, player) {
        const nextPosition = this.move(player);
        const hasCollision = this.hasCollision(obstacles, nextPosition);
        if (!hasCollision) {
            this.position = nextPosition;
        }
    }
    
    move(player) {
        this.seekForPlayer(player);
        
        const { x, y } = this.position;
        
        const nextX = x + cos(this.movingAngle) * this.speed;
        const nextY = y + sin(this.movingAngle) * this.speed;
        
        return new Point(nextX, nextY);
    }

    seekForPlayer(player) {
        const { x: px, y: py } = player.position;
        const { x: zx, y: zy } = this.position;

        this.movingAngle = Geometry.getAngleBetweenPoints(px, py, zx, zy)
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
                position.x, position.y, this.size / 2);
    }
    
    hitBy(bullet) {
        const { collisionZone } = bullet;
        
        return Geometry
            .circleCircleCollision(
                this.position.x, this.position.y, this.size/2, 
                collisionZone.x, collisionZone.y, collisionZone.r);
    }
}
