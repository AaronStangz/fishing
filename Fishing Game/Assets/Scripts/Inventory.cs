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

    [Header("Player & Cameras")]
    public GameObject PlayerCam;
    public GameObject InvCam;
    public GameObject Inv;
    public GameObject player;
    [SerializeField]
    private PlayerCam camScrpit;

    [Header("Items")]
    public GameObject harpoonItem;
    public GameObject metalItem;
    public GameObject scrapMetalItem;
    public GameObject rawMetalItem;
    public GameObject woodItem;
    public GameObject scrapWoodItem;
    public GameObject nailsItem;
    public GameObject paperItem;
    public GameObject plasticItem;
    public GameObject ropeItem;

    [Header("Mat Text")]
    public GameObject harpoonText;
    public GameObject metalText;
    public GameObject scrapMetalText;
    public GameObject rawMetalText;
    public GameObject woodText;
    public GameObject scrapWoodText;
    public GameObject nailsText;
    public GameObject paperText;
    public GameObject plasticText;
    public GameObject ropeText;

    [Header("HoldableItems")]
    public GameObject harpoonItemHoldable;
    public GameObject harpoonGUI;

    private PlayerMovement Move;
    public bool InvOpen = false;

    void Start()
    {
        InvOpen = false;
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

        if (InvOpen == true)
        {
            //Move.enabled = false;
            harpoonItemHoldable.SetActive(false);
            harpoonGUI.SetActive(false);

            camScrpit.enabled = false;
            Inv.SetActive(true);
            InvCam.SetActive(true);
            PlayerCam.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void TextUpdate()
    {
        if(metalCount >= 1)
        {
            metalText.SetActive(true);
            metalItem.SetActive(true);
            metalText.GetComponent<TextMeshProUGUI>().text = "(Metal: " + metalCount + ")";
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
            scrapMetalText.GetComponent<TextMeshProUGUI>().text = "(ScrapMetal: " + scrapMetalCount + ")";
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
            rawMetalText.GetComponent<TextMeshProUGUI>().text = "(RawMetal: " + rawMetalCount + ")";
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
            woodText.GetComponent<TextMeshProUGUI>().text = "(Wood: " + woodCount + ")";
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
            scrapWoodText.GetComponent<TextMeshProUGUI>().text = "(ScrapWood: " + scrapWoodCount + ")";
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
            nailsText.GetComponent<TextMeshProUGUI>().text = "(Nails: " + nailsCount + ")";
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
            paperText.GetComponent<TextMeshProUGUI>().text = "(Paper: " + paperCount + ")";
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
            plasticText.GetComponent<TextMeshProUGUI>().text = "(Plastic: " + plasticCount + " ";
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
            ropeText.GetComponent<TextMeshProUGUI>().text = "(Rope: " + ropeCount + ")";
        }
        else
        {
            ropeText.SetActive(false);
            ropeItem.SetActive(false);
        }
    }

    public void HoldHarpoon()
    {
        ForceEscape();
        Debug.Log("HoldingHarpoon");
        harpoonItemHoldable.SetActive(true);
        harpoonGUI.SetActive(true);
    }
    void Escape()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && InvOpen == true)
        {
            ForceEscape();
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
