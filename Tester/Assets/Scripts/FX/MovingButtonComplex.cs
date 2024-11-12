using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingButtonComplex : MonoBehaviour
{
    [SerializeField] float moveDuration = 2f;

    void Start()
    {
        MoveAlongPoints();
    }

    void MoveAlongPoints()
    {
        LeanTween.moveY(gameObject, transform.position.y - 50, moveDuration).setEaseInBounce().setLoopPingPong();
    }
}
