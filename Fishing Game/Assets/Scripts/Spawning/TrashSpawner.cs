using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public float spawntimer;
    public float SpawnArea = 10;
    public List<GameObject> Spawnpoints;
    public List<GameObject> Spawning;

    public void Start()
    {
        StartCoroutine(Spawntimer());
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) 
        {
            foreach (GameObject p in Spawnpoints)
            {
                Vector3 spawnPoint = new Vector3(Random.Range(-SpawnArea, SpawnArea), 0, Random.Range(-SpawnArea, SpawnArea));
                Instantiate(Spawning[Random.Range(0, Spawning.Count)], p.transform.position + spawnPoint, p.transform.rotation);
            }
        }
    }

    IEnumerator Spawntimer()
    {
        yield return new WaitForSeconds(spawntimer);
        foreach (GameObject p in Spawnpoints)
        {
            Vector3 spawnPoint = new Vector3(Random.Range(-SpawnArea, SpawnArea), 0, Random.Range(-SpawnArea, SpawnArea));
            Instantiate(Spawning[Random.Range(0, Spawning.Count)], p.transform.position + spawnPoint, p.transform.rotation);
        }
        Done();
    }

    public void Done()
    {
        StartCoroutine(Spawntimer());
    }

}
