using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector2 currentRotation;
    float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            transform.position = transform.position + Camera.main.transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        }
        if (Input.GetButton("Vertical"))
        {
            transform.position = transform.position + Camera.main.transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed;
        }

        Vector2 mouseRotation = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        currentRotation += mouseRotation;

        Camera.main.transform.localRotation = Quaternion.AngleAxis(-currentRotation.y, Vector3.right);
        transform.localRotation = Quaternion.AngleAxis(currentRotation.x, Vector3.up);
    }
}
