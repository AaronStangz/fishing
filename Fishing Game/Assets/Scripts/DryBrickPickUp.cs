using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class DryBrickPickUp : MonoBehaviour
{
    Inventory maxBrickCount;
    Inventory brickCount;
    public LayerMask PickUp;
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        brickCount = player.GetComponent<Inventory>();
        maxBrickCount = player.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main == null) return;

        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 5.0f, PickUp))
        {
            Debug.Log("PickUp");
            if (Input.GetKeyDown(KeyCode.E) && brickCount.brickCount < maxBrickCount.maxBrickCount)
            {
                Debug.Log("PickUp");
                brickCount.brickCount += 1;
                Destroy(hit.transform.gameObject, 0.1f);
            }
        }
    }
}
