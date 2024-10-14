using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStartButton : MonoBehaviour
{
    public float moveDuration = 2f;

    void Start()
    {
        MoveAlongPoints();
    }

    void MoveAlongPoints()
    {
        LeanTween.moveY(gameObject, transform.position.y - 20, moveDuration).setEaseInOutSine().setLoopPingPong();
    }
}
