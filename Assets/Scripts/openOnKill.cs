using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openOnKill : MonoBehaviour
{
    public GameObject[] objects;
    public float speed = 50;
    // Update is called once per frame
    void Update()
    {
        bool go = true;
        foreach (GameObject g in objects)
        {
            if (g != null)
                go = false;
        }
        if (go)
            transform.position -= speed * transform.up * Time.deltaTime;
    }
}
