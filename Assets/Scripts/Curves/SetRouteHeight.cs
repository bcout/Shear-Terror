using UnityEngine;

/*
 * This script allows us to move the entire route along the y axis given a height value.
 * We need this script because the only way to translate a bezier curve along the y axis is to update each 
 * control point, something that you can't do by just updating the parent object's y value.
 * 
 * This script should be attached to a route. Routes can only have bezier curves as children.
 */
public class SetRouteHeight : MonoBehaviour
{
    public float height = 0.0f;

    // OnValidate() only gets called when a public variable is changed in the editor, which is better than having this run
    // all the time without ever changing the value.
    private void OnValidate()
    {
        foreach (Transform child in transform)
        {
            foreach (Transform grandchild in child.transform)
            {
                grandchild.transform.position = new Vector3(grandchild.transform.position.x, height, grandchild.transform.position.z);
            }
        }
    }
}
