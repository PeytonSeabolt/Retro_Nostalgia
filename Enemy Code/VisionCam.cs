using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCam : MonoBehaviour
{
    Transform cam;
    // Use this for initialization
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("Cam").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam);
    }
}
