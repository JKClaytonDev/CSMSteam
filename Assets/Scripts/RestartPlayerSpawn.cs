using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartPlayerSpawn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
            FindObjectOfType<PlayerMovement>().transform.position = FindObjectOfType<PlayerMovement>().startSpawnPos;
    }
}
