using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowHealthSpawner : MonoBehaviour
{
    public float healthCutoff;
    public GameObject spawn;
    public enemyHealth bossHealth;
    // Start is called before the first frame update
   
    // Update is called once per frame
    void Update()
    {
        spawn.SetActive(bossHealth.currentHealth < healthCutoff);
    }
}
