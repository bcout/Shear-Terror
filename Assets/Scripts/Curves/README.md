# Curves

These scripts contain all the logic behind bezier curves in this game.

## CurveData

Every bezier curve has a CurveData script attached to it. This class contains:
- An array containing the curve's 4 controls points
```
curve_data = GetComponent<CurveData>();
curve_data.control_points[i];
```
- A `GetNextPoint()` function to get the position of a specific point along the curve

## EditorDrawCurve

When this script is attached to an object that has a CurveData script, it will draw dots along that bezier 
curve inside the Editor.
- The curve is not visible in-game

## FollowBezierCurve

When this script is attached to an object, it will move the object along a given route.
- A route is a series of bezier curves connected end-to-end
- Supports an unlimited number of bezier curves per route

## SetRouteHeight

When this script is attached to a route object, it will move the entire route along the y-axis to a given 
point.
- A route is an object with only bezier curves as children.
