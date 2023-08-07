using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
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
    Inventory inventory;

    public float pickUpRage = 5f;

    void Start()
    {
        player = GameObject.Find("Player");
        inventory = player.GetComponent<Inventory>();
    }

    public void Update()
    {
        if (Camera.main == null) return;

        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, pickUpRage, Collectable))
        {
            Debug.Log("Collectable");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Collected");
                CollectItem();
                Destroy(hit.transform.gameObject);
            }
        }
    }

    public void CollectItem()
    {
        if(inventory.metalCount > inventory.maxMetalCount)
        {
            inventory.metalCount += Metal;
        }
        if(inventory.scrapMetalCount < inventory.maxScrapMetalCount)
        {
            inventory.scrapMetalCount += ScrapMetal;
        }
        if (inventory.rawMetalCount < inventory.maxRawMetalCount)
        {
            inventory.rawMetalCount += RawMetal;
        }
        if (inventory.woodCount < inventory.maxWoodCount) 
        {
            inventory.woodCount += Wood;
        }
        if (inventory.scrapMetalCount < inventory.maxScrapMetalCount)
        {
            inventory.scrapWoodCount += ScrapWood;
        }
        if (inventory.nailsCount < inventory.maxNailsCount)
        {
            inventory.nailsCount += Nails;
        }
        if (inventory.paperCount < inventory.maxPaperCount)
        {
            inventory.paperCount += Paper;
        }
        if (inventory.plasticCount < inventory.maxPlasticCount)
        {
            inventory.plasticCount += Plastic;
        }
        if (inventory.ropeCount < inventory.maxRopeCount)
        {
            inventory.ropeCount += Rope;
        }
        if (inventory.clayCount < inventory.maxClayCount)
        {
            inventory.clayCount += Clay;
        }
        if (inventory.brickCount < inventory.maxBrickCount)
        {
            inventory.brickCount += Brick;
        }
        if (inventory.clothCount < inventory.maxClothCount)
        {
            inventory.clothCount += Cloth;
        }
    }
}
