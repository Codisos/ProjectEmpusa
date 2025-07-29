using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayerCam : MonoBehaviour
{
    public Transform cam;

    private void Awake()
    {
        if(cam == null) { cam = Camera.main.transform; }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam, Vector3.up);
    }
}
