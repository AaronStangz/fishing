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
    Inventory clayCount;
    Inventory brickCount;

    public GameObject metalToNailsText;
    public GameObject scrapMetalToMetalText;
    public GameObject scrapWoodToWoodText;
    public GameObject smelterText;
    public GameObject clayToBrickText;

    public GameObject toolTab;
    public GameObject stations;
    public GameObject itemTab;

    public GameObject SmelterPrefab;

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
        clayCount = player.GetComponent<Inventory>();
        brickCount = player.GetComponent<Inventory>();
        camScrpit = player.GetComponent<PlayerCam>();
    }

    void Update()
    {
        TextUpdate();

        if (CraftGuiOpen == true)
        {
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
                    craftGui.SetActive(true);
                    Debug.Log("OpeningCraftingBeach");
                    CraftGuiOpen = true;
                }
            }
        }
    }

    public void ToolsTab()
    {
        toolTab.SetActive(true);
        itemTab.SetActive(false);
        stations.SetActive(false);
    }
    ////////////////////////////////////////////////////////////////////////////////////////////// StationTab ///////////////////////////////////////////////////////////////////////////////////////////
    public void StationTab()
    {
        toolTab.SetActive(false);
        itemTab.SetActive(false);
        stations.SetActive(true);
    }
    public void Smelter()
    {
        if (brickCount.brickCount >= 8)
        {
            brickCount.brickCount -= 8;
            Instantiate(SmelterPrefab);
            ForceEscape();
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////// ItemTab ///////////////////////////////////////////////////////////////////////////////////////////
    public void ItemTab()
    {
        itemTab.SetActive(true);
        toolTab.SetActive(false);
        stations.SetActive(false);
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
    public void ClayToBrick()
    {
        if (clayCount.clayCount >= 4)
        {
            clayCount.clayCount -= 4;
            brickCount.brickCount += 1;
        }
    }
    public void TextUpdate()
    {
        if (CraftGuiOpen == true)
        {
            metalToNailsText.GetComponent<TextMeshProUGUI>().text = "( Metal: " + metalCount.metalCount + " / 1 )";
            scrapMetalToMetalText.GetComponent<TextMeshProUGUI>().text = "( ScrapMetal: " + scrapMetalCount.scrapMetalCount + " / 4 )";
            scrapWoodToWoodText.GetComponent<TextMeshProUGUI>().text = "( ScrapWood: " + scrapWoodCount.scrapWoodCount + " / 4 )";
            smelterText.GetComponent<TextMeshProUGUI>().text = "( Scrapbrick: " + brickCount.brickCount + " / 8 )";
            clayToBrickText.GetComponent<TextMeshProUGUI>().text = "( Clay: " + clayCount.clayCount + " / 4 )";
        }
        else return;
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
