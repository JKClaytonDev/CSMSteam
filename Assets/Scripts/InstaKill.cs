using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaKill : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>() || other.gameObject.name.Contains("Player"))
        {
            FindObjectOfType<playerHealth>().health -= 120;
            GetComponent<Collider>().isTrigger = false;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() || collision.gameObject.name.Contains("Player"))
        {
            FindObjectOfType<playerHealth>().health -= 120;
        }
    }
}
