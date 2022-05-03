using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public void Start()
    {
        Time.timeScale = 1;
    }
    public AudioClip sound;

    private void OnCollisionStay(Collision collision)
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
