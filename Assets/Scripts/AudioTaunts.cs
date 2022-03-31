using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTaunts : MonoBehaviour
{
    public AudioClip[] starSound;
    public int[] starCount;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<MusicManager>().GetComponent<AudioSource>().volume = 1f;
        this.enabled = false;
        for (int i = 0; i < starCount.Length; i++) {
            if (PlayerPrefs.GetInt("ShardCount") >= starCount[i])
            {
                if (PlayerPrefs.GetInt("PlayedSound" + i) == 0)
                {
                    GetComponent<AudioSource>().PlayOneShot(starSound[i]);
                    PlayerPrefs.SetInt("PlayedSound" + i, 2);
                    PlayerPrefs.Save();
                    this.enabled = true;
                    return;
                }
                
            }
            }
    }
    private void Update()
    {
        if (GetComponent<AudioSource>().isPlaying)
        {
            FindObjectOfType<MusicManager>().GetComponent<AudioSource>().volume = 0.1f;
        }
        else
        {
            FindObjectOfType<MusicManager>().GetComponent<AudioSource>().volume = 1f;
            this.enabled = false;
        }
    }

}
