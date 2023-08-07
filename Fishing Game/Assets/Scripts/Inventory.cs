using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Timeline.Actions;

public class Inventory : MonoBehaviour
{
    [Header("Mats")]
    public int metalCount;
    public int scrapMetalCount;
    public int rawMetalCount;
    public int woodCount;
    public int scrapWoodCount;
    public int nailsCount;
    public int paperCount;
    public int plasticCount;
    public int ropeCount;
    public int clayCount;
    public int brickCount;
    public int clothCount;

    [Header("MaxMats")]
    public int maxMetalCount;
    public int maxScrapMetalCount;
    public int maxRawMetalCount;
    public int maxWoodCount;
    public int maxScrapWoodCount;
    public int maxNailsCount;
    public int maxPaperCount;
    public int maxPlasticCount;
    public int maxRopeCount;
    public int maxClayCount;
    public int maxBrickCount;
    public int maxClothCount;

    [Header("Player & Cameras")]
    public GameObject PlayerCam;
    public GameObject InvCam;
    public GameObject Inv;
    public GameObject player;
    [SerializeField]
    private PlayerCam camScrpit;

    [Header("Items")]
    public GameObject harpoonItem;
    public GameObject hamerItem;
    public GameObject metalItem;
    public GameObject scrapMetalItem;
    public GameObject rawMetalItem;
    public GameObject woodItem;
    public GameObject scrapWoodItem;
    public GameObject nailsItem;
    public GameObject paperItem;
    public GameObject plasticItem;
    public GameObject ropeItem;
    public GameObject clayItem;
    public GameObject brickItem;
    public GameObject clothItem;

    [Header("Mat Text")]
    public GameObject harpoonText;
    public GameObject hamerText;
    public GameObject metalText;
    public GameObject scrapMetalText;
    public GameObject rawMetalText;
    public GameObject woodText;
    public GameObject scrapWoodText;
    public GameObject nailsText;
    public GameObject paperText;
    public GameObject plasticText;
    public GameObject ropeText;
    public GameObject clayText;
    public GameObject brickText;
    public GameObject clothText;

    [Header("HoldableItems")]
    public GameObject harpoonItemHoldable;
    public GameObject harpoonGUI;
    public GameObject hamerItemHoldable;
    public GameObject hamerGUI;
    public GameObject woodItemHoldable;

    private PlayerMovement Move;
    BuildMechanic buildMechanic;
    public bool InvOpen = false;
    public bool WoodHolding = false;
    public bool HarpoonHolding = false;
    public bool HamerHolding = false;

    void Start()
    {
        InvOpen = false;
        WoodHolding = false;
    }

    void Update()
    {
        Escape();
        TextUpdate();
        if (Input.GetKeyDown(KeyCode.I))
        {
            InvOpen = true;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InvOpen = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && HarpoonHolding)
        {
            harpoonItemHoldable.SetActive(false);
            harpoonGUI.SetActive(false);
            HarpoonHolding = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && HamerHolding && buildMechanic.BuildMenu == false)
        {
            hamerItemHoldable.SetActive(false);
            hamerGUI.SetActive(false);
            HamerHolding = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && WoodHolding)
        {
            woodItemHoldable.SetActive(false);
            WoodHolding = false;
        }

        if (HarpoonHolding == false)
        {
            harpoonItemHoldable.SetActive(false);
            harpoonGUI.SetActive(false);
        }
        if (HamerHolding == false)
        {
            hamerItemHoldable.SetActive(false);
            hamerGUI.SetActive(false);
        }
        if (WoodHolding == false)
        {
            woodItemHoldable.SetActive(false); 
        }

        if (InvOpen == true)
        {
            //Move.enabled = false;
            HarpoonHolding = false;
            HamerHolding = false;
            WoodHolding = false;

            camScrpit.enabled = false;
            Inv.SetActive(true);
            InvCam.SetActive(true);
            PlayerCam.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
        }
        if(woodCount == 0)
        {
            WoodHolding = false;
        }
    }

    void TextUpdate()
    {
        if (InvOpen == true)
        {
            if (metalCount >= 1)
            {
                metalText.SetActive(true);
                metalItem.SetActive(true);
                metalText.GetComponent<TextMeshProUGUI>().text = "(Metal: " + metalCount + "/" + maxMetalCount + ")";
            }
            else
            {
                metalText.SetActive(false);
                metalItem.SetActive(false);
            }

            if (scrapMetalCount >= 1)
            {
                scrapMetalText.SetActive(true);
                scrapMetalItem.SetActive(true);
                scrapMetalText.GetComponent<TextMeshProUGUI>().text = "(ScrapMetal: " + scrapMetalCount + "/" + maxScrapMetalCount + ")";
            }
            else
            {
                scrapMetalText.SetActive(false);
                scrapMetalItem.SetActive(false);
            }

            if (rawMetalCount >= 1)
            {
                rawMetalText.SetActive(true);
                rawMetalItem.SetActive(true);
                rawMetalText.GetComponent<TextMeshProUGUI>().text = "(RawMetal: " + rawMetalCount + "/" + maxRawMetalCount + ")";
            }
            else
            {
                rawMetalText.SetActive(false);
                rawMetalItem.SetActive(false);
            }

            if (woodCount >= 1)
            {
                woodText.SetActive(true);
                woodItem.SetActive(true);
                woodText.GetComponent<TextMeshProUGUI>().text = "(Wood: " + woodCount + "/" + maxWoodCount + ")";
            }
            else
            {
                woodText.SetActive(false);
                woodItem.SetActive(false);
            }

            if (scrapWoodCount >= 1)
            {
                scrapWoodText.SetActive(true);
                scrapWoodItem.SetActive(true);
                scrapWoodText.GetComponent<TextMeshProUGUI>().text = "(ScrapWood: " + scrapWoodCount + "/" + maxScrapWoodCount + ")";
            }
            else
            {
                scrapWoodText.SetActive(false);
                scrapWoodItem.SetActive(false);
            }

            if (nailsCount >= 1)
            {
                nailsText.SetActive(true);
                nailsItem.SetActive(true);
                nailsText.GetComponent<TextMeshProUGUI>().text = "(Nails: " + nailsCount + "/" + maxNailsCount + ")";
            }
            else
            {
                nailsText.SetActive(false);
                nailsItem.SetActive(false);
            }

            if (paperCount >= 1)
            {
                paperText.SetActive(true);
                paperItem.SetActive(true);
                paperText.GetComponent<TextMeshProUGUI>().text = "(Paper: " + paperCount + "/" + maxPaperCount + ")";
            }
            else
            {
                paperText.SetActive(false);
                paperItem.SetActive(false);
            }

            if (plasticCount >= 1)
            {
                plasticText.SetActive(true);
                plasticItem.SetActive(true);
                plasticText.GetComponent<TextMeshProUGUI>().text = "(Plastic: " + plasticCount + "/" + maxPlasticCount + ")";
            }
            else
            {
                plasticText.SetActive(false);
                plasticItem.SetActive(false);
            }

            if (ropeCount >= 1)
            {
                ropeText.SetActive(true);
                ropeItem.SetActive(true);
                ropeText.GetComponent<TextMeshProUGUI>().text = "(Rope: " + ropeCount + "/" + maxRopeCount + ")";
            }
            else
            {
                ropeText.SetActive(false);
                ropeItem.SetActive(false);
            }

            if (clayCount >= 1)
            {
                clayText.SetActive(true);
                clayItem.SetActive(true);
                clayText.GetComponent<TextMeshProUGUI>().text = "(Clay: " + clayCount + "/" + maxClayCount + ")";
            }
            else
            {
                clayText.SetActive(false);
                clayItem.SetActive(false);
            }

            if (brickCount >= 1)
            {
                brickText.SetActive(true);
                brickItem.SetActive(true);
                brickText.GetComponent<TextMeshProUGUI>().text = "(Bricks: " + brickCount + "/" + maxBrickCount + ")";
            }
            else
            {
                brickText.SetActive(false);
                brickItem.SetActive(false);
            }
            if (clothCount >= 1)
            {
                clothText.SetActive(true);
                clothItem.SetActive(true);
                clothText.GetComponent<TextMeshProUGUI>().text = "(Bricks: " + clothCount + "/" + maxClothCount + ")";
            }
            else
            {
                clothText.SetActive(false);
                clothItem.SetActive(false);
            }
        }
        else return;

       
    }

    public void HoldHarpoon()
    {
        ForceEscape();
        Debug.Log("HoldingHarpoon");
        harpoonItemHoldable.SetActive(true);
        harpoonGUI.SetActive(true);
        HarpoonHolding = true;
    }
    public void HoldHamer()
    {
        ForceEscape();
        Debug.Log("HoldingHamer");
        hamerItemHoldable.SetActive(true);
        hamerGUI.SetActive(false);
        HamerHolding = true;
    }
    public void HoldWood()
    {
        ForceEscape();
        Debug.Log("HoldingWood");
        woodItemHoldable.SetActive(true);
        WoodHolding = true;
    }
    void Escape()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && InvOpen == true)
        {
            ForceEscape();
            WoodHolding = false;
        }
    }

    void ForceEscape()
    {
        //Move.enabled = true;
        camScrpit.enabled = true;
        Inv.SetActive(false);
        InvCam.SetActive(false);
        PlayerCam.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        InvOpen = false;
    }


}
