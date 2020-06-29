class WheelsTrace
{    
    leftWheel;
    rightWheel;
    size;
    
    constructor(leftWheel, rightWheel)
    {
        this.leftWheel = leftWheel;
        this.rightWheel = rightWheel;
        this.size = 1.5;
    }
    
    show(scale)
    {
        
        
        const size = this.size;
    stroke("#000")
    circle(this.leftWheel.X*scale, this.leftWheel.Y*scale, size, size);
        
    circle(this.rightWheel.X*scale, this.rightWheel.Y*scale, size, size);
    }
}