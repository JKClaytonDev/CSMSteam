using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class MusicManager : MonoBehaviour
{
    public string[] sceneNames;
    public AudioClip[] sceneMusic;
    public AudioClip[] tracks;
    NightClock night;
    AudioSource source;
    AudioSource nightSource;
    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        
        if (FindObjectOfType<MusicPriority>())
        {
            this.enabled = false;
            GetComponent<AudioSource>().enabled = false;
        }

        startTime = Time.realtimeSinceStartup + 2;
        source = GetComponent<AudioSource>();
        source.volume = 1.5f * 1.75f * PlayerPrefs.GetFloat("MusicVolume") / 100 ;
        int trackNum = SceneManager.GetActiveScene().buildIndex;
        string name = SceneManager.GetActiveScene().name;
        while (trackNum > tracks.Length - 1)
            trackNum -= tracks.Length;
        AudioClip soundClip = tracks[trackNum];
        if (FindObjectOfType<CustomMusic>())
            soundClip = FindObjectOfType<CustomMusic>().track;
        for (int i = 0; i<sceneNames.Length; i++)
        {
            if (name == sceneNames[i])
                soundClip = sceneMusic[i];
        }
        GetComponent<AudioSource>().clip = soundClip;
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().loop = true;
        source.volume = 1.5f * 1.75f * PlayerPrefs.GetFloat("MusicVolume") / 100;
    }


}
