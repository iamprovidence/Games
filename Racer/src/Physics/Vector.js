class Vector
{
    // PROPERTIES
    X;
    Y;
    
    get length() 
    {
        const { X, Y } = this;
        
        return sqrt(X * X + Y * Y);
    }
    
    get angle()
    {
        const { X, Y } = this;
        
        return atan2(Y, X);
    }

    // CONSTRUCTORS
    constructor(x, y)
    {
        this.X = x;
        this.Y = y;
    }
    
    
    // OPERATOR
    static MoveTowards(currentVector, targetVector, maxDistanceDelta)
    {
        const direction = Vector.Substract(targetVector, currentVector);
        const magnitude = direction.length;
        
        if (magnitude <= maxDistanceDelta || magnitude <= Number.EPSILON)
        {
            return targetVector;
        }
        return Vector.Add(currentVector, Vector.Multiply(direction, maxDistanceDelta / magnitude));
    }

    static Normalized(vector)
    {
        const length = vector.length;
        return new Vector(vector.X / length, vector.Y / length);
    }

    
    static Add(v1, v2)
    {
        return new Vector(v1.X + v2.X, v1.Y + v2.Y);
    }

    static Substract(v1, v2)
    {
        return new Vector(v1.X - v2.X, v1.Y - v2.Y);
    }

    static Multiply(v1, k)
    {
        return new Vector(v1.X * k, v1.Y * k);
    }
    
    static Scale(v1, k1, k2)
    {
        return new Vector(v1.X + k1, v1.Y + k2);
    }

    /// <summary>
    /// Projection on direction
    /// </summary>
    static Projection(vector, normalizedDirection)
    {
        const cosAngle = vector.X * normalizedDirection.X + vector.Y * normalizedDirection.Y;
        return new Vector(normalizedDirection.X * cosAngle, normalizedDirection.Y * cosAngle);
    }
    
    static Rotate(v, angle)
    {
            const cosAngle = cos(angle);
            const sinAngle = sin(angle);
        return new Vector(cosAngle * v.X - sinAngle * v.Y, sinAngle * v.X + cosAngle * v.Y);
    }
}