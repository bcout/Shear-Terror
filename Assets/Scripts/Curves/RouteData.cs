using UnityEngine;

/**
 * This script is attached to the route game object, and is used to perform all sorts of functions on the route.
 */
public class RouteData : MonoBehaviour
{
    private Transform[] curves;
    private int num_curves;

    public float height = 0.0f;

    // OnValidate() only gets called when a public variable is changed in the editor, which is better than having this run
    // all the time without ever changing the value.
    private void OnValidate()
    {
        // Set the height of the route based on the value given in the inspector
        foreach (Transform child in transform)
        {
            foreach (Transform grandchild in child.transform)
            {
                grandchild.transform.position = new Vector3(grandchild.transform.position.x, height, grandchild.transform.position.z);
            }
        }
    }

    /*
     * Called when the link curves button is pressed in the inspector
     * This function assigns all the undefined control points for all curves in a route
     * We assume that the curves are in order in the hierarchy
     * i.e. the top curve connects to the next, which connects to the next
     */
    public void LinkCurves()
    {
        num_curves = transform.childCount;
        curves = new Transform[num_curves];
        for(int i = 0; i < num_curves; i++)
        {
            curves[i] = transform.GetChild(i);
        }

        if (!ValidateCurves())
        {
            return;
        }

        CurveData curr_curve;
        CurveData prev_curve;
        for (int i = 1; i < num_curves; i++)
        {
            curr_curve = curves[i].GetComponent<CurveData>();
            prev_curve = curves[i - 1].GetComponent<CurveData>();
            switch (curves[i].name)
            {
                case "3 Point Curve":
                    // 3 point curves share their first control point with the previous curve's last point
                    curr_curve.control_points[0] = prev_curve.control_points[3];
                    break;
                case "2 Point Curve":
                    curr_curve.control_points[0] = prev_curve.control_points[3];
                    curr_curve.control_points[3] = curves[0].GetComponent<CurveData>().control_points[0];
                    break;
            }
        }

        Debug.Log("Done linking!");
    }

    private bool ValidateCurves()
    {
        bool valid = true;
        // If the first curve is not a 4 Point Curve, the curves aren't in the right order, so abandon the linking
        if (curves[0].name != "4 Point Curve")
        {
            Debug.LogError("First curve in hierarchy must be a 4 point curve. Abandoning linking.");
            valid = false;
        }

        
        for (int i = 1; i < num_curves; i++)
        {
            // If there is more than one 4 point curve, abandon the linking
            if (curves[i].name == "4 Point Curve")
            {
                Debug.LogError("Only one 4 point curve is allowed. Abandoning linking.");
                valid = false;
            }

            // A 2 point curve can only be at the end of the route
            if (curves[i].name == "2 Point Curve" && i != (num_curves - 1))
            {
                Debug.LogError("2 point curves must be at the end of the route. Abandoning linking.");
                valid = false;
            }

            // If the curve isn't named correctly, abandon the linking
            if (curves[i].name != "2 Point Curve" && curves[i].name != "3 Point Curve" && curves[i].name != "4 Point Curve")
            {
                Debug.LogError("Invalid curve name: " + curves[i].name + ". Abandoning linking.");
                valid = false;
            }
        }

        return valid;
    }
}
