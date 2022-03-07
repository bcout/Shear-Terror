using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RouteData))]
public class EditorRouteData : Editor
{
    public override void OnInspectorGUI()
    {
        // Keep the original height control
        base.OnInspectorGUI();

        // target is the object currently being inspected (RouteData), but it is of type Object, so we must cast it.
        RouteData rd = (RouteData)target;

        // Add a button
        if (GUILayout.Button("Link Curves"))
        {
            // That function returns true when the button is clicked.
            rd.LinkCurves();
        }
    }
}
