using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantScript : MonoBehaviour
{
    float collideTime;
    public GameObject instantiate;
    private void OnTriggerStay(Collider other)
    {
        if (other.name.Contains("Player") && Time.realtimeSinceStartup > collideTime)
        {
            FindObjectOfType<PlayerVoices>().Gas();
            collideTime = Time.realtimeSinceStartup + 5;
            GameObject k = Instantiate(instantiate);
            Destroy(k, 5);
        }
    }

}
