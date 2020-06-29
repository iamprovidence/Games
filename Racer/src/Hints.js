class Hints
{    
    flag;
    car;
    
    constructor(flag, car)
    {
        this.flag = flag;
        this.car = car;
    }
    
    show()
    {
        const {X:cx,Y: cy} = this.car.position;
        const {X:fx,Y: fy} = this.flag.position;

        stroke(226, 204, 0);
        
        if (cx > fx) line(10, 10, 10, height);
        if (cx < fx) line(width - 10, 10, width- 10, height);
        
        if (cy > fy) line(10, height -10, width, height -10);
        if (cy < fy) line(10, 10, width,10);
        
    }
        
    
}