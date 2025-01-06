using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float lookSpeed = 400.0f;
    public Transform body;
    private float rotationX = 0;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime;
        
        // Accumulate vertical rotation and clamp it
        rotationX -= mouseY; // Subtract because we want to invert the vertical look
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        
        // Apply rotations
        // Set absolute rotation for vertical look (camera)
        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        
        // Apply relative rotation for horizontal look (body)
        body.transform.Rotate(Vector3.up * mouseX);

    }
}
