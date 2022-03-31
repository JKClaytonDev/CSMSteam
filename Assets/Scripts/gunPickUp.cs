using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunPickUp : MonoBehaviour
{
    public int index;
    public AudioClip sound;
    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<WolfMovement>())
        {
            WolfMovement m = other.gameObject.GetComponent<WolfMovement>();
            m.equippedWeapons[index] = true;
            m.equippedWeapon = index;
            other.gameObject.GetComponent<AudioSource>().PlayOneShot(sound);
            Destroy(gameObject);
        }
    }
    void Update()
    {
        transform.position = startPos + ((transform.up / 3) * (Mathf.Sin(Time.realtimeSinceStartup)));   
    }

}
