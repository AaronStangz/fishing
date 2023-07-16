using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public float TrashSpeed;
    public float despawnTime;

    Vector3 targetDirection;
    public void Start()
    {
        targetDirection = transform.forward;
        StartCoroutine(DespawnTime());
    }

    void Update()
    {
        transform.position += targetDirection * Time.deltaTime * TrashSpeed;
    }

    IEnumerator DespawnTime()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
