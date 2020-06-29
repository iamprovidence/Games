
class Car
{
    image;
    
    mass; // kg
    wheelbase; // meters
    trackWidth; // meters
    
    adhesion; // коеф зчеплення коліс із поверхнею
    throttle; // вижимання газа, [-1, +1]
    enginePower; // ye
    steeringSensitivity; 
    airFriction;
    breaksFriction; 
    tireFriction; 

    isBraking;
    steeringAngle;
    velocity;
    lookAt;
    position; // положення центру автомобіля

    
    constructor(imageFile)
    {
        this.image = imageFile;
        
        this.mass = 1200; 
        this.wheelbase = 2.9;
        this.trackWidth = 1.4;
        
        this.adhesion = 1;
        this.throttle = 0;
        this.enginePower = 15000;
        this.steeringSensitivity = 5;
        this.airFriction = 0.05;
        this.breaksFriction = 1;
        this.tireFriction = 0.3;
        
        this.isBraking = false;
        this.steeringAngle = 0;
        this.velocity = new Vector(0, 0);
        this.lookAt = new Vector(1, 0)
        this.position = new Vector(10, 10);

        
        
    }
    

    // METHODS

    show(scaleFactor) 
    {
        
        
        translate(this.position.X*scaleFactor, this.position.Y*scaleFactor);
        rotate(this.lookAt.angle)
        
        imageMode(CENTER);
        image(this.image, 0, 0, this.wheelbase*scaleFactor, this.trackWidth*scaleFactor);
        
    }
    
    update({ throttle, steering, dt, isBraking, scaleFactor })
    {
        // max steering angle
        const maxAngle = radians(30);
        
        const k = this.steeringSensitivity * dt;
        let s = this.steeringAngle; s = s * (1 - k) + steering * k;
        
        this.steeringAngle = constrain(s, -maxAngle, maxAngle);
        this.throttle = constrain(throttle, -1, 1);
        
        this.isBraking = isBraking;

        // F = ma
        const force = this.throttle * this.enginePower;
        this.velocity = Vector.Add(this.velocity, Vector.Multiply(this.lookAt, force * dt / this.mass));

        // air friction
        this.velocity = Vector.Substract(this.velocity, Vector.Multiply(this.velocity, this.airFriction * dt));

        // tires friction
        let tiresFriction = this.tireFriction * abs(this.steeringAngle);
        
         // brakes
        if (isBraking) tiresFriction = this.breaksFriction;

        const projection = Vector.Projection(this.velocity, this.lookAt);
        this.velocity = Vector.Substract(this.velocity, Vector.Multiply(projection, tiresFriction * this.adhesion * dt));

        // wheels position
        const wheelDistance = Vector.Multiply(this.lookAt, this.wheelbase / 2);
        let frontWheel = Vector.Add(this.position, wheelDistance);
        let backWheel = Vector.Substract(this.position, wheelDistance);
        
        
        backWheel = this.calcWheelMoving({ wheelPosition: backWheel, wheelDirection: this.lookAt, velocity: this.velocity, dt: dt });
        frontWheel = this.calcWheelMoving({ wheelPosition: frontWheel, wheelDirection: Vector.Rotate(this.lookAt, this.steeringAngle), velocity: this.velocity, dt: dt });

        // new car orientation
        this.lookAt = Vector.Substract(frontWheel, backWheel);
        this.lookAt = Vector.Normalized(this.lookAt);


        // calc new velocity
        const speed = this.velocity.length;

        const prev = this.velocity;
        this.velocity = Vector.Multiply(this.lookAt, speed);
        this.velocity = Vector.MoveTowards(prev, this.velocity, 0.5 * this.adhesion);

        // assign new position
        this.position = Vector.Add(this.position, Vector.Multiply(this.velocity, dt));
        
        return [this.calcWheelTrace(scaleFactor),this.calcWheelTrace2(scaleFactor)];
    }
    
    calcWheelMoving({ wheelPosition, wheelDirection, velocity, dt })
    {
        const tangentСomponent = Vector.Projection(velocity, wheelDirection);
        
        // зміщення колеса
        const wheelOffset = Vector.Add(tangentСomponent, this.velocity);
        const moving = Vector.Multiply(wheelOffset, 0.5 * dt); // із врахування ковзання
        // const moving = Vector.Multiply(tangentСomponent, dt); // без врахування ковзання

        return Vector.Add(wheelPosition, moving);
    }
    
    calcWheelTrace(scaleFactor)
    {
        let n = Vector.Rotate(Vector.Multiply(this.lookAt, 1/3), radians(90)) // Vector.Rotate(this.lookAt,radians(90))
        const l = Vector.Multiply(Vector.Multiply(this.lookAt, 1), this.wheelbase/scaleFactor);
        
        let f = Vector.Add(this.position, l);
       // f = Vector.Multiply(f, 2)
        
        let s1 = Vector.Add(f, n);
        let s2 = Vector.Substract(f, n);

        
        //s1 = Vector.Scale(s1, 1, 0.5)
        //s2 =Vector.Scale(s2, 1, -0.5)

        //console.log(s1)
        //console.log(s2)        
        
        return new WheelsTrace( s1, s2)
    }
    
    calcWheelTrace2(scaleFactor)
    {
        const n = Vector.Rotate(Vector.Multiply(this.lookAt, 1/3),  radians(90))
        const l =Vector.Multiply(this.lookAt, this.wheelbase/scaleFactor);
        
        const s1 = Vector.Add(Vector.Substract(this.position, l), n);
        const s2 = Vector.Substract(Vector.Substract(this.position, l), n);
        
        return new WheelsTrace(s1, s2)
    }
    
}