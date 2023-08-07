using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Timeline.Actions;

public class Smelter : MonoBehaviour
{
    GameObject player;
    Inventory inventory;

    public Slider Slider;
    public Slider WoodLevelSlider;
    public GameObject woodGui;
    float timer;
    float targetTimer;
    public float maxWoodLevel = 4;
    float woodLevel;

    bool smelterInUse = false;

    public float metalSmeltingTime;

    public GameObject rawMetaltoMetalText;
    public GameObject scrapMetaltoMetalText;

    [SerializeField] private LayerMask SmelterLayer;

    public GameObject smelterGui;
    public bool smelterGuiOpen = false;

    void Start()
    {
        player = GameObject.Find("Player");
        smelterGuiOpen = false;
        inventory = player.GetComponent<Inventory>();
    }

    
    public void Update()
    {
        TextUpdate();

        if (smelterGuiOpen == true)
        {
            Cursor.lockState = CursorLockMode.None;
            WoodLevelSlider.value = woodLevel;
            inventory.HarpoonHolding = false;
            inventory.HamerHolding = false;
            inventory.WoodHolding = false;
            Escape();
        }
        if(woodLevel <= maxWoodLevel)
        {
            woodGui.SetActive(false);
        }
        if(inventory.WoodHolding == false) 
        {
            woodGui.SetActive(false);
        }

        if (Camera.main == null) return;

        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 5.0f, SmelterLayer) && smelterGuiOpen == false)
        {
            if (Input.GetKeyDown(KeyCode.E) && inventory.WoodHolding == false)
            {
                //camScrpit.enabled = false;
                smelterGui.SetActive(true);
                Debug.Log("OpeningSmelter");
                smelterGuiOpen = true;

            }
            if (Input.GetKeyDown(KeyCode.E) && inventory.WoodHolding == true)
            {
                
                if (inventory.scrapWoodCount >= 1 && woodLevel <= maxWoodLevel) 
                {
                    Debug.Log("AddWood");
                    inventory.scrapWoodCount -= 1;
                    woodLevel += 1;
                }
            }
            if(inventory.WoodHolding == true && woodLevel <= maxWoodLevel)
            {
                woodGui.SetActive(true);
            }
          
        }

        timer += Time.deltaTime;
        if (timer > 0)
        {
            Slider.value = timer / targetTimer;
        }
    }

    public void RawMetalToMetal()
    {
        if (inventory.rawMetalCount >= 1 && inventory.metalCount < inventory.maxMetalCount && smelterInUse == false && woodLevel >= 1)
        {
            inventory.rawMetalCount -= 1;
            woodLevel -= 1;
            StartCoroutine(RawMetalToMetalIE());
            targetTimer = metalSmeltingTime;
            smelterInUse = true;
            timer = 0;
        }
    }

    IEnumerator RawMetalToMetalIE()
    {
        yield return new WaitForSeconds(metalSmeltingTime);
        inventory.metalCount += 1;
        smelterInUse = false;
    }
    public void ScrapMetalToMetal()
    {
        if (inventory.scrapMetalCount >= 4 && inventory.metalCount < inventory.maxMetalCount && smelterInUse == false && woodLevel >= 1)
        {
            inventory.scrapMetalCount -= 4;
            woodLevel -= 1;
            StartCoroutine(ScrapMetalToMetalIE());
            targetTimer = metalSmeltingTime;
            smelterInUse = true;
            timer = 0;
        }
    }
    IEnumerator ScrapMetalToMetalIE()
    {
        yield return new WaitForSeconds(metalSmeltingTime);
        inventory.metalCount += 2;
        smelterInUse = false;
    }

    void TextUpdate()
    {
        if (smelterGuiOpen == true)
        {
            rawMetaltoMetalText.GetComponent<TextMeshProUGUI>().text = "( RawMetal: " + inventory.rawMetalCount + " / 1 )";
            scrapMetaltoMetalText.GetComponent<TextMeshProUGUI>().text = "( ScrapMetal: " + inventory.scrapMetalCount + " / 4 )";
        }
        else return;
    }


    void Escape()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && smelterGuiOpen == true)
        {
            ForceEscape();
        }
    }

    void ForceEscape()
    {
        //Move.enabled = true;
        //camScrpit.enabled = true;
        smelterGui.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        smelterGuiOpen = false;
    }
}
