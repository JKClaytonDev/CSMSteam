using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 1);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<enemyHealth>())
            other.gameObject.GetComponent<enemyHealth>().currentHealth -= Time.deltaTime * 20 * other.gameObject.GetComponent<enemyHealth>().damages[2];
        
    }
}
