using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class damagePlayer : MonoBehaviour
{
    public int customDamage = 1;
    public playerHealth ph;
    // Start is called before the first frame update
    void Start()
    {
        if (customDamage == 0)
            customDamage = 1;
        ph = FindObjectOfType<playerHealth>();
    }


    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.name == "Player")
        ph.health -= 60*Time.deltaTime * customDamage;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "Player")
            ph.health -= 60 * Time.deltaTime * customDamage;
    }
}
