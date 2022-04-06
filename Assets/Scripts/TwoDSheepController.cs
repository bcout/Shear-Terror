using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDSheepController : MonoBehaviour
{
    private SpriteRenderer rend;
    public Animator anim;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<SpriteRenderer> ();
        anim = GetComponent<Animator>();
        anim.speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void placeDaSheep()
    {
        var dist = (transform.position - Camera.main.transform.position).z;
        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        Vector3 start = new Vector3(leftBorder, 0, 0);
        transform.position = transform.position + start;
        anim.speed = 2f;
    }

    public void MoveDaSheep(float howFar)
    {
        var dist = (transform.position - Camera.main.transform.position).z;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        Vector3 velocity = new Vector3(howFar*rightBorder*2, 0, 0);
        transform.position = transform.position + velocity;
    }
}