using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    [SerializeField]
    private PlayerCam camScrpit;

    [SerializeField] private LayerMask CraftingBeach;

    float xRotation = 0f;
   // float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -50f, 50f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 5.0f, CraftingBeach))
        {
            Debug.Log("CraftingBeach");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("OpeningCraftingBeach");
                camScrpit.enabled = false;
            }
        }

    }
}
