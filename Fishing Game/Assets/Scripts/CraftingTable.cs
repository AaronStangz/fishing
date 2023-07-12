using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    Inventory metalCount;
    Inventory scrapMetalCount;
    Inventory rawMetalCount;
    Inventory woodCount;
    Inventory scrapWoodCount;
    Inventory nailsCount;
    Inventory paperCount;
    Inventory plasticCount;
    Inventory ropeCount;

    GameObject player;

    Inventory InvOpen;

    [SerializeField]
    private PlayerCam camScrpit;

    public GameObject craftGui;
    [SerializeField] private LayerMask CraftingBeach;

    public bool CraftGuiOpen = false;

    void Start()
    {
        CraftGuiOpen = false;
        player = GameObject.Find("Player");
        metalCount = player.GetComponent<Inventory>();
        scrapMetalCount = player.GetComponent<Inventory>();
        rawMetalCount = player.GetComponent<Inventory>();
        woodCount = player.GetComponent<Inventory>();
        scrapWoodCount = player.GetComponent<Inventory>();
        nailsCount = player.GetComponent<Inventory>();
        paperCount = player.GetComponent<Inventory>();
        plasticCount = player.GetComponent<Inventory>();
        ropeCount = player.GetComponent<Inventory>();
        camScrpit = player.GetComponent<PlayerCam>();
    }

    void Update()
    {
        if(CraftGuiOpen == true)
        {
            craftGui.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Escape();
        }

        if (Camera.main == null) return;

        if (InvOpen == false)
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 5.0f, CraftingBeach) && CraftGuiOpen == false)
            {
                Debug.Log("CraftingBeach");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //camScrpit.enabled = false;
                    Debug.Log("OpeningCraftingBeach");
                    CraftGuiOpen = true;
                }
            }
        }
    }

    public void MetalToNails()
    {
        if (metalCount.metalCount >= 1)
        {
            metalCount.metalCount -= 1;
            nailsCount.nailsCount += 2;
        }
    }
    public void ScrapMetalToMetal()
    {
        if (scrapMetalCount.scrapMetalCount >= 4)
        {
            scrapMetalCount.scrapMetalCount -= 4;
            metalCount.metalCount += 1;
        }
    }
    public void ScrapWoodToWood()
    {
        if (scrapWoodCount.scrapWoodCount >= 4)
        {
            scrapWoodCount.scrapWoodCount -= 4;
            woodCount.woodCount += 1;
        }
    }

    void Escape()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && CraftGuiOpen == true)
        {
            ForceEscape();
        }
    }

    void ForceEscape()
    {
        //Move.enabled = true;
        //camScrpit.enabled = true;
        craftGui.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        CraftGuiOpen = false;
    }
}
