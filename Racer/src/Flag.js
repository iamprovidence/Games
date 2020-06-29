class Flag
{    
    position;
    
    constructor()
    {
        this.position = new Vector(10, 10);
    }
    
    show()
    {
        fill("red")
        line(this.position.X, this.position.Y, this.position.X, this.position.Y - 20)
        triangle(this.position.X, this.position.Y- 20, this.position.X+10, this.position.Y-15, this.position.X, this.position.Y- 10);
    }
    
    update(car)
    {
        if (this.collide(car))
        {
            this.respawn();
        }
    }
    
    collide(car)
    {
        
        const {X:cX,Y: cY} = car.position;
        const {X:fX,Y: fY} = this.position;
        
        const distX = cX - fX;
        const distY = cY - fY;
        
        const distance = sqrt( (distX*distX) + (distY*distY) );
        
        console.log(distance)
        return distance <= 10;
    }
    
    respawn()
    {
        const xOffset = random(5);
        const yOffset = random(5);
        
        const {X,Y} = this.position;

        this.position = new Vector(X + xOffset, Y + yOffset)
    }
}