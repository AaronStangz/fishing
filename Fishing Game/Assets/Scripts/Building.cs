using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    Inventory MetalCount;
    Inventory ScrapMetalCount;
    Inventory RawMetalCount;
    Inventory WoodCount;
    Inventory ScrapWoodCount;
    Inventory NailsCount;
    Inventory PaperCount;
    Inventory PlasticCount;

    public GameObject platForm;
    public GameObject wall;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Instantiate(platForm);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Instantiate(wall);
        }

    }
}
