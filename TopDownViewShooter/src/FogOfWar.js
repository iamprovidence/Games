class FogOfWar {
    // FIELDS
    sightZoneColor;
    fuzzyRadius;
    amountOfViewPoints;

    // CONSTRUCTORS
    constructor(sightZoneColor, fuzzyRadius = 10, amountOfViewPoints = 10) {
        this.sightZoneColor = sightZoneColor;
        this.fuzzyRadius = fuzzyRadius;
        this.amountOfViewPoints = amountOfViewPoints;
    }

    // METHODS
    showOnlyObservedObjects(doShowObserved = true) {
        const drawingOperation = doShowObserved ? "source-atop" : "source-over";
        drawingContext.globalCompositeOperation = drawingOperation;
    }
    
    show(obstacles, position) {
        const observedZones = this.computeObservedZones(obstacles, position);
        this.drawSightZones(observedZones)
    }
    
    computeObservedZones(obstacles, position) {
        const { x: posX, y: posY } = position;
        
        // sight zones from different view points 
        // to create fuzzy effect
        const observedZones = [];
        for (let angle = 0; angle < Math.PI * 2; angle += (PI * 2) / this.amountOfViewPoints) {
            const dx = cos(angle) * this.fuzzyRadius;
            const dy = sin(angle) * this.fuzzyRadius;

            const observationZone = Geometry.getIntersecations(obstacles, posX + dx, posY + dy);
            
            observedZones.push(observationZone);
        };

        // main sight zone
        const centralObservationZone = Geometry.getIntersecations(obstacles, posX, posY);
        
        return [centralObservationZone, ...observedZones];
    }
    
    drawSightZones(observedZones) {
        // draw fuzzy effect
        this.sightZoneColor.setAlpha(50)
        for (let i = 1; i < observedZones.length; ++i) {
            this.drawSightZone(observedZones[i], this.sightZoneColor);
        }
        
        // draw main
        this.sightZoneColor.setAlpha(255)
        this.drawSightZone(observedZones[0], this.sightZoneColor);
    }
    
    drawSightZone(observedZone, fillStyle) {
        fill(fillStyle);
        noStroke();

        beginShape();
        for (const { x: vertexX, y: vertexY } of observedZone) {
            vertex(vertexX, vertexY);
        }
        endShape(CLOSE);
    }
}