using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WetBrickToDryBrick : MonoBehaviour
{
    public float drayTime;
    public GameObject prefab;

    void Start()
    {
        StartCoroutine(Drying());
    }


    IEnumerator Drying()
    {
        yield return new WaitForSeconds(drayTime);
        Instantiate(prefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
