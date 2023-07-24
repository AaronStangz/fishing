using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
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

    [SerializeField] private LayerMask Collectable;
    [Header("How Much To Give")]
    public int Metal;
    public int ScrapMetal;
    public int RawMetal;
    public int Wood;
    public int ScrapWood;
    public int Nails;
    public int Paper;
    public int Plastic;
    public int Rope;
    public int Clay;
    public int Brick;
    public int Cloth;

    GameObject player;

    void Start()
    {
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

    public void Update()
    {
        if (Camera.main == null) return;

        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 5.0f, Collectable))
        {
            Debug.Log("Collectable");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Collected");
                CollectItem();
                Destroy(gameObject, 0.1f);
            }
        }
    }

    public void CollectItem()
    {
        if(metalCount.metalCount > maxMetalCount.maxMetalCount)
        {
            metalCount.metalCount += Metal;
        }
        if(scrapMetalCount.scrapMetalCount < maxScrapMetalCount.maxScrapMetalCount)
        {
            scrapMetalCount.scrapMetalCount += ScrapMetal;
        }
        if (rawMetalCount.rawMetalCount < maxRawMetalCount.rawMetalCount)
        {
            rawMetalCount.rawMetalCount += RawMetal;
        }
        if (woodCount.woodCount < maxWoodCount.maxWoodCount) 
        {
            woodCount.woodCount += Wood;
        }
        if (scrapMetalCount.scrapMetalCount < maxScrapMetalCount.maxScrapMetalCount)
        {
            scrapWoodCount.scrapWoodCount += ScrapWood;
        }
        if (nailsCount.nailsCount < maxNailsCount.maxNailsCount)
        {
            nailsCount.nailsCount += Nails;
        }
        if (paperCount.paperCount < maxPaperCount.maxPaperCount)
        {
            paperCount.paperCount += Paper;
        }
        if (plasticCount.plasticCount < maxPlasticCount.maxPlasticCount)
        {
            plasticCount.plasticCount += Plastic;
        }
        if (ropeCount.ropeCount < maxRopeCount.maxRopeCount)
        {
            ropeCount.ropeCount += Rope;
        }
        if (clayCount.clayCount < maxClayCount.maxClayCount)
        {
            clayCount.clayCount += Clay;
        }
        if (brickCount.brickCount < maxBrickCount.maxBrickCount)
        {
            brickCount.brickCount += Brick;
        }
        if (clothCount.clothCount < maxClothCount.maxClothCount)
        {
            clothCount.clothCount += Cloth;
        }
    }
}
