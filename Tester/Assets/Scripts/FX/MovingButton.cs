using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingButton : MonoBehaviour
{
    [SerializeField] float moveDuration = 2f;

    void Start()
    {
        MoveAlongPoints();
    }

    void MoveAlongPoints()
    {
        LeanTween.moveY(gameObject, transform.position.y - 20, moveDuration).setEaseInOutSine().setLoopPingPong();
        LeanTween.moveX(gameObject, transform.position.x - 20, moveDuration + 2).setEaseShake().setLoopPingPong();
    }
}
