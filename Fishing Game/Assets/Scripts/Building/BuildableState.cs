using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableState : MonoBehaviour
{
    public Vector3 RootPosition = Vector3.zero;
    public Vector3 BuildOffset = Vector3.zero;

    public BuildPoint.AttachType AttachType = BuildPoint.AttachType.Wall;
}
