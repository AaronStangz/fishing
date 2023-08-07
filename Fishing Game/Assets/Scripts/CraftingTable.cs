using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    [Header("MatText")]
    public GameObject metalToNailsText;
    public GameObject scrapWoodToWoodText;
    public GameObject clayToBrickText;

    [Header("MatPrefab")]
    public GameObject brickPrefab;

    [Header("MatPreview")]
    public GameObject metalToNailsPreview;
    public GameObject scrapWoodToWoodPreview;
    public GameObject clayToBrickPreview;

    [Header("WorkshopText")]
    public GameObject smelterText;

    [Header("WorkshopPreview")]
    public GameObject smelterPreview;

    [Header("DecText")]
    public GameObject tableWoodText;
    public GameObject tableNailText;
    public GameObject coffeeTableWoodText;
    public GameObject coffeeTableNailText;
    public GameObject carpetText;
    public GameObject chairWoodText;
    public GameObject chairNailText;
    public GameObject shelfWoodText;
    public GameObject shelfNailText;
    public GameObject smallShelfWoodText;
    public GameObject smallShelfNailText;

    [Header("DecPreview")]
    public GameObject tablePreview;
    public GameObject coffeeTablePreview;
    public GameObject carpetPreview;
    public GameObject chairPreview;
    public GameObject shelfPreview;
    public GameObject smallShelfPreview;

    [Header("Tab")]
    public GameObject toolTab;
    public GameObject stations;
    public GameObject itemTab;
    public GameObject decorationTab;

    [Header("WorkshopPrefab")]
    public GameObject smelterPrefab;

    [Header("DecPrefab")]
    public GameObject tablePrefab;
    public GameObject coffeeTablePrefab; // the guy who made the buidling Styem
    public GameObject carpetPrefab;
    public GameObject chairPrefab;
    public GameObject shelfPrefab;
    public GameObject smallShelfPrefab;

    GameObject player;

    Inventory inventory;

    [SerializeField]
    private PlayerCam camScrpit;

    public GameObject craftGui;
    [SerializeField] private LayerMask CraftingBeach;

    public bool CraftGuiOpen = false;

    void Start()
    {
        CraftGuiOpen = false;
        player = GameObject.Find("Player");
        inventory = player.GetComponent<Inventory>();
    }

    void Update()
    {
        TextUpdate();

        if (CraftGuiOpen == true)
        {
            Cursor.lockState = CursorLockMode.None;
            inventory.HarpoonHolding = false;
            inventory.HamerHolding = false;
            inventory.WoodHolding = false;
            Escape();
        }

        if (Camera.main == null) return;

        if (inventory.InvOpen == false)
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
    public void SmelterPreview()
    {
        ClosePreviews();
        smelterPreview.SetActive(true);
    }
    public void Smelter()
    {
        if (inventory.brickCount >= 8)
        {
            inventory.brickCount -= 8;
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
    public void MetalToNailsPreview()
    {
        ClosePreviews();
        metalToNailsPreview.SetActive(true);
    }
    public void MetalToNails()
    {
        if (inventory.metalCount >= 1 && inventory.nailsCount < inventory.maxNailsCount)
        {
            inventory.metalCount -= 1;
            inventory.nailsCount += 2;
        }
    }
    public void ScrapWoodToWoodPreview()
    {
        ClosePreviews();
        scrapWoodToWoodPreview.SetActive(true);
    }
    public void ScrapWoodToWood()
    {
        if (inventory.scrapWoodCount >= 4 && inventory.woodCount < inventory.maxWoodCount)
        {
            inventory.scrapWoodCount -= 4;
            inventory.woodCount += 1;
        }
    }
    public void ClayToBrickPreview()
    {
        ClosePreviews();
        clayToBrickPreview.SetActive(true);
    }
    public void ClayToBrick()
    {
        if (inventory.clayCount >= 4)
        {
            inventory.clayCount -= 4;
            Instantiate(brickPrefab);
            ForceEscape();
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
    public void TablePreview()
    {
        ClosePreviews();
        tablePreview.SetActive(true);
    }
    public void Table()
    {
        if (inventory.woodCount >= 8 && inventory.nailsCount >= 4)
        {
            inventory.woodCount -= 8;
            inventory.nailsCount -= 4;
            Instantiate(tablePrefab);
            ForceEscape();
        }
    }
    public void CoffeeTablePreview()
    {
        ClosePreviews();
        coffeeTablePreview.SetActive(true);
    }
    public void CoffeeTable()
    {
        if (inventory.woodCount >= 4 && inventory.nailsCount >= 4)
        {
            inventory.woodCount -= 4;
            inventory.nailsCount -= 4;
            Instantiate(coffeeTablePrefab);
            ForceEscape();
        }
    }
    public void CarpetPreview()
    {
        ClosePreviews();
        carpetPreview.SetActive(true);
    }
    public void Carpet()
    {
        if (inventory.clothCount >= 4)
        {
            inventory.clothCount -= 4;
            Instantiate(carpetPrefab);
            ForceEscape();
        }
    }
    public void ChairPreview()
    {
        ClosePreviews();
        chairPreview.SetActive(true);
    }
    public void Chair()
    {
        if (inventory.woodCount >= 6 && inventory.nailsCount >= 6)
        {
            inventory.woodCount -= 6;
            inventory.nailsCount -= 6;
            Instantiate(chairPrefab);
            ForceEscape();
        }
    }
    public void ShelfPreview()
    {
        ClosePreviews();
        shelfPreview.SetActive(true);
    }
    public void Shelf()
    {
        if (inventory.woodCount >= 5 && inventory.nailsCount >= 6)
        {
            inventory.woodCount -= 5;
            inventory.nailsCount -= 6;
            Instantiate(shelfPrefab);
            ForceEscape();
        }
    }
    public void SmallShelfPreview()
    {
        ClosePreviews();
        smallShelfPreview.SetActive(true);
    }
    public void SmallShelf()
    {
        if (inventory.woodCount >= 4 && inventory.nailsCount >= 4)
        {
            inventory.woodCount -= 4;
            inventory.nailsCount -= 4;
            Instantiate(smallShelfPrefab);
            ForceEscape();
        }
    }
    public void TextUpdate()
    {
        if (CraftGuiOpen == true)
        {
            metalToNailsText.GetComponent<TextMeshProUGUI>().text = inventory.metalCount + " / 1 ";
            scrapWoodToWoodText.GetComponent<TextMeshProUGUI>().text = inventory.scrapWoodCount + " / 4 ";
            smelterText.GetComponent<TextMeshProUGUI>().text = inventory.brickCount + " / 8 ";
            clayToBrickText.GetComponent<TextMeshProUGUI>().text = inventory.clayCount + " / 4 ";

            tableWoodText.GetComponent<TextMeshProUGUI>().text = inventory.woodCount + " / 8 ";
            tableNailText.GetComponent<TextMeshProUGUI>().text = inventory.nailsCount + " / 4 ";
            coffeeTableWoodText.GetComponent<TextMeshProUGUI>().text = inventory.woodCount + " / 4 ";
            coffeeTableNailText.GetComponent<TextMeshProUGUI>().text = inventory.nailsCount + " / 4 ";
            carpetText.GetComponent<TextMeshProUGUI>().text = inventory.clothCount + " / 4 ";
            chairWoodText.GetComponent<TextMeshProUGUI>().text = inventory.woodCount + " / 6 ";
            chairNailText.GetComponent<TextMeshProUGUI>().text = inventory.nailsCount + " / 6 ";
            shelfWoodText.GetComponent<TextMeshProUGUI>().text = inventory.woodCount + " / 5 ";
            shelfNailText.GetComponent<TextMeshProUGUI>().text = inventory.nailsCount + " / 6 ";
            smallShelfWoodText.GetComponent<TextMeshProUGUI>().text = inventory.woodCount + " / 4 ";
            smallShelfNailText.GetComponent<TextMeshProUGUI>().text = inventory.nailsCount + " / 4 ";

        }
        else return;
    }

    public void ClosePreviews()
    {
        metalToNailsPreview.SetActive(false);
        scrapWoodToWoodPreview.SetActive(false);
        clayToBrickPreview.SetActive(false);
        smelterPreview.SetActive(false);
        tablePreview.SetActive(false);
        smallShelfPreview.SetActive(false);
        shelfPreview.SetActive(false);
        carpetPreview.SetActive(false);
        coffeeTablePreview.SetActive(false);
        chairPreview.SetActive(false);
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
        ClosePreviews();
        craftGui.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        CraftGuiOpen = false;
    }
}
