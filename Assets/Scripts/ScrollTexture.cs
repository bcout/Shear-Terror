using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour
{
    [SerializeField] private float scroll_x = 0.5f;
    [SerializeField] private float scroll_y = 0.5f;

    private void Update()
    {
        float offset_x = Time.time * scroll_x;
        float offset_y = Time.time * scroll_y;

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offset_x, offset_y);
    }
}
