using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildHitboxCheck : MonoBehaviour
{
    public bool CollidingWithBuilt = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Built"))
        {
            print("CollidingWithBuilt = true");
            CollidingWithBuilt = true;
        } else
        {
            print("CollidingWith something");
        }
    }
}
