# Curve Prefabs

A route is made up of curves, and these are the 3 types of curves that make up a route.

### 4 point curve

A 4 point curve starts the route off. It is always the *first* curve in the route.
- All 4 control points are unique transforms

### 3 point curve

3 point curves make up the middle of the route and the end. It is always the *last* curve in the route.
- Its first control point is the same transform as the last control point in the curve before it.

### 2 point curve (Optional)

A 2 point curve is used to end a loop. It is always the *last* curve in the loop.
- Its first control point is the same transform as the last point in the curve before it.
- Its last control point is the same transform as the first point in the first curve in the route.

