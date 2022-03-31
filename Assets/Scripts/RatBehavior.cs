using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBehavior : MonoBehaviour
{
    float randomTime;
    // Start is called before the first frame update
    void Start()
    {
        randomTime = Time.realtimeSinceStartup + 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup > randomTime)
        {
            GetComponent<Animator>().speed = Random.Range(0.3f, 0.1f);
            GetComponent<Animator>().SetInteger("Random", (int)Random.Range(-1, 1));

            randomTime = Time.realtimeSinceStartup + 10;
        }
    }
}
