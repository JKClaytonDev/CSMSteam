using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBag : MonoBehaviour
{
    public AudioClip SFX;
    public AudioClip Life;
    public GameObject lifeEarned;
    private void Start()
    {
        transform.parent = null;
        Time.timeScale = 1;
    }
    private void OnEnable()
    {
        transform.parent = null;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("JUST HIT " + other);
        if (other.gameObject.layer == 11)
        {
            if (Time.realtimeSinceStartup < FindObjectOfType<PlayerVoices>().playTime)
                FindObjectOfType<PlayerMovement>().GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
            FindObjectOfType<PlayerMovement>().GetComponent<AudioSource>().PlayOneShot(SFX);
            FindObjectOfType<PlayerMovement>().money += 10;
            PlayerPrefs.SetFloat("LevelStartCash", PlayerPrefs.GetFloat("LevelStartCash") + 10);
            if (FindObjectOfType<PlayerMovement>().money > 200)
            {
                FindObjectOfType<PlayerMovement>().money = 0;
                FindObjectOfType<PlayerMovement>().GetComponent<AudioSource>().pitch = 1;
                FindObjectOfType<PlayerMovement>().GetComponent<AudioSource>().PlayOneShot(Life, 3);
                if (lifeEarned)
                    Instantiate(lifeEarned);
                PlayerPrefs.SetInt("Lives", PlayerPrefs.GetInt("Lives") + 1);
                FindObjectOfType<lifeCounter>().checkLives();
            }
            Destroy(gameObject);
        }
    }
}
