using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeChildren : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject g in transform)
            g.transform.parent = null;
    }

}
