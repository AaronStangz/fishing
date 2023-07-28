using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Timeline.Actions;

public class BuildMechanic : MonoBehaviour
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

    private const float MaxRange = 10000000;
    private const float FloorCastDownRange = 2.05f;
    [Header("Build Prefabs")]
    public GameObject FloorPrefab;
    public GameObject StairsPrefab;
    public GameObject WallPrefab;
    public GameObject WallDoorPrefab;
    public GameObject WallWindowPrefab;
    public GameObject FencePrefab;

    [Header("Ghost Prefabs")]
    public GameObject GhostFloorPrefab;
    public GameObject GhostStairsPrefab;
    public GameObject GhostWallPrefab;
    public GameObject GhostWallDoorPrefab;
    public GameObject GhostWallWindowPrefab;
    public GameObject GhostFencePrefab;

    [Header("Hitbox Prefabs")]
    public GameObject HitboxFloorPrefab;
    public GameObject HitboxStairsPrefab;
    public GameObject HitboxWallPrefab;
    public GameObject HitboxWallDoorPrefab;
    public GameObject HitboxWallWindowPrefab;
    public GameObject HitboxFencePrefab;

    public GameObject[] CanBeRotatedPrefabs;

    private GameObject targetPrefab;
    private GameObject targetGhostPrefab;
    private GameObject targetHitboxPrefab;

    private GameObject spawnedGhost;
    int rotation = 0; // Used for stairs

    public Transform FacingObject;
    public GameObject player;
    public GameObject canBuildGuiText;

    public Material GhostValid;
    public Material GhostInvalid;
    private int woodPrice;
    private int nailsPrice;
    private int metalPrice;

    bool inBuildMenu = false;

    public LayerMask targetBuildableLayer;
    public LayerMask alreadyBuiltLayer;
    public Vector3 BuildScale = new Vector3(4.25f, 3, 4.25f);

    [Header("UI")]
    public GameObject BuildMenu;
    public GameObject CrossHairs;


    void Start()
    {
        targetPrefab = FloorPrefab;
        targetGhostPrefab = GhostFloorPrefab;
        targetHitboxPrefab = HitboxFloorPrefab;

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
    }

    void Update()
    {
        CheckRotateBuilding();
        Build();

        CheckInput();

        Escape();
        //canBuildGuiText.GetComponent<TextMeshProUGUI>().text = "(Wood: " + woodCount.woodCount + "/" + woodPrice + " )";
    }

    void Build()
    {
        if (!targetPrefab)
            return;

        if (!targetGhostPrefab)
            return;

        RaycastHit hit;

        if (spawnedGhost)
        {
            Destroy(spawnedGhost);
        }

        if (inBuildMenu)
            return;

        // Check to make sure there is nothing in the way
        float raycastDistance = MaxRange;
        if (Physics.Raycast(FacingObject.position, FacingObject.forward, out hit, raycastDistance, alreadyBuiltLayer))
        {
            raycastDistance = hit.distance;
        }

        // Check raycast for the buildable anchor points
        RaycastHit[] raycastHits = Physics.RaycastAll(FacingObject.position, FacingObject.forward, raycastDistance, targetBuildableLayer);
        if (raycastHits.Length > 0)
        {
            BuildableState prefabBuildableState = targetPrefab.GetComponentInChildren<BuildableState>();
            bool canAttach = false;
            RaycastHit targetHit = new RaycastHit();
            BuildPoint buildPoint = null;

            foreach (RaycastHit raycastHit in raycastHits)
            {
                buildPoint = raycastHit.collider.GetComponent<BuildPoint>();
                if (buildPoint)
                {
                    if (buildPoint.AllowedAttachedTypes.Contains(prefabBuildableState.AttachType))
                    {
                        canAttach = true;
                        targetHit = raycastHit;
                        break;
                    }
                }
            }

            // Did we find an anchor we can attach to?
            if (!canAttach)
            {
                return;
            }

            BuildableState buildableState = targetHit.collider.GetComponentInParent<BuildableState>();

            if (buildableState != null)
            {
                Vector3 buildableItemPosition = targetHit.transform.position;

                // check if this prefab can be rotated manually
                bool canBeRotated = CanTargetPrefabBeRotated(targetPrefab);
                Quaternion buildRotation;
                if (canBeRotated)
                {
                    buildRotation = Quaternion.Euler(0, rotation * 90, 0);
                }
                else
                {
                    buildRotation = targetHit.collider.transform.rotation;
                }

                Vector3 offsetPosition = RotatePointAroundPivot(prefabBuildableState.BuildOffset, Vector3.zero, buildRotation * Quaternion.Euler(0, -90, 0));
                Vector3 originOffset = RotatePointAroundPivot(buildPoint.BuildOffset, Vector3.zero, buildRotation * Quaternion.Euler(0, -90, 0));

                offsetPosition.x *= BuildScale.x;
                offsetPosition.y *= BuildScale.y;
                offsetPosition.z *= BuildScale.z;

                originOffset.x *= BuildScale.x;
                originOffset.y *= BuildScale.y;
                originOffset.z *= BuildScale.z;

                Vector3 blockPlacePosition = Vector3.zero;
                blockPlacePosition += offsetPosition;
                blockPlacePosition += buildableItemPosition;
                blockPlacePosition += originOffset;

                spawnedGhost = Instantiate(targetGhostPrefab, blockPlacePosition, buildRotation);

                bool canBuild = true;

                if (prefabBuildableState == null)
                {
                    print("Error: Prefab doesnt have a buildableState");
                }
                else
                {
                    GhostBuilding ghostBuilding = spawnedGhost.GetComponent<GhostBuilding>();

                    BoxCollider boxcollider = targetHitboxPrefab.GetComponentInChildren<BoxCollider>();
                    if (boxcollider == null)
                    {
                        canBuild = false;
                        print("boxcollider == null");
                    }
                    else
                    {
                        // Setup a box position and size to check if the position is already filled
                        Vector3 boxcastSize = boxcollider.transform.localScale;
                        boxcastSize.x *= boxcollider.size.x;
                        boxcastSize.y *= boxcollider.size.y;
                        boxcastSize.z *= boxcollider.size.z;
                        Vector3 rotatedSize = RotatePointAroundPivot(boxcastSize, Vector3.zero, buildRotation);

                        Vector3 rotatedPoint = RotatePointAroundPivot(boxcollider.transform.localPosition, Vector3.zero, buildRotation);

                        rotatedPoint.x *= BuildScale.x;
                        rotatedPoint.y *= BuildScale.y;
                        rotatedPoint.z *= BuildScale.z;

                        Vector3 boxcastPosition = blockPlacePosition + rotatedPoint;

                        // Check the funky physics with already built objects
                        if (Physics.CheckBox(boxcastPosition, rotatedSize, Quaternion.identity, alreadyBuiltLayer))
                        {
                            canBuild = false;
                        }
                        else
                        {
                            canBuild = true;
                        }

                        if (prefabBuildableState.AttachType == BuildPoint.AttachType.Floor)
                        {
                            // If its a floor, make sure theres something under it to build on with a raycast
                            if (!Physics.Raycast(blockPlacePosition, Vector3.down, out hit, FloorCastDownRange * BuildScale.y, alreadyBuiltLayer))
                            {
                                canBuild = false;
                            }
                        }
                    }

                    // Set ghost material to valid or invalid
                    if (canBuild && CanAffordResources())
                    {
                        ghostBuilding?.SetMaterial(GhostValid);
                    }
                    else
                    {
                        ghostBuilding?.SetMaterial(GhostInvalid);
                    }
                }

                // Mouse button pushed. Place it NOW!
                if (Input.GetMouseButtonDown(0))
                {
                    if (canBuild)
                    {
                        if (CanAffordResources())
                        {
                            GameObject spawnedBuildingPiece = Instantiate(targetPrefab, blockPlacePosition, buildRotation);

                            SpendResources();
                        }
                    }
                }
            }
        }
    }

    private bool CanTargetPrefabBeRotated(GameObject prefab)
    {
        foreach (GameObject rotateableObject in CanBeRotatedPrefabs)
        {
            if (rotateableObject == prefab)
            {
                return true;
            }
        }

        return false;
    }

    private bool CanAffordResources()
    {
        if (woodCount.woodCount >= woodPrice && nailsCount.nailsCount >= nailsPrice && metalCount.metalCount >= metalPrice)
        {
            return true;
        }

        return false;
    }

    private void SpendResources()
    {
        woodCount.woodCount -= woodPrice;
        nailsCount.nailsCount -= nailsPrice;
        metalCount.metalCount -= metalPrice;
    }

    /// <summary>
    /// Called each tick
    /// </summary>
    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleBuildMenu();
        }
    }
    private void ToggleBuildMenu()
    {
        inBuildMenu = !inBuildMenu;
        CrossHairs.SetActive(!inBuildMenu);
        BuildMenu.SetActive(inBuildMenu);

        Cursor.visible = inBuildMenu;
        if (inBuildMenu)
        {
            Cursor.lockState = CursorLockMode.None;
            //TODO: Lock player look around movement
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            //TODO: Unlock player look around movement
        }

    }
    private void CloseBuildMenu()
    {
        inBuildMenu = false;
        CrossHairs.SetActive(true);
        BuildMenu.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        //TODO: Unlock player look around movement
    }

    public void CheckRotateBuilding()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                rotation--;
            }
            else
            {
                rotation++;
            }

            if (rotation > 3)
            {
                rotation = 0;
            }
            else if (rotation < 0)
            {
                rotation = 3;
            }
        }
    }

    public void SetTargetBuildFloor()
    {
        SetTargetBuild(FloorPrefab, GhostFloorPrefab, HitboxFloorPrefab, 10, 10, 0);
        CloseBuildMenu();
    }
    public void SetTargetBuildStairs()
    {
        SetTargetBuild(StairsPrefab, GhostStairsPrefab, HitboxStairsPrefab, 12, 12, 0);
        CloseBuildMenu();
    }

    public void SetTargetBuildWall()
    {
        SetTargetBuild(WallPrefab, GhostWallPrefab, HitboxWallPrefab, 8, 8, 0);
        CloseBuildMenu();
    }

    public void SetTargetBuildFence()
    {
        SetTargetBuild(FencePrefab, GhostFencePrefab, HitboxFencePrefab, 4, 4, 0);
        CloseBuildMenu();
    }

    public void SetTargetBuildDoor()
    {
        SetTargetBuild(WallDoorPrefab, GhostWallDoorPrefab, HitboxWallDoorPrefab, 4, 2, 0);
        CloseBuildMenu();
    }

    public void SetTargetBuildWindow()
    {
        SetTargetBuild(WallWindowPrefab, GhostWallWindowPrefab, HitboxWallWindowPrefab, 6, 4, 0);
        CloseBuildMenu();
    }

    public void SetTargetBuild(GameObject cloneBuildPrefab, GameObject ghostClonePrefab, GameObject hitboxClonePrefab, int targetWoodPrice, int targetNailPrice, int targetMetalPrice)
    {
        if (cloneBuildPrefab && ghostClonePrefab)
        {
            targetPrefab = cloneBuildPrefab;
            targetGhostPrefab = ghostClonePrefab;
            targetHitboxPrefab = hitboxClonePrefab;
            woodPrice = targetWoodPrice;
            nailsPrice = targetNailPrice;
            metalPrice = targetMetalPrice;
        }
    }

    Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion angle)
    {
        Vector3 direction = point - pivot;
        direction = angle * direction;
        point = direction + pivot;
        return point;
    }

    /// <summary>
    /// For debugging  from gizmos
    /// </summary>
    /// <param name="canBuild"></param>
    /// <param name="rotatedSize"></param>
    /// <param name="boxcastPosition"></param>
    private static void DrawCube(bool canBuild, Vector3 rotatedSize, Vector3 boxcastPosition)
    {
        if (canBuild)
        {
            Gizmos.color = new Color(1, 1, 1, 0.5f);
            Gizmos.DrawCube(boxcastPosition, rotatedSize);
        }
        else
        {
            Gizmos.color = new Color(1, 0, 1, 0.5f);
            Gizmos.DrawCube(boxcastPosition, rotatedSize);
        }
    }

    void Escape()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && inBuildMenu == true)
        {
            ForceEscape();
        }
    }

    void ForceEscape()
    {
        ToggleBuildMenu();
        BuildMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        inBuildMenu = false;
    }
}
