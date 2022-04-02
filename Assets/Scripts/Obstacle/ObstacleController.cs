using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public GameObject sheep;

    public float x_max;
    public float y_max;
    public float z_max;
    public float speed;

    private float t;
    private Vector3 destination;
    private bool moving;
    private SheepController sheep_controller;

    // Start is called before the first frame update
    void Start()
    {
        moving = false;

        destination = new Vector3(x_max, y_max, z_max);

        sheep_controller = sheep.GetComponent<SheepController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving)
        {
            StartCoroutine(MoveBackAndForth(new Vector3(0, 0, 0), destination));
        }
    }

    private IEnumerator MoveBackAndForth(Vector3 a, Vector3 b)
    {
        yield return MoveTo(a, b);
        yield return MoveTo(b, a);
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
        destination = -destination;
        moving = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.name == "Sheep")
        {
            sheep_controller.SetState(sheep_controller.GetRagdollState());
        }
    }
}
