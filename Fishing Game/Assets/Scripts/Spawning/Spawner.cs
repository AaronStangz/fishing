using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> Spawnpoints;


    public List<GameObject> Spawning;

    void Start()
    {
        foreach (GameObject p in Spawnpoints)
        {
            Instantiate(Spawning[Random.Range(0, Spawning.Count)],p.transform);
        }
    }

   
}
