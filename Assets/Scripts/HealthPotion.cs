using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        FindObjectOfType<PlayerMovement>().refillHealth();
    }
}
