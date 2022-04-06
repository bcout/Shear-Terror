using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float x_min;
    public float y_min;
    public float z_min;
    public float x_max;
    public float y_max;
    public float z_max;
    public float speed;

    private float t;
    private Vector3 min;
    private Vector3 max;
    private bool moving;
    private GameObject sheep;
    private SheepController sheep_controller;
    private RagdollController ragdoll_controller;

    // Start is called before the first frame update
    void Start()
    {
        moving = false;

        min = new Vector3(x_min, y_min, z_min);
        max = new Vector3(x_max, y_max, z_max);

        sheep = GameObject.Find("Sheep");
        sheep_controller = sheep.GetComponent<SheepController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving)
        {
            StartCoroutine(MoveBackAndForth(new Vector3(0, 0, 0), min, max));
        }
    }

    private IEnumerator MoveBackAndForth(Vector3 origin, Vector3 min, Vector3 max)
    {
        if (origin != max)
        {
            yield return MoveTo(origin, max);
            yield return MoveTo(max, origin);
        }
        if (origin != min)
        {
            yield return MoveTo(origin, min);
            yield return MoveTo(min, origin);
        }
    }

    private IEnumerator MoveTo(Vector3 a, Vector3 b)
    {
        moving = true;
        
        while(t < 1)
        {
            t += Time.deltaTime * speed;

            transform.localPosition = Vector3.Lerp(a, b, t);

            yield return new WaitForEndOfFrame();
        }

        t = 0f;
        moving = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Sheep")
        {
            sheep_controller.SetState(sheep_controller.GetRagdollState());
            sheep_controller.SetCollidedObstacle(gameObject);
        }
    }
}
