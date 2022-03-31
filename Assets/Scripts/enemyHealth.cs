using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public MeshRenderer m;
    public bool dontshow;
    public float stunTime;
    public string enemyName;
    public bool unkillable;
    public GameObject parent;
    public GameObject disableLowHealth;
    public float currentHealth = 1;
    public float[] damages;
    public GameObject bloodObj;
    public AudioClip hitSound;
    public float bloodTime;
    public float colorTime;
    // Update is called once per frame
    void Update()
    {

        if (stunTime > Time.realtimeSinceStartup)
        {
            bool enable = stunTime < Time.realtimeSinceStartup + 0.05f;
            if (GetComponent<ZombieScript>())
            {
                GetComponent<ZombieScript>().enabled = enable;
            }
            if (GetComponent<MoveEnemy>())
            {
                GetComponent<MoveEnemy>().enabled = enable;
            }
            if (GetComponent<BlobFollowAI>())
            {
                GetComponent<BlobFollowAI>().enabled = enable;
            }

            
        }
        if (colorTime > Time.realtimeSinceStartup)
        {
            if (!m)
            {
                if (GetComponent<MeshRenderer>())
                    m = GetComponent<MeshRenderer>();
                else if (GetComponentInChildren<MeshRenderer>())
                    m = GetComponentInChildren<MeshRenderer>();
                else if (transform.GetChild(0).gameObject.GetComponent<MeshRenderer>())
                    m = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
            }
            bool enable = colorTime < Time.realtimeSinceStartup + 0.05f;
            if (m != null)
            {
                if (!enable)
                {
                    m.GetComponent<Animator>().enabled = false;
                    float emissiveIntensity = 1000;
                    Color emissiveColor = Color.red;
                    m.material.SetColor("_EmissiveColor", emissiveColor * emissiveIntensity);
                }
                else
                {
                    m.GetComponent<Animator>().enabled = true;
                    float emissiveIntensity = 0;
                    Color emissiveColor = Color.black;
                    m.material.SetColor("_EmissiveColor", emissiveColor * emissiveIntensity);
                }
            }
        }
        if (disableLowHealth)
            disableLowHealth.gameObject.SetActive(currentHealth != 1);
        else if (currentHealth < 0)
        {
            FindObjectOfType<PlayerVoices>().KillZombie();
            GameObject k = Instantiate(FindObjectOfType<PlayerMovement>().deadEnemyParticles);
            PlayerPrefs.SetFloat("LevelKills", PlayerPrefs.GetFloat("LevelKills") + 1);
            k.transform.parent = null;
            k.transform.position = transform.position;
            Destroy(k, 1);
            GameObject g = new GameObject();
            g.AddComponent<AudioSource>();
            g.GetComponent<AudioSource>().PlayOneShot(hitSound);
            Destroy(g);
            if (parent)
                Destroy(parent);
            Destroy(gameObject);
        }
    }
}
