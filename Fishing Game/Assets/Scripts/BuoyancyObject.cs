using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyancyObject : MonoBehaviour
{
    public float underWaterDrag = 3f;
    public float underWaterAngularDrag = 1f;
    public float airDrag = 0f;
    public float airAngularDrag = 0.05f;
    public float floatingPower = 15f;
    public float waterHeight = 1f;

    public bool underwater;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float difference = transform.position.y - waterHeight;

        if(difference < 0)
        {
            rb.AddForceAtPosition (Vector3.up * floatingPower * Mathf.Abs(difference), transform.position, ForceMode.Force);
            if(!underwater)
            {
                underwater = true;
                SwitchState(true);
            }
        }
        else if(underwater)
        {
            underwater = false;
            SwitchState(false);
        }
    }

    void SwitchState(bool isunderwater)
    {
        if(isunderwater)
        {
            rb.drag = underWaterDrag;
            rb.angularDrag = underWaterAngularDrag;
        }
        else
        {
            rb.drag = airDrag;
            rb.angularDrag = airAngularDrag;
        }
    }
}
