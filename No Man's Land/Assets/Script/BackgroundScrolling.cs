using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public float scrollSpeed = 1f;
    public float resetPosition = -20f;

    private Transform[] backgroundLayers;
    private float backgroundWidth;
    private bool isScrollingForward = false;
    private bool isScrollingReverse = false;

    private void Start()
    {
        backgroundLayers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            backgroundLayers[i] = transform.GetChild(i);
        }

        backgroundWidth = 33f; // backgroundLayers[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        if (isScrollingForward)
        {
            foreach (var layer in backgroundLayers)
            {
                ScrollBackgroundForward(layer);
            }
        }
        else if (isScrollingReverse)
        {
            foreach (var layer in backgroundLayers)
            {
                ScrollBackgroundReverse(layer);
            }
        }
    }

    private void ScrollBackgroundForward(Transform layer)
    {
        layer.Translate(Vector2.down * scrollSpeed * Time.deltaTime);

        if (layer.position.x < resetPosition)
        {
            layer.position = new Vector2(layer.position.x + backgroundWidth,
                layer.position.y);
        }
    }

    public void StartScrollingForward()
    {
        isScrollingForward = true;
    }

    public void StopScrollingForward()
    {
        isScrollingForward = false;
    }

    public void StartScrollingReverse()
    {
        isScrollingReverse = true;
    }

    public void StopScrollingReverse()
    {
        isScrollingReverse = false;
    }

    private void ScrollBackgroundReverse(Transform layer)
    {
        layer.Translate(Vector2.up * scrollSpeed * Time.deltaTime);

        if (layer.position.x < resetPosition)
        {
            layer.position = new Vector2(layer.position.x + backgroundWidth, layer.position.y);
        }
    }

}
