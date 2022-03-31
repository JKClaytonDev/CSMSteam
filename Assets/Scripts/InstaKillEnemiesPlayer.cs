using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaKillEnemiesPlayer : MonoBehaviour
{
    public bool enable;
    private void OnTriggerStay(Collider other)
    {
        if (!enable)
            return;
        if (other.gameObject.GetComponent<PlayerMovement>() || other.gameObject.name.Contains("Player"))
        {
            FindObjectOfType<playerHealth>().health -= 120;
            GetComponent<Collider>().isTrigger = false;
        }
        if (other.gameObject.GetComponent<EnemyHealth>())
        {
            Destroy(other.gameObject);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (!enable)
            return;
        if (collision.gameObject.GetComponent<PlayerMovement>() || collision.gameObject.name.Contains("Player"))
        {
            FindObjectOfType<playerHealth>().health -= 120;
        }
        if (collision.gameObject.GetComponent<EnemyHealth>())
        {
            Destroy(collision.gameObject);
        }
    }
}
