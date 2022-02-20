# Curves

These scripts contain all the logic behind bezier curves in this game.

## CurveData

Every bezier curve has a CurveData script attached to it. This class contains:
- An array containing the curve's 4 controls points
- A `GetNextPoint()` function to get the position of a specific point along the curve


