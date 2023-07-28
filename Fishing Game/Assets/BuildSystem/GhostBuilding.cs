using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostBuilding : MonoBehaviour
{
    List<MeshRenderer> Meshes = new List<MeshRenderer>();

  private void Awake()
  {
        MeshRenderer[] meshes1 = GetComponents<MeshRenderer>();
        if (meshes1.Length > 0)
        {
            Meshes.AddRange(meshes1);
        }
        MeshRenderer[] meshes2 = GetComponentsInChildren<MeshRenderer>();
        if (meshes2.Length > 0)
        {
            Meshes.AddRange(meshes2);
        }
  }

  public void SetMaterial(Material material)
  {
    foreach (var mesh in Meshes)
    {
      if (mesh != null)
      {
        mesh.material = material;
      }
    }
  }
}