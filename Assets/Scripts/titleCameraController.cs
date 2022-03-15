using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleCameraController : MonoBehaviour
{
    float xRotCounter = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xRotCounter += 2.0f * Time.deltaTime;
        transform.localEulerAngles = new Vector3(0, xRotCounter, 0);
    }
}
