using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAnimatorSpeed : MonoBehaviour
{
    public enemyHealth ch;
    

    // Update is called once per frame
    void Update()
    {
            GetComponent<Animator>().speed = 1 + Mathf.Sin(Time.realtimeSinceStartup/5) / 15;
        if (ch.currentHealth < 0.6f)
            GetComponent<Animator>().speed = 1.3f + Mathf.Sin(Time.realtimeSinceStartup/4)/8;
        if (ch.currentHealth < 0.3f)
            GetComponent<Animator>().speed = 2f + Mathf.Sin(Time.realtimeSinceStartup/3)/4;
    }
}
