using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] BoxCollider2D cameraRef;

	void Start()
    {

        float screenRatio = (float) Screen.width / (float) Screen.height;
        float targetRatio = cameraRef.bounds.size.x / cameraRef.bounds.size.y;

        if(screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = cameraRef.bounds.size.y / 2;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = cameraRef.bounds.size.y / 2 * differenceInSize;
        }

        Destroy(GameObject.Find("CameraRef"));
	}
}
