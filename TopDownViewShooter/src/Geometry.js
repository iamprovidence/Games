class Geometry {
    
    static drawLineByAngle(x, y, angle, length) {
        const px = x + cos(angle) * length
        const py = y - sin(angle) * length
        line(x, y, px, py);
    }

    static getAngleBetweenPoints(x1, y1, x2, y2)
    {
        const distX = x1 - x2;
        const distY = y1 - y2;
        const angle = atan2(distY, distX);
        
        return angle;
    }
    
    // INTERSECTIONS WITH OBSTACLES
    static getIntersecations(obstacles, positionX, positionY) {
        const vertices = obstacles.reduce((acc, curr) => acc.concat(curr.vertices), []);

        const angles = [];
        for (const vertex of vertices) {
            
            const angle = atan2(vertex.y - positionY, vertex.x - positionX);

            angles.push(angle - 0.0001, angle, angle + 0.0001);
        }


        const intersectionsByAngle = [];
        for (const angle of angles) {
            // Calculate dx & dy from angle
            const dx = cos(angle);
            const dy = sin(angle);

            // Ray from center of screen to mouse
            const ray = {
                a: {
                    x: positionX,
                    y: positionY
                },
                b: {
                    x: positionX + dx,
                    y: positionY + dy
                },
            };

            // Find CLOSEST intersection
            const closestIntersection = Geometry.findClosestIntersection(ray, obstacles)
            intersectionsByAngle.push({
                intersection: closestIntersection,
                angle
            });
        }

        return intersectionsByAngle
            .sort((a, b) => a.angle - b.angle)
            .map(ia => ia.intersection)
            .filter(i => i);
    }

    static findClosestIntersection(ray, obstacles) {
        let closestIntersection = null;
        for (let i = 0; i < obstacles.length; ++i) {
            const segments = obstacles[i].segments

            for (let j = 0; j < segments.length; ++j) {
                const intersection = Geometry.getIntersection(ray, segments[j]);
                if (!intersection) continue;

                if (!closestIntersection || intersection.param < closestIntersection.param) {
                    closestIntersection = intersection;
                }
            }
        }
        return closestIntersection;
    }

    static getIntersection(ray, segment) {
        // RAY in parametric: Point + Direction*T1
        const r_px = ray.a.x;
        const r_py = ray.a.y;
        const r_dx = ray.b.x - ray.a.x;
        const r_dy = ray.b.y - ray.a.y;

        // SEGMENT in parametric: Point + Direction*T2
        const s_px = segment.start.x;
        const s_py = segment.start.y;
        const s_dx = segment.end.x - segment.start.x;
        const s_dy = segment.end.y - segment.start.y;

        // Are they parallel? If so, no intersect
        const r_mag = Math.sqrt(r_dx * r_dx + r_dy * r_dy);
        const s_mag = Math.sqrt(s_dx * s_dx + s_dy * s_dy);
        if (r_dx / r_mag == s_dx / s_mag && r_dy / r_mag == s_dy / s_mag) {
            // Directions are the same.
            return null;
        }

        // SOLVE FOR T1 & T2
        // r_px+r_dx*T1 = s_px+s_dx*T2 && r_py+r_dy*T1 = s_py+s_dy*T2
        // ==> T1 = (s_px+s_dx*T2-r_px)/r_dx = (s_py+s_dy*T2-r_py)/r_dy
        // ==> s_px*r_dy + s_dx*T2*r_dy - r_px*r_dy = s_py*r_dx + s_dy*T2*r_dx - r_py*r_dx
        // ==> T2 = (r_dx*(s_py-r_py) + r_dy*(r_px-s_px))/(s_dx*r_dy - s_dy*r_dx)
        const T2 = (r_dx * (s_py - r_py) + r_dy * (r_px - s_px)) / (s_dx * r_dy - s_dy * r_dx);
        const T1 = (s_px + s_dx * T2 - r_px) / r_dx;

        // Must be within parametic whatevers for RAY/SEGMENT
        if (T1 < 0)             return null;
        if (T2 < 0 || T2 > 1)   return null;

        // Return the POINT OF INTERSECTION
        return {
            x: r_px + r_dx * T1,
            y: r_py + r_dy * T1,
            param: T1,
        };
    }
    
    // -- COLLISION --

    // POLYGON/CIRCLE
    static polygonCircleCollision(vertices, cx, cy, r) {
        for (let current = 0; current < vertices.length; ++current) {
            const next = (current + 1) % vertices.length;

            const vc = vertices[current];
            const vn = vertices[next];

            // check for collision between the circle and
            // a line formed between the two vertices
            const collision = Geometry.lineCircleCollision(vc.x, vc.y, vn.x, vn.y, cx, cy, r);
            if (collision) return true;
        }

        return false;
    }


    // LINE/CIRCLE
    static lineCircleCollision(x1, y1, x2, y2, cx, cy, r) {
        // is either end INSIDE the circle?
        // if so, return true immediately
        const isInside1 = Geometry.linePointCollision(x1, y1, cx, cy, r);
        const isInside2 = Geometry.linePointCollision(x2, y2, cx, cy, r);
        if (isInside1 || isInside2) return true;

        // get length of the line
        let distX = x1 - x2;
        let distY = y1 - y2;

        const len = sqrt((distX * distX) + (distY * distY));

        // get dot product of the line and circle
        const dot = (((cx - x1) * (x2 - x1)) + ((cy - y1) * (y2 - y1))) / (len * len) // pow(len, 2);

        // find the closest point on the line
        const closestX = x1 + (dot * (x2 - x1));
        const closestY = y1 + (dot * (y2 - y1));

        // is this point actually on the line segment?
        // if so keep going, but if not, return false
        const onSegment = Geometry.linePointCollision(x1, y1, x2, y2, closestX, closestY);
        if (!onSegment) return false;

        // get distance to closest point
        distX = closestX - cx;
        distY = closestY - cy;
        const distance = sqrt((distX * distX) + (distY * distY));

        // is the circle on the line?
        return distance <= r
    }

    // LINE/POINT
    static linePointCollision(x1, y1, x2, y2, px, py) {
        // get distance from the point to the two ends of the line
        const d1 = dist(px, py, x1, y1);
        const d2 = dist(px, py, x2, y2);

        // get the length of the line
        const lineLen = dist(x1, y1, x2, y2);

        // since floats are so minutely accurate, add
        // a little buffer zone that will give collision
        const buffer = 0.1;

        // if the two distances are equal to the line's
        // length, the point is on the line
        return (
            d1 + d2 >= lineLen - buffer &&
            d1 + d2 <= lineLen + buffer)
    }
    
    // POINT/CIRCLE
    static pointCircleCollision(px, py, cx, cy, r) {
        // get distance between the point and circle's center
        // using the Pythagorean Theorem
        const distX = px - cx;
        const distY = py - cy;
        const distance = sqrt((distX * distX) + (distY * distY));

        // if the distance is less than the circle's 
        // radius the point is inside!
        return distance <= r
    }
    
    // CIRCLE/CIRCLE
    static circleCircleCollision(c1x, c1y, c1r, c2x, c2y, c2r) {

      // get distance between the circle's centers
      // use the Pythagorean Theorem to compute the distance
      const distX = c1x - c2x;
      const distY = c1y - c2y;
      const distance = sqrt( (distX*distX) + (distY*distY) );

      // if the distance is less than the sum of the circle's
      // radii, the circles are touching!
      return distance <= c1r + c2r; 
    }
}