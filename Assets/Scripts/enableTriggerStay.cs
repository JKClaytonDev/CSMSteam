using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableTriggerStay : MonoBehaviour
{
    public GameObject enable;
    public float time;
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name.Contains("Player"))
            time = Time.realtimeSinceStartup + 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        enable.SetActive(Time.realtimeSinceStartup < time);
    }
}
