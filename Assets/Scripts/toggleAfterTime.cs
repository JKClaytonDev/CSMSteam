using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleAfterTime : MonoBehaviour
{
    public GameObject enable;
    public GameObject disable;
    public float time;
    public float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.realtimeSinceStartup + time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup > startTime)
        {
            enable.SetActive(true);
            Destroy(disable);
        }

    }
}
