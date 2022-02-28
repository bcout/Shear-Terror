using System.Collections;
using UnityEngine;

/**
 * This script makes an object move along a given route at a given speed.
 * This script should be attached to the object being moved.
 */
public class FollowBezierCurve : MonoBehaviour
{
    // A route is a game object with multiple bezier curves as children
    public GameObject route;
    public float speed = 0.5f;

    // An array of all the curves in the given route
    private Transform[] curves;
    private int next_curve;
    // Used in the GetNextPoint function to find points along the curve
    private float t;
    // The follower is the object following the curve
    private Vector3 follower_position;

    // We will do the actual movement along each curve in a coroutine, but we only
    // want to move along one curve at a time. So, we lock the coroutine behind
    // this boolean until the follower is at the end of their current curve.
    private bool coroutine_available;

    void Start()
    {
        next_curve = 0;
        t = 0f;
        coroutine_available = true;

        PrepareCurves();
    }

    void PrepareCurves()
    {
        // A route can have any number of curves in it, so we'll look at the route's children to find out how many
        int num_curves = route.transform.childCount;
        curves = new Transform[num_curves];
        for (int i = 0; i < num_curves; i++)
        {
            curves[i] = route.transform.GetChild(i);
        }
    }

    void Update()
    {
        if (coroutine_available)
        {
            StartCoroutine(FollowNextCurve(next_curve));
        }
    }

    private IEnumerator FollowNextCurve(int curve_number)
    {
        // We're in the process of following a curve, so we don't want to start following another one.
        // To prevent this, we stop the game from starting any new coroutines
        coroutine_available = false;

        // To calculate the next position along the curve, we need to know this curve's control points
        // Conveniently, we made control_points a public array in the EditorDrawCurve script.
        CurveData curve_data = curves[curve_number].GetComponent<CurveData>();

        while (t < 1)
        {
            t += Time.deltaTime * speed;

            follower_position = curve_data.GetNextPoint(t);

            transform.LookAt(follower_position);
            transform.position = follower_position;

            yield return new WaitForEndOfFrame();
        }

        t = 0f;
        next_curve++;
        if (next_curve > curves.Length - 1)
        {
            next_curve = 0;
        }

        coroutine_available = true;
    }
}
