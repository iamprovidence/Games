class Bullet {
    // FIELDS
    position;
    angle;
    speed;
    size;
    
    // CONSTRUCTORS
    constructor(position, angle) {        
        this.position = position;
        this.angle = angle;
        
        this.speed = 12;
        this.size = 7;
    }
    
    // PROPERTIES
    get collisionZone() {
        return { 
            x: this.position.x,
            y: this.position.y,
            r: this.size / 2
        }
    }
    
    // METHODS
    show() {
        fill('yellow');
        noStroke();
        
        circle(this.position.x, this.position.y, this.size, this.size);
    }
    
    update() {
        this.move();
    }
    
    move() {
        const { x, y } = this.position;
        
        const nextX = x + cos(this.angle) * this.speed;
        const nextY = y + sin(this.angle) * this.speed;
        
        this.position = new Point(nextX, nextY);
    }
    
    offTheScreen() {
        const { x, y } = this.position;
        
        return (
            x < 0 ||
            y < 0 ||
            x > width ||
            y > width
        )
    }
    
    collided(obstacles) {
        for (const obstacle of obstacles) {
            if (this.collide(obstacle)) {
                return true;
            }
        }
        return false;
    }
    
    collide(obstacle) {
        return Geometry
            .polygonCircleCollision(
                obstacle.vertices, 
                this.position.x, this.position.y, this.size / 2)
    }
}
