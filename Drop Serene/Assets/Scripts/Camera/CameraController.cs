using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float xMouseRotationSpeed = 90;
    public float yMouseRotationSpeed = 90;
    public float yMaxPanLimit = 90;
    public float yMinPanLimit = -90;

    public float x;
    public float y;
    public GameObject player;

    // Use this for initialization
    void Start ()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        player = GameObject.Find("Player");
        Cursor.lockState = CursorLockMode.Locked;
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        GetComponent<Camera>().orthographic = false;
        GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;

        x += Input.GetAxis("Mouse X") * xMouseRotationSpeed * 0.02f;
        y -= Input.GetAxis("Mouse Y") * yMouseRotationSpeed * 0.02f;

        y = ClampAngle(y, yMinPanLimit, yMaxPanLimit);

        Quaternion rotation = Quaternion.Euler(y, x, 0);

        transform.rotation = rotation;

        if (Input.GetKeyDown(KeyCode.F1))
        {
            Cursor.visible = !Cursor.visible;
            if (Cursor.visible)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }


    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
