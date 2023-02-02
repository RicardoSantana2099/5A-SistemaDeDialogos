using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lluvia : MonoBehaviour
{
    public GameObject[] Rocas;
    public float SegundosSpawn = 0.5f;
    public float MinPiedra;
    public float MaxPiedra;
    void Start()
    {
        StartCoroutine(RockSpawn());
    }

    IEnumerator RockSpawn()
    {
        while(true)
        {
            var wanted = Random.Range(MinPiedra, MaxPiedra);
            var position = new Vector3(wanted, transform.position.y);
            GameObject gameObject = Instantiate(Rocas[Random.Range(0, Rocas.Length)], position, Quaternion.identity);
            yield return new WaitForSeconds(SegundosSpawn);
            Destroy(gameObject, 5f);
        }
    }

  
}
