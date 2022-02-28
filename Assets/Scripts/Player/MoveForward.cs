using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 0.5f;
    private void Update()
    {
        transform.Translate(Time.deltaTime * Vector3.forward * speed);
    }
}
