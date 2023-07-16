using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.UIElements;

public class BuildMechanic : MonoBehaviour
{
    private const float CheckSpotFilledMultipler = 0.3f;
    private const float CheckSpotNearbyBuildings = 1.1f;
    [Header("Prefab")]
    public GameObject WaterPlatformPrefab;
    public GameObject WallPrefab;
    public GameObject PlatformPrefab;

    [Header("GhostPrefabs")]
    public GameObject GhostWaterPlatformPrefab;
    public GameObject GhostWallPrefab;
    public GameObject GhostPlatformPrefab;

    private GameObject targetPrefab;
    private GameObject targetGhostPrefab;

    public GameObject buildGui;
    public GameObject hamerItem;

    public bool buildGuiOpen = false;
    public bool buildingPlatform = false;

    private GameObject SpawnedGhost;
    int rotation = 0;

    public Transform FacingObject;

    public float Grid = 9;

    [Header("Mats")]
    public Material GhostValid;
    public Material GhostInvalid;

    void Start()
    {
        buildGuiOpen = false;
        targetPrefab = WaterPlatformPrefab;
        targetGhostPrefab = GhostWaterPlatformPrefab;
    }

    void Update()
    {
        if(hamerItem != null)
        {
            if (Input.GetKeyUp(KeyCode.E)) 
            {
                buildGuiOpen = true;
                buildGui.SetActive(true);
            }
        }
        Escape();

        NewMethod();

        CheckInput();
    }

    private void NewMethod()
    {
        RaycastHit hit;
        int buildableLayer = 7; // Building layer
        int buildableLayerMask = 1 << buildableLayer;

        int builtLayer = 8; // Built layer
        int builtLayerMask = 1 << builtLayer;

        if (SpawnedGhost)
        {
            Destroy(SpawnedGhost);
        }

        if (Physics.Raycast(FacingObject.position, FacingObject.forward, out hit, 10000000, buildableLayerMask))
        {
            BuildableState buildableState = hit.collider.GetComponent<BuildableState>();

            if (buildableState != null)
            {
                Vector3 blockPlacePosition;

                bool rightSide = false;
                bool topSide = false;
                bool frontSide = false;

                rightSide = (FacingObject.forward.x < 0);
                topSide = (FacingObject.forward.y < 0);
                frontSide = (FacingObject.forward.z < 0);

                if ((Mathf.Abs(hit.point.z - hit.transform.position.z) > Mathf.Abs(hit.point.y - hit.transform.position.y)) && (Mathf.Abs(hit.point.z - hit.transform.position.z) > Mathf.Abs(hit.point.x - hit.transform.position.x)))
                {
                    if (frontSide)
                    {
                        blockPlacePosition = hit.transform.position + new Vector3(0, 0, +Grid);
                    }
                    else
                    {
                        blockPlacePosition = hit.transform.position + new Vector3(0, 0, -Grid);
                    }
                }
                else if (Mathf.Abs(hit.point.x - hit.transform.position.x) > Mathf.Abs(hit.point.y - hit.transform.position.y))
                {
                    if (rightSide)
                    {
                        blockPlacePosition = hit.transform.position + new Vector3(Grid, 0, 0);
                    }
                    else
                    {
                        blockPlacePosition = hit.transform.position + new Vector3(-Grid, 0, 0);
                    }
                }
                else
                {
                    if (topSide)
                    {
                        blockPlacePosition = hit.transform.position + new Vector3(0, +Grid, 0);
                    }
                    else
                    {
                        blockPlacePosition = hit.transform.position + new Vector3(0, -Grid, 0);
                    }
                }

                Quaternion wallRotation = GetRotation();
                SpawnedGhost = Instantiate(GhostWaterPlatformPrefab, blockPlacePosition, wallRotation);
                bool canBuild = false;
                BuildableState prefabBuildableState = targetPrefab.GetComponent<BuildableState>();

                if (prefabBuildableState == null)
                {
                    print("Error: Prefab doesnt have a buildableState");
                }
                else
                {
                    GhostBuilding ghostBuilding = SpawnedGhost.GetComponent<GhostBuilding>();

                    // Setup a box position and size to check if the position is already filled
                    Vector3 boxcastSize = prefabBuildableState.MainCollider.transform.localScale * CheckSpotFilledMultipler;
                    boxcastSize.x *= prefabBuildableState.MainCollider.size.x;
                    boxcastSize.y *= prefabBuildableState.MainCollider.size.y;
                    boxcastSize.z *= prefabBuildableState.MainCollider.size.z;
                    Vector3 rotatedSize = RotatePointAroundPivot(boxcastSize, Vector3.zero, wallRotation);

                    Vector3 rotatedPoint = RotatePointAroundPivot(prefabBuildableState.MainCollider.transform.localPosition, Vector3.zero, wallRotation);
                    Vector3 boxcastPosition = blockPlacePosition + rotatedPoint;

                    if (Physics.CheckBox(boxcastPosition, rotatedSize, Quaternion.identity, builtLayerMask))
                    {
                        canBuild = false;
                    }
                    else
                    {
                        // Setup a box position and size to check if there are items next to it to attach to
                        boxcastSize = prefabBuildableState.MainCollider.transform.localScale * CheckSpotNearbyBuildings;
                        boxcastSize.x *= prefabBuildableState.MainCollider.size.x;
                        boxcastSize.y *= prefabBuildableState.MainCollider.size.y;
                        boxcastSize.z *= prefabBuildableState.MainCollider.size.z;

                        rotatedSize = RotatePointAroundPivot(boxcastSize, Vector3.zero, wallRotation);

                        // Don't let the size be negative
                        rotatedSize.x = Mathf.Abs(rotatedSize.x);
                        rotatedSize.y = Mathf.Abs(rotatedSize.y);
                        rotatedSize.z = Mathf.Abs(rotatedSize.z);

                        if (Physics.CheckBox(boxcastPosition, rotatedSize, Quaternion.identity, builtLayerMask))
                        {
                            canBuild = true;
                        }
                        else
                        {
                            canBuild = false;
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
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (canBuild)
                    {
                        Instantiate(targetPrefab, blockPlacePosition, wallRotation);
                    }
                }
            }
        }
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            targetPrefab = WaterPlatformPrefab;
            targetGhostPrefab = GhostWaterPlatformPrefab;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            targetPrefab = WallPrefab;
            targetGhostPrefab = GhostWallPrefab;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            targetPrefab = PlatformPrefab;
            targetGhostPrefab = GhostPlatformPrefab;
        }

        // Rotate building
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rotation -= 1;
            }
            else
            {
                rotation += 1;
            }

            if (rotation > 3)
            {
                rotation = 0;
            }
            if (rotation < 0)
            {
                rotation = 3;
            }
        }
    }

    private Quaternion GetRotation()
    {
        Quaternion wallRotation = Quaternion.identity;
        switch (rotation)
        {
            case 0:
                wallRotation = Quaternion.Euler(0, 0, 0);
                break;
            case 1:
                wallRotation = Quaternion.Euler(0, 90, 0);
                break;
            case 2:
                wallRotation = Quaternion.Euler(0, 180, 0);
                break;
            case 3:
                wallRotation = Quaternion.Euler(0, 270, 0);
                break;
            default:
                break;
        }

        return wallRotation;
    }
    Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion angle)
    {
        Vector3 direction = point - pivot;
        direction = angle * direction;
        point = direction + pivot;
        return point;
    }
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
        if (Input.GetKeyDown(KeyCode.Escape) && buildGuiOpen == true)
        {
            ForceEscape();
        }
    }

    void ForceEscape()
    {
        //Move.enabled = true;
        //camScrpit.enabled = true;
        buildGui.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        buildGuiOpen = false;
    }
}
