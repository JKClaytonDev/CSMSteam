using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{
    public Slider music;
    public Slider voices;
    public AudioSource a;
    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    public void init()
    {
        if (a)
        {
            a.volume = PlayerPrefs.GetFloat("MusicVolume") / 100;
        }
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 100);
            Debug.Log("NO KEY");
        }
        if (!PlayerPrefs.HasKey("VoiceVolume")) {
            PlayerPrefs.SetFloat("VoiceVolume", 100);
            Debug.Log("NO KEY");
        }
        music.value = PlayerPrefs.GetFloat("MusicVolume");
        voices.value = PlayerPrefs.GetFloat("VoiceVolume");
        PlayerPrefs.Save();
        FindObjectOfType<PlayerVoices>().updateSounds();
    }
    public void setMusicVolume(float f)
    {
        if (f == 100)
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
        if (k == 100)
            return;
        Debug.Log("MUSIC VALUE CHANGE");
        PlayerPrefs.SetFloat("VoiceVolume", k);
        PlayerPrefs.Save();
        FindObjectOfType<PlayerVoices>().updateSounds();
    }
    // Update is called once per frame

}
