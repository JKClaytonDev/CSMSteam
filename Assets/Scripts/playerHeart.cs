using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHeart : MonoBehaviour
{
    

    public AudioClip sound;
    private void Start()
    {

        if (FindObjectOfType<playerHealth>().health >= PlayerPrefs.GetFloat("MaxHealth"))
        {
            FindObjectOfType<playerHealth>().health = PlayerPrefs.GetFloat("MaxHealth");
            Destroy(gameObject);
        }
        if (Random.Range(1, 3) != 1)
            Destroy(gameObject);
        Destroy(gameObject, 4);
        GetComponent<Rigidbody>().velocity = Vector3.up * 15;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            if (FindObjectOfType<playerHealth>().health < PlayerPrefs.GetFloat("MaxHealth"))
            {
                transform.parent = null;
                FindObjectOfType<PlayerMovement>().refillHealth();
                FindObjectOfType<PlayerMovement>().GetComponent<AudioSource>().PlayOneShot(sound);
            }
            Destroy(gameObject);
        }
        
    }
}
