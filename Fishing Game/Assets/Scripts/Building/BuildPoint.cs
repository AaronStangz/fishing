using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPoint : MonoBehaviour
{
  public enum AttachType
    {
        Wall,
        Floor,
        Ceiling,
        Fence
    }
    public List<AttachType> AllowedAttachedTypes;

    // How far offset the item will be from this hitbox
    public Vector3 BuildOffset;
}
