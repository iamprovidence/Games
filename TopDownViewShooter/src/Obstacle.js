class Obstacle {
    // FIELDS
    color;
    vertices;

    // CONSTRUCTORS
    constructor(vertices, color) {
        this.vertices = vertices;
        this.color = color;
    }

    // METHODS
    show() {
        noStroke();
        if (this.color) fill(this.color);
        else            noFill();

        beginShape();
        for (const { x: vx, y: vy } of this.vertices) {
            vertex(vx, vy);
        }
        endShape(CLOSE);
    }

    // PROPERTIES
    get segments(){
        const segments = []
        for (let current = 0; current < this.vertices.length; ++current) {
            const next = (current + 1) % this.vertices.length;

            const vc = this.vertices[current];
            const vn = this.vertices[next];

            segments.push({ start: vc, end: vn })
        }

        return segments
    }
    
    get vertices() {
        return this.vertices
    }
}
