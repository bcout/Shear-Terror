using UnityEngine;
using UnityEditor;

/**
 * This class contains data for a single bezier curve. It holds the 4 control points
 * and contains the function used to find points along the curve.
 */
public class CurveData : MonoBehaviour
{

    /**
     * An array of the four control points that make up a bezier curve.
     */
    public Transform[] control_points;

    /**
     * The four control points that make up the bezier curve.
     */
    [SerializeField]
    private Transform point_0, point_1, point_2, point_3;
    

    private void Start()
    {
        control_points = new Transform[] { point_0, point_1, point_2, point_3 };
    }


    /**
     * We calculate each position along the bezier curve using the explicit form cubic formula:
     * B(t) = (1-t)^3(P0) + 3(1-t)^2(t)(P1) + 3(1-t)(t^2)(P2) + (t^3)(P3), 0 <= t <= 1
     * 
     * t determines how far along the curve this calculation should be.
     * Returns a vector3 representing the position of the next point along the curve.
     * If t is an invalid value, returns Vector3.zero
     */
    public Vector3 GetNextCurvePoint(float t)
    {
        if (t < 0 || t > 1)
        {
            return Vector3.zero;
        }

        
        return Mathf.Pow(1 - t, 3) * control_points[0].position 
               + 3 * Mathf.Pow(1 - t, 2) * t * control_points[1].position 
               + 3 * (1 - t) * Mathf.Pow(t, 2) * control_points[2].position 
               + Mathf.Pow(t, 3) * control_points[3].position;
        
    }
}
