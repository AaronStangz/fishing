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
    Inventory clothCount;

    Inventory maxMetalCount;
    Inventory maxScrapMetalCount;
    Inventory maxRawMetalCount;
    Inventory maxWoodCount;
    Inventory maxScrapWoodCount;
    Inventory maxNailsCount;
    Inventory maxPaperCount;
    Inventory maxPlasticCount;
    Inventory maxRopeCount;
    Inventory maxClayCount;
    Inventory maxBrickCount;
    Inventory maxClothCount;

    public GameObject metalToNailsText;
    public GameObject scrapWoodToWoodText;
    public GameObject clayToBrickText;

    public GameObject smelterText;

    public GameObject tableText;
    public GameObject coffeeTableText;
    public GameObject carpetText;
    public GameObject chairText;
    public GameObject shelfText;
    public GameObject smallShelfText;

    public GameObject toolTab;
    public GameObject stations;
    public GameObject itemTab;
    public GameObject decorationTab;

    public GameObject smelterPrefab;

    public GameObject tablePrefab;
    public GameObject coffeeTablePrefab; // the guy who made the buidling Styem
    public GameObject carpetPrefab;
    public GameObject chairPrefab;
    public GameObject shelfPrefab;
    public GameObject smallShelfPrefab;

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
        clothCount = player.GetComponent<Inventory>();

        maxMetalCount = player.GetComponent<Inventory>();
        maxScrapMetalCount = player.GetComponent<Inventory>();
        maxRawMetalCount = player.GetComponent<Inventory>();
        maxWoodCount = player.GetComponent<Inventory>();
        maxScrapWoodCount = player.GetComponent<Inventory>();
        maxNailsCount = player.GetComponent<Inventory>();
        maxPaperCount = player.GetComponent<Inventory>();
        maxPlasticCount = player.GetComponent<Inventory>();
        maxRopeCount = player.GetComponent<Inventory>();
        maxClayCount = player.GetComponent<Inventory>();
        maxBrickCount = player.GetComponent<Inventory>();
        maxClothCount = player.GetComponent<Inventory>();
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
        decorationTab.SetActive(false);
    }
    ////////////////////////////////////////////////////////////////////////////////////////////// StationTab ///////////////////////////////////////////////////////////////////////////////////////////
    public void StationTab()
    {
        toolTab.SetActive(false);
        itemTab.SetActive(false);
        stations.SetActive(true);
        decorationTab.SetActive(false);
    }
    public void Smelter()
    {
        if (brickCount.brickCount >= 8)
        {
            brickCount.brickCount -= 8;
            Instantiate(smelterPrefab);
            ForceEscape();
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////// ItemTab ///////////////////////////////////////////////////////////////////////////////////////////
    public void ItemTab()
    {
        itemTab.SetActive(true);
        toolTab.SetActive(false);
        stations.SetActive(false);
        decorationTab.SetActive(false);
    }

    public void MetalToNails()
    {
        if (metalCount.metalCount >= 1 && nailsCount.nailsCount < maxNailsCount.maxNailsCount)
        {
            metalCount.metalCount -= 1;
            nailsCount.nailsCount += 2;
        }
    }
    public void ScrapWoodToWood()
    {
        if (scrapWoodCount.scrapWoodCount >= 4 && woodCount.woodCount < maxWoodCount.maxWoodCount)
        {
            scrapWoodCount.scrapWoodCount -= 4;
            woodCount.woodCount += 1;
        }
    }
    public void ClayToBrick()
    {
        if (clayCount.clayCount >= 4 && brickCount.brickCount < maxBrickCount.maxBrickCount)
        {
            clayCount.clayCount -= 4;
            brickCount.brickCount += 1;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////// ItemTab ///////////////////////////////////////////////////////////////////////////////////////////
    public void DecorationTab()
    {
        itemTab.SetActive(false);
        toolTab.SetActive(false);
        stations.SetActive(false);
        decorationTab.SetActive(true);
    }
    public void Table()
    {
        if (woodCount.woodCount >= 8 && nailsCount.nailsCount >= 4)
        {
            woodCount.woodCount -= 8;
            nailsCount.nailsCount -= 4;
            Instantiate(tablePrefab);
            ForceEscape();
        }
    }
    public void CoffeeTable()
    {
        if (woodCount.woodCount >= 4 && nailsCount.nailsCount >= 4)
        {
            woodCount.woodCount -= 4;
            nailsCount.nailsCount -= 4;
            Instantiate(coffeeTablePrefab);
            ForceEscape();
        }
    }
    public void Carpet()
    {
        if (clothCount.clothCount >= 4)
        {
            clothCount.clothCount -= 4;
            Instantiate(carpetPrefab);
            ForceEscape();
        }
    }
    public void Chair()
    {
        if (woodCount.woodCount >= 6 && nailsCount.nailsCount >= 6)
        {
            woodCount.woodCount -= 6;
            nailsCount.nailsCount -= 6;
            Instantiate(chairPrefab);
            ForceEscape();
        }
    }
    public void Shelf()
    {
        if (woodCount.woodCount >= 5 && nailsCount.nailsCount >= 6)
        {
            woodCount.woodCount -= 5;
            nailsCount.nailsCount -= 6;
            Instantiate(shelfPrefab);
            ForceEscape();
        }
    }
    public void SmallShelf()
    {
        if (woodCount.woodCount >= 4 && nailsCount.nailsCount >= 4)
        {
            woodCount.woodCount -= 4;
            nailsCount.nailsCount -= 4;
            Instantiate(smallShelfPrefab);
            ForceEscape();
        }
    }
    public void TextUpdate()
    {
        if (CraftGuiOpen == true)
        {
            metalToNailsText.GetComponent<TextMeshProUGUI>().text = "( Metal: " + metalCount.metalCount + " / 1 )";
            scrapWoodToWoodText.GetComponent<TextMeshProUGUI>().text = "( ScrapWood: " + scrapWoodCount.scrapWoodCount + " / 4 )";
            smelterText.GetComponent<TextMeshProUGUI>().text = "( Scrapbrick: " + brickCount.brickCount + " / 8 )";
            clayToBrickText.GetComponent<TextMeshProUGUI>().text = "( Clay: " + clayCount.clayCount + " / 4 )";

            tableText.GetComponent<TextMeshProUGUI>().text = "( Wood: " + woodCount.woodCount + " / 8 )" + "( Nail: " + nailsCount.nailsCount + " / 4 )";
            coffeeTableText.GetComponent<TextMeshProUGUI>().text = "( Wood: " + woodCount.woodCount + " / 4 )" + "( Nail: " + nailsCount.nailsCount + " / 4 )";
            carpetText.GetComponent<TextMeshProUGUI>().text = "( Cloth: " + clothCount.clothCount + " / 4 )";
            chairText.GetComponent<TextMeshProUGUI>().text = "( Wood: " + woodCount.woodCount + " / 6 )" + "( Nail: " + nailsCount.nailsCount + " / 6 )";
            shelfText.GetComponent<TextMeshProUGUI>().text = "( Wood: " + woodCount.woodCount + " / 5 )" + "( Nail: " + nailsCount.nailsCount + " / 6 )";
            smallShelfText.GetComponent<TextMeshProUGUI>().text = "( Wood: " + woodCount.woodCount + " / 4 )" + "( Nail: " + nailsCount.nailsCount + " / 4 )";
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
