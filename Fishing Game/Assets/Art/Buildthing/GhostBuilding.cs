using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBuilding : MonoBehaviour
{
    MeshRenderer Mesh;

    private void Awake()
    {
        Mesh = GetComponent<MeshRenderer>();

        if (Mesh == null)
        {
            Mesh = GetComponentInChildren<MeshRenderer>();
        }
    }

    public void SetMaterial(Material material)
    {
        if (Mesh != null)
        {
            Mesh.material = material;
        }
    }
}
