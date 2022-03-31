using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintTrigger : MonoBehaviour
{
    public GameObject child;

    private void Start()
    {
        child.SetActive(false);
        GetComponent<MeshRenderer>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            child.SetActive(true);
            Destroy(gameObject, 4);
        }
    }
}
