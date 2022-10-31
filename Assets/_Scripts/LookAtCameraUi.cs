using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCameraUi : MonoBehaviour
{
    public Camera mainCamera;
    
    void Update()
    {
        transform.LookAt(mainCamera.transform);
    }
}
