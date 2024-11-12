using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundZoomEffect : MonoBehaviour
{
    [SerializeField] float zoomDuration = 15f;
    [SerializeField] float scaleFactor = 0.9f;

    void Start()
    {
        ScaleBackground();
    }

    void ScaleBackground()
    {
        LeanTween.scale(gameObject, transform.localScale * scaleFactor, zoomDuration).setEaseOutSine().setLoopPingPong();
    }
}
