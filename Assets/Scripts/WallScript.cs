using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    public GameObject[] enemies;

    // Update is called once per frame
    void Update()
    {
        bool found = false;
        foreach (GameObject a in enemies)
        {
            if (a)
                found = true;
        }
        if (!found)
            transform.position -= Vector3.up * 10 * Time.deltaTime;
    }
}
