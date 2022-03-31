using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombScript : MonoBehaviour
{
    public GameObject explode;
    private void OnCollisionEnter(Collision collision)
    {
        GameObject k = Instantiate(explode);
        k.transform.position = transform.position;
        Destroy(k, 1);
        Destroy(gameObject);
    }
}
