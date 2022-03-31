using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionCollider : MonoBehaviour
{
    Vector3 pos;
    private void Start()
    {
        pos = transform.position;
    }
    private void Update()
    {
        transform.position = pos + Vector3.up * Mathf.Sin(Time.realtimeSinceStartup);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            
            FindObjectOfType<PlayerAnimations>().gunAnim.Play("DrinkPotion");

            Destroy(gameObject);
        }
    }
}
