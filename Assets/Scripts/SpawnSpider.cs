using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpider : MonoBehaviour
{
    float spawnTime;
    public GameObject brain;
    public GameObject spider;
    public float count = 1;
    // Start is called before the first frame update
    private void OnEnable()
    {
        spawnTime = Time.realtimeSinceStartup + 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup > spawnTime && count > 0)
        {
           GameObject f =  Instantiate(spider);
            f.transform.position = transform.position;
            Destroy(brain);
            count--;
        }
    }
}
