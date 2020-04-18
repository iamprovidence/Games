class Point {
    // FIELDS
    x;
    y;

    // CONSTRUCTORS
    constructor(x, y) {
        this.x = x;
        this.y = y;
    }

    // PROPERTIES
    get x() {
        return this.x;
    }
    get y() {
        return this.y;
    }
    
    // METHODS
    clone(){
        return new Point(this.x, this.y);
    }
}
