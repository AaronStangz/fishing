using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float despawnTime;
    public void Start()
    {
        StartCoroutine(DespawnTime());
    }

    IEnumerator DespawnTime()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
