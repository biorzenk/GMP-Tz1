using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Camera mainCamera;

    void Update()
    {
        if (mainCamera != null)
        {
            transform.position = mainCamera.transform.position + new Vector3(0, 2, -5);
        }
    }
}
