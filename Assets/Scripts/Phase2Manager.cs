using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Manager : MonoBehaviour
{
    public enemyHealth h;
    float startHealth;
    public GameObject phase2;
    public GameObject phase3;
    // Start is called before the first frame update
    void Start()
    {
        startHealth = h.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        phase2.SetActive(h.currentHealth / startHealth < 0.7f);
        phase3.SetActive(h.currentHealth / startHealth < 0.4f);
    }
}
