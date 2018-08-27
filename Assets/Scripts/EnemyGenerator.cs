using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemy;
    List<Transform> spawners;

    // Use this for initialization
    void Start()
    {
        spawners = new List<Transform>();
        foreach (Transform child in transform)
        {
            spawners.Add(child);
        }
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Spawn()
    {
        int platform1 = Random.Range(0, spawners.Count);
        int platform2 = Random.Range(0, spawners.Count);

        Instantiate(enemy, spawners[platform1].position + Vector3.up, new Quaternion());
        Instantiate(enemy, spawners[platform2].position + Vector3.up, new Quaternion());
        yield return new WaitForSeconds(7);
        yield return Spawn();
    }
}
