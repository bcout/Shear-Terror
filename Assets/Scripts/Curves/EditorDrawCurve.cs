using UnityEngine;

/*
* This script draws a bezier curve within the Unity editor, to make it easier to place
* control points. 
* 
* It places dots along the curve to help visualize how the speed of the object will vary as it follows the curve.
* If a bunch of dots are close together, the object will move slowly through that section. If they are spread out, 
* the object will move fast.
*/
public class EditorDrawCurve : MonoBehaviour
{
    // A lower draw step will draw more dots, higher will draw less
    private const float MIN_DRAW_STEP = 0.01f, MAX_DRAW_STEP = 0.25f;
    public float dots_y_value = 0.0f;
    public float dots_draw_step = 0.05f;
    public float dots_radius = 0.25f;

    private CurveData curve_data;
    private Vector3 gizmos_position;

    // This only gets called when a public variable gets changed in the editor, as opposed to running all the time
    private void OnValidate()
    {
        // Keeps the step value within reasonable bounds, so it's easier to change the value in the inspector
        // We also don't want it to be 0, because that will freeze the editor(!)
        dots_draw_step = Mathf.Clamp(dots_draw_step, MIN_DRAW_STEP, MAX_DRAW_STEP);
    }

    private void OnDrawGizmos()
    {
        curve_data = transform.GetComponent<CurveData>();
        transform.position = new Vector3(transform.position.x, dots_y_value, transform.position.z);
        for (float t = 0; t <= 1; t += dots_draw_step)
        {
            gizmos_position = curve_data.GetNextPoint(t);

            // We use spheres to visually draw the bezier curve. This also makes it easy to see where the curve is bunched up, as
            // that will look like a cluster of dots
            Gizmos.DrawSphere(gizmos_position, dots_radius);
        }

        // Draw some handle lines to make finding the control points easier
        Gizmos.DrawLine(curve_data.control_points[0].position, curve_data.control_points[1].position);
        Gizmos.DrawLine(curve_data.control_points[2].position, curve_data.control_points[3].position);
    }
}
