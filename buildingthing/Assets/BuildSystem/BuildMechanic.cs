using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMechanic : MonoBehaviour
{
    private const float MaxRange = 10000000;
    [Header("Build Prefabs")]
    public GameObject FloorPrefab;
    public GameObject WallPrefab;
    public GameObject WallDoorPrefab;
    public GameObject WallWindowPrefab;
    public GameObject FencePrefab;

    [Header("Ghost Prefabs")]
    public GameObject GhostFloorPrefab;
    public GameObject GhostWallPrefab;
    public GameObject GhostWallDoorPrefab;
    public GameObject GhostWallWindowPrefab;
    public GameObject GhostFencePrefab;

    [Header("Hitbox Prefabs")]
    public GameObject HitboxFloorPrefab;
    public GameObject HitboxWallPrefab;
    public GameObject HitboxWallDoorPrefab;
    public GameObject HitboxWallWindowPrefab;
    public GameObject HitboxFencePrefab;

    private GameObject targetPrefab;
    private GameObject targetGhostPrefab;
    private GameObject targetHitboxPrefab;

    private GameObject spawnedGhost;
    int rotation = 0;

    public Transform FacingObject;

    public Material GhostValid;
    public Material GhostInvalid;
    private int woodStored;
    private int woodPrice;

    bool inBuildMenu = false;

    [Header("UI")]
    public GameObject BuildMenu;
    public GameObject CrossHairs;


    void Start()
    {
        targetPrefab = FloorPrefab;
        targetGhostPrefab = GhostFloorPrefab;
        targetHitboxPrefab = HitboxFloorPrefab;
    }

    void Update()
    {
        Build();

        CheckInput();
    }

    void Build()
    {
        if (!targetPrefab)
            return;

        if (!targetGhostPrefab)
            return;

        RaycastHit hit;
        int buildableLayer = 6; // Building layer
        int buildableLayerMask = 1 << buildableLayer;

        int builtLayer = 7; // Built layer
        int builtLayerMask = 1 << builtLayer;

        if (spawnedGhost)
        {
            Destroy(spawnedGhost);
        }

        if (inBuildMenu)
            return;

        float raycastDistance = MaxRange;
        if (Physics.Raycast(transform.position, FacingObject.forward, out hit, raycastDistance, builtLayerMask))
        {
            raycastDistance = hit.distance;
        }

        RaycastHit[] raycastHits = Physics.RaycastAll(transform.position, FacingObject.forward, raycastDistance, buildableLayerMask);
        if (raycastHits.Length > 0)
        {
            BuildableState prefabBuildableState = targetPrefab.GetComponentInChildren<BuildableState>();
            bool canAttach = false;
            RaycastHit targetHit = new RaycastHit();
            BuildPoint buildPoint = new BuildPoint();

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

            if (!canAttach)
            {
                return;
            }

            BuildableState buildableState = targetHit.collider.GetComponentInParent<BuildableState>();

            if (buildableState != null)
            {
                Vector3 buildableItemPosition = targetHit.transform.position;// + hit.collider.bounds.center;
                Quaternion buildRotation = targetHit.collider.transform.rotation;

                Vector3 offsetPosition = RotatePointAroundPivot(prefabBuildableState.BuildOffset, Vector3.zero, buildRotation * Quaternion.Euler(0, -90, 0));
                Vector3 originOffset = RotatePointAroundPivot(buildPoint.BuildOffset, Vector3.zero, buildRotation * Quaternion.Euler(0, -90, 0));

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
                        Vector3 boxcastPosition = blockPlacePosition + rotatedPoint;

                        if (Physics.CheckBox(boxcastPosition, rotatedSize, Quaternion.identity, builtLayerMask))
                        {
                            canBuild = false;
                        }
                        else
                        {
                            canBuild = true;
                        }

                        if (prefabBuildableState.AttachType == BuildPoint.AttachType.Floor)
                        {
                            if (!Physics.Raycast(blockPlacePosition, Vector3.down, out hit, 2.05f, builtLayerMask))
                            {
                                canBuild = false;
                            }
                        }
                    }

                    if (canBuild)
                    {
                        ghostBuilding?.SetMaterial(GhostValid);
                    }
                    else
                    {
                        ghostBuilding?.SetMaterial(GhostInvalid);
                    }
                    //ghostBuilding.gameObject.SetActive(false);
                }

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

    private bool CanAffordResources()
    {
        if (woodPrice >= woodStored)
        {
            return true;
        }

        return false;
    }

    private void SpendResources()
    {
        woodStored -= woodPrice;
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

    public void SetTargetBuildFloor()
    {
        SetTargetBuild(FloorPrefab, GhostFloorPrefab, HitboxFloorPrefab, 0);
        CloseBuildMenu();
    }

    public void SetTargetBuildWall()
    {
        SetTargetBuild(WallPrefab, GhostWallPrefab, HitboxWallPrefab, 0);
        CloseBuildMenu();
    }

    public void SetTargetBuildFence()
    {
        SetTargetBuild(FencePrefab, GhostFencePrefab, HitboxFencePrefab, 0);
        CloseBuildMenu();
    }

    public void SetTargetBuildDoor()
    {
        SetTargetBuild(WallDoorPrefab, GhostWallDoorPrefab, HitboxWallDoorPrefab, 0);
        CloseBuildMenu();
    }

    public void SetTargetBuildWindow()
    {
        SetTargetBuild(WallWindowPrefab, GhostWallWindowPrefab, HitboxWallWindowPrefab, 0);
        CloseBuildMenu();
    }

    public void SetTargetBuild(GameObject cloneBuildPrefab, GameObject ghostClonePrefab, GameObject hitboxClonePrefab, int targetWoodPrice)
    {
        if (cloneBuildPrefab && ghostClonePrefab)
        {
            targetPrefab = cloneBuildPrefab;
            targetGhostPrefab = ghostClonePrefab;
            targetHitboxPrefab = hitboxClonePrefab;
            woodPrice = targetWoodPrice;
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
}
