using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectAtPosition : MonoBehaviour
{
    GameObject spawn;
    void OnEnable()
    {
        GameObject k = Instantiate(spawn);
        k.transform.parent = null;
        k.transform.position = transform.position;
    }
}
