using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableNextScene : MonoBehaviour
{
    public GameObject enable;
    public GameObject disable;

    // Update is called once per frame
    void Update()
    {
        enable.SetActive(true);
        disable.SetActive(false);
    }
}
