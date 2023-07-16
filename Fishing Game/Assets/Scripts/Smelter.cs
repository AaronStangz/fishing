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

    Inventory InvOpen;
    GameObject player;

    public Slider Slider;
    float timer;
    float targetTimer;

    bool smelterInUse = false;

    public float metalSmeltingTime;

    public GameObject rawMetaltoMetalText;

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
        if (rawMetalCount.rawMetalCount >= 1 && smelterInUse == false)
        {
            rawMetalCount.rawMetalCount -= 1;
            StartCoroutine(CraftingMetal());
            targetTimer = metalSmeltingTime;
            smelterInUse = true;
            timer = 0;
        }
    }

    IEnumerator CraftingMetal()
    {
        yield return new WaitForSeconds(metalSmeltingTime);
        metalCount.metalCount += 1;
        smelterInUse = false;
    }

    void TextUpdate()
    {
        if (smelterGuiOpen == true)
        {
            rawMetaltoMetalText.GetComponent<TextMeshProUGUI>().text = "( RawMetal: " + rawMetalCount.rawMetalCount + " / 1 )";
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
