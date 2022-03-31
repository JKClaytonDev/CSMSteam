using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableTrigger : MonoBehaviour
{
    public GameObject t;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            t.SetActive(true);
        }
    }
}
