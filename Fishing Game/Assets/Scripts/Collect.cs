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
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(Input.GetKey(KeyCode.E))
            { 
                CollectItem();
            }
        }
    }

    public void CollectItem()
    {
        metalCount.metalCount += Metal;
        scrapMetalCount.scrapMetalCount += ScrapMetal;
        rawMetalCount.rawMetalCount += RawMetal;
        woodCount.woodCount += Wood;
        scrapWoodCount.scrapWoodCount += ScrapWood;
        nailsCount.nailsCount += Nails;
        paperCount.paperCount += Paper;
        plasticCount.plasticCount += Plastic;
        ropeCount.ropeCount += Rope;
        clayCount.clayCount += Clay;
        brickCount.brickCount += Brick;
    }
}
