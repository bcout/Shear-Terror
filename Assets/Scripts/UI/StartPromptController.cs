using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPromptController : MonoBehaviour
{
    [SerializeField] private GameObject prompt;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            prompt.SetActive(false);
        }
    }
}
