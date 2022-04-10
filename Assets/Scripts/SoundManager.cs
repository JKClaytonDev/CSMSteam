using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{
    public Slider music;
    public Slider voices;
    public Slider master;
    public AudioSource a;
    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.realtimeSinceStartup;
        init();
    }

    public void init()
    {
        if (a)
            a.volume = PlayerPrefs.GetFloat("MusicVolume") / 100;
        if (master)
            master.value = PlayerPrefs.GetFloat("vol");
        if (music)
            music.value = PlayerPrefs.GetFloat("MusicVolume");
        if (voices)
            voices.value = PlayerPrefs.GetFloat("VoiceVolume");
        PlayerPrefs.Save();
        FindObjectOfType<PlayerVoices>().updateSounds();
    }
    public void setMusicVolume(float f)
    {
        if (f == 100 || Time.realtimeSinceStartup < startTime + 1)
            return;
        Debug.Log("MUSIC VALUE CHANGE");
        PlayerPrefs.SetFloat("MusicVolume", f);
        PlayerPrefs.Save();
        if (a)
        {
            a.volume = PlayerPrefs.GetFloat("MusicVolume") / 100;
        }
        FindObjectOfType<PlayerVoices>().updateSounds();
    }
    public void setVoiceVolume(float k)
    {

        if (k == 100 || Time.realtimeSinceStartup < startTime + 1)
            return;
        Debug.Log("MUSIC VALUE CHANGE");
        PlayerPrefs.SetFloat("VoiceVolume", k);
        PlayerPrefs.Save();
        FindObjectOfType<PlayerVoices>().updateSounds();
    }
    // Update is called once per frame

}
