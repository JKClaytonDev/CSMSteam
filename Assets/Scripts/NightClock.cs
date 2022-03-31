using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightClock : MonoBehaviour
{
    public GameObject sunset;
    public GameObject dark;
    public GameObject sunrise;
    public AudioClip track;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Start()
    {
        if (PlayerPrefs.GetInt("ShardCount") < 7)
            this.gameObject.SetActive(false);
    }
    void Update()
    {
        if ((sunset.activeInHierarchy || dark.activeInHierarchy) && !GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().PlayOneShot(track);
        GetComponent<AudioSource>().volume = 0;
        if (sunset.activeInHierarchy || dark.activeInHierarchy)
        GetComponent<AudioSource>().volume = 1;
        sunset.SetActive(System.DateTime.Now.Second < 10);
        dark.SetActive(!(System.DateTime.Now.Second < 10 || System.DateTime.Now.Second > 55));
        sunrise.gameObject.SetActive(System.DateTime.Now.Second > 55);
    }
}
