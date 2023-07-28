using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Harpoon : MonoBehaviour
{
    [Header("Ref")]
    public Transform cam;
    public Transform harpoonTip;
    public LayerMask whatIsHarpoonable;
    public LineRenderer lr;
    private GameObject objectHit;
    //public Rigidbody rb;

    [Header("Valuse")]
    public float dragDist;
    public float dragSpeed;
    public float bringSpeed;
    public float bringSpeedDefault;
    public float maxHarpoonDist;
    public float harpoonCd;
    public float harpoonCdTimer;
    public float harpoonPickUpDist;
    private Vector3 harpoonPoint;

    [Header("Slider")]
    public Slider harpooningBarSlider;
    public GameObject harpooingBarGrafic;

    [Header("Keys")]
    public KeyCode HarpoonKeyTake = KeyCode.Mouse1;
    public KeyCode HarpoonKeyLetGo = KeyCode.Mouse0;

    private bool harpooning = false;
    private bool sliderdone = false;

    public Collect collect;

    private void Start()
    {
        harpooningBarSlider.maxValue = harpoonCd;
        harpooning = false;
        sliderdone = false;
        harpoonCdTimer = 0;

    }
    void Update()
    {
        if (Input.GetKeyDown(HarpoonKeyTake) && !harpooning)
        {
            sliderdone = false;
            StartHarpoon();
        }
        if (Input.GetKeyUp(HarpoonKeyLetGo) && harpooning)
        {
            Debug.Log("LetGo");
            StopHarpoon();
        }
        if (Input.GetKey(HarpoonKeyTake))
        {
            BringIn();
        }
        DragObject();
        CollectObject();
        BingInSpeed();


        if (harpoonCdTimer < harpoonCd)
            harpoonCdTimer += Time.deltaTime;

        harpooningBarSlider.value = harpoonCdTimer;

        if (harpoonCdTimer >= harpoonCd && !sliderdone)
        {
            harpooingBarGrafic.SetActive(false);
            sliderdone = true;
            Debug.Log("BarGrafic");
        }

        
    }

    private void LateUpdate()
    {
        if (harpooning)
            lr.SetPosition(0, harpoonTip.position);

        if(objectHit != null)
        {
            lr.SetPosition(1, objectHit.transform.position);
        }
    }
    private void StartHarpoon()
    {
        if (harpoonCdTimer <= harpoonCd) return;

        RaycastHit hit;
        if(Physics.Raycast(cam.position, cam.forward, out hit, maxHarpoonDist, whatIsHarpoonable))
        {
            if (hit.transform.CompareTag("HarpoonableFish"))
            {
                print("HitFish");
                FishCollect(hit.transform.gameObject);
            }

            else if (hit.transform.CompareTag("HarpoonableObject"))
            {
                print("HitObject");
                objectHit = hit.transform.gameObject;
                dragDist = Vector3.Distance(objectHit.transform.position, transform.position);
            }

            harpoonPoint = hit.point;
            harpooning = true;
        }
        else
        {
            harpoonPoint = cam.position + cam.forward * maxHarpoonDist;
            StopHarpoon();
        }

        lr.enabled = true;
        lr.SetPosition(1, harpoonPoint);
    }

    private void BringIn()
    {
        if (objectHit == null) return;
        print("BringIn");
        objectHit.transform.position = Vector3.MoveTowards(objectHit.transform.position, transform.position, Time.deltaTime * bringSpeed);
    }

    private void CollectObject()
    {
        if (objectHit == null) return;
        print("collected");
        float distFromTarget = Vector3.Distance(objectHit.transform.position, transform.position);
        if (distFromTarget < harpoonPickUpDist)
        {
            objectHit.GetComponent<Collect>().CollectItem();
            Destroy(objectHit);
            StopHarpoon();
            bringSpeed = bringSpeedDefault;
            objectHit = null;
        }
    }

    private void BingInSpeed()
    {
        if (objectHit == null) return;
        float distFromTarget = Vector3.Distance(objectHit.transform.position, transform.position);
        if (distFromTarget < 5)
        {
            bringSpeed = 0.5f + bringSpeed;
        }
        else if (distFromTarget < 10)
        {
            bringSpeed = 0.5f + bringSpeed;
        }
        else if (distFromTarget < 15)
        {
            bringSpeed = 0.5f + bringSpeed;
        }
        else if (distFromTarget < 20)
        {
            bringSpeed = 0.5f + bringSpeed;
        }
        else if (distFromTarget < 25)
        {
            bringSpeed = 0.5f + bringSpeed;
        }
        else if (distFromTarget < 30)
        {
            bringSpeed = 0.5f + bringSpeed;
        }
        else if (distFromTarget < 35)
        {
            bringSpeed = 0.5f + bringSpeed;
        }
        else if (distFromTarget < 40)
        {
            bringSpeed = 0.5f + bringSpeed;
        }
    }

    private void FishCollect(GameObject fish)
    {
        fish.GetComponent<Collect>().CollectItem();
        print("collectedFish");
        Destroy(fish);
        StopHarpoon();
    }

    private void DragObject()
    {
        if (objectHit == null) return;
        float distFromTarget = Vector3.Distance(objectHit.transform.position, transform.position);
        print($"{distFromTarget} {dragDist}");
        if (distFromTarget > dragDist)
        {
            objectHit.transform.position = Vector3.MoveTowards(objectHit.transform.position, transform.position, Time.deltaTime * dragSpeed);
        }
    }

    private void StopHarpoon()
    {
        harpooning = false;

        objectHit = null;

        harpoonCdTimer = 0;
        harpooningBarSlider.value = 0;

        sliderdone = false;
        lr.enabled = false;
        harpooingBarGrafic.SetActive(true);
        
    }
}
