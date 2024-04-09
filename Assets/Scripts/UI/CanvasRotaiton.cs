using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRotaiton : MonoBehaviour
{
    Camera main;

    private void Awake()
    {
        main = Camera.main;
    }

    void FixedUpdate()
    {
        LookCamera();
    }

    void LookCamera()
    {
        transform.rotation = main.transform.rotation;
    }
}
