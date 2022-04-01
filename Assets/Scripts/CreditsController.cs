using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditsController : MonoBehaviour
{
    [SerializeField] private float speed = 25f;
    private const float MAX_Y_VALUE = 1372; // 17 * number of lines

    private void Start()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, 0f, transform.localPosition.z);
    }

    private void Update()
    {
        if (transform.localPosition.y < MAX_Y_VALUE)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + Time.deltaTime * speed, transform.localPosition.z);
        }
        
    }
}
