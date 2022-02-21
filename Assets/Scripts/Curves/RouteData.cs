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

        // If the first curve is not a 4 Point Curve, the curves aren't in the right order, so abandon the linking
        if (curves[0].name != "4 Point Curve")
        {
            Debug.LogError("First curve in hierarchy must be a 4 point curve!\nAbandoning linking.");
            return;
        }


    }
}
