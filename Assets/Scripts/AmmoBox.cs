using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public AudioClip sound;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            FindObjectOfType<PlayerVoices>().FoundAmmo();
            FindObjectOfType<WeaponsAnim>().refillAmmo();
            FindObjectOfType<PlayerMovement>().GetComponent<AudioSource>().PlayOneShot(sound);
            Destroy(gameObject);
        }
    }
}
