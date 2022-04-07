using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditsController : MonoBehaviour
{
    [SerializeField] GameObject prompt;
    [SerializeField] float end_y_value;
    [SerializeField] float start_y_value;
    [SerializeField] float speed;

    private void Start()
    {
        prompt.SetActive(false);
        transform.localPosition = new Vector3(transform.localPosition.x, start_y_value, transform.localPosition.z);
    }

    private void Update()
    {
        if (transform.localPosition.y < end_y_value)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + Time.deltaTime * speed, transform.localPosition.z);
        }
        else
        {
            StartCoroutine(DelayShowPrompt());
        }
    }

    private IEnumerator DelayShowPrompt()
    {
        yield return new WaitForSeconds(2);
        prompt.SetActive(true);
    }
}
