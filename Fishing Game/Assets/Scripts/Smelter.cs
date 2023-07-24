using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Timeline.Actions;

public class Smelter : MonoBehaviour
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

    Inventory InvOpen;
    GameObject player;

    public Slider Slider;
    float timer;
    float targetTimer;

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

    
    public void Update()
    {
        TextUpdate();

        if (smelterGuiOpen == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Escape();
        }

        if (Camera.main == null) return;

        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 5.0f, SmelterLayer) && smelterGuiOpen == false)
        {
            Debug.Log("Smelter");
            if (Input.GetKeyDown(KeyCode.E))
            {
                //camScrpit.enabled = false;
                smelterGui.SetActive(true);
                Debug.Log("OpeningSmelter");
                smelterGuiOpen = true;
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
        if (rawMetalCount.rawMetalCount >= 1 && metalCount.metalCount < maxMetalCount.maxMetalCount && smelterInUse == false)
        {
            rawMetalCount.rawMetalCount -= 1;
            StartCoroutine(RawMetalToMetalIE());
            targetTimer = metalSmeltingTime;
            smelterInUse = true;
            timer = 0;
        }
    }

    IEnumerator RawMetalToMetalIE()
    {
        yield return new WaitForSeconds(metalSmeltingTime);
        metalCount.metalCount += 1;
        smelterInUse = false;
    }
    public void ScrapMetalToMetal()
    {
        if (scrapMetalCount.scrapMetalCount >= 4 && metalCount.metalCount < maxMetalCount.maxMetalCount && smelterInUse == false)
        {
            scrapMetalCount.scrapMetalCount -= 4;
            StartCoroutine(ScrapMetalToMetalIE());
            targetTimer = metalSmeltingTime;
            smelterInUse = true;
            timer = 0;
        }
    }
    IEnumerator ScrapMetalToMetalIE()
    {
        yield return new WaitForSeconds(metalSmeltingTime);
        metalCount.metalCount += 2;
        smelterInUse = false;
    }

    void TextUpdate()
    {
        if (smelterGuiOpen == true)
        {
            rawMetaltoMetalText.GetComponent<TextMeshProUGUI>().text = "( RawMetal: " + rawMetalCount.rawMetalCount + " / 1 )";
            scrapMetaltoMetalText.GetComponent<TextMeshProUGUI>().text = "( ScrapMetal: " + scrapMetalCount.scrapMetalCount + " / 4 )";
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
