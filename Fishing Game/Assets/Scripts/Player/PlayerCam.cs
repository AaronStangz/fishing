using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float initialMouseSensitivity;

    public bool IsGuiOpen;

    public Transform playerBody;

    float xRotation = 0f;
   // float yRotation = 0f;

    void Start()
    {
        initialMouseSensitivity = mouseSensitivity;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
       // if (IsGuiOpen == true)
       // {
            float mouseX = Input.GetAxis("Mouse X") * initialMouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * initialMouseSensitivity * Time.deltaTime;
       // }
//else
       // {
       //     float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //    float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;   
       // }

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
