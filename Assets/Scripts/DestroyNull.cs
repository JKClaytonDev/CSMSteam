using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNull : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject instantiateGO;
    public GameObject reference;
    // Update is called once per frame

    void Update()
    {
        bool destroy = true;
        foreach (GameObject k in objects)
        {
            if (k != null)
                destroy = false;
        }
        if (destroy)
        {
            Destroy(gameObject);
            if (instantiateGO)
            {
                GameObject i = Instantiate(instantiateGO);
                i.transform.position = transform.position;
                if (reference)
                    i.transform.position = reference.transform.position;
            }
        }
    }
}
