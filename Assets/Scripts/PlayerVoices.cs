using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerVoices : MonoBehaviour
{
    public float playTime;
    public AudioClip[] foundAmmoSounds;
    public AudioClip[] gasSounds;
    public AudioClip[] killZombieSounds;
    public AudioClip[] noAmmoSounds;
    public AudioClip[] painSounds;
    public AudioClip[] pushSounds;
    public AudioClip startClip;
    public AudioClip[] buttons;
    public AudioClip[] ghosts;
    public float multiplier;
    public void updateSounds()
    {
        Debug.Log("INIT MUSIC VOLUME " + PlayerPrefs.GetFloat("MusicVolume"));
        foreach (AudioSource a in FindObjectsOfType<AudioSource>())
        {
            if (a.clip)
            {
                if (a.clip.length > 30)
                {
                    a.volume = 0.3f * PlayerPrefs.GetFloat("MusicVolume") / 100;
                }
            }
        }
        GetComponent<AudioSource>().volume = 1.75f * PlayerPrefs.GetFloat("VoiceVolume") / 100;
    }
    private void Start()
    {
        updateSounds();
        multiplier = 1;
        if (SceneManager.GetActiveScene().name.Contains("1"))
            multiplier = 0.2f;
        if (SceneManager.GetActiveScene().name.Contains("2"))
            multiplier = 1;
        if (SceneManager.GetActiveScene().name.Contains("3"))
            multiplier = 0.8f;
        if (SceneManager.GetActiveScene().name.Contains("Boss"))
            multiplier = 0.9f;
    }
    public void FoundAmmo()
    {
        if (Random.Range(1, 100) > 100 - ((100 - 50) * multiplier))
        {
            PlayClip(foundAmmoSounds);
        }
    }
    public void GhostGotcha()
    {
        if (Random.Range(1, 100) > 100 - ((100 - 50) * multiplier))
        {
            PlayClip(ghosts);
        }
    }
    public void FoundSwitch()
    {
        if (Random.Range(1, 100) > 100 - ((100 - 25) * multiplier))
        {
            PlayClip(buttons);
        }
    }
    public void Gas()
    {
        if (Random.Range(1, 100) > 100 - ((100 - 15) * multiplier))
        {
            PlayClip(gasSounds);
        }
    }
    public void KillZombie()
    {
        if (Random.Range(1, 100) > 100 - ((100 - 95) * multiplier))
        {
            PlayClip(killZombieSounds);
            if (Time.realtimeSinceStartup > playTime)
            {
                if (PlayerPrefs.GetInt("Missions") == 1)
                    return;
                Debug.Log("CALLED FOR SOUND");
                int max = Mathf.Min(8+ PlayerPrefs.GetInt("ShardCount")/3, killZombieSounds.Length - 1);
                AudioClip clip = killZombieSounds[Random.Range(0, max)];
                playTime = Time.realtimeSinceStartup + clip.length + Random.Range(0, 3);
                GetComponent<AudioSource>().pitch = 1;
                GetComponent<AudioSource>().PlayOneShot(clip, 3);
            }
            
        }
    }
    public void NoAmmo()
    {
        if (Random.Range(1, 100) > 100-((100-25)*multiplier))
        {
            PlayClip(noAmmoSounds);
        }
    }
    public void Pain()
    {
        if (Random.Range(1, 100) > 100 - ((100 - 35) * multiplier))
        {
            PlayClip(painSounds);
        }
    }
    public void Push()
    {
        if (Random.Range(1, 100) > 100 - ((100 - 25) * multiplier))
        {
            PlayClip(pushSounds);
        }
    }
    public void PlayClip(AudioClip[] a)
    {
        if (Time.realtimeSinceStartup > playTime && !GetComponent<AudioSource>().isPlaying)
        {
            if (PlayerPrefs.GetInt("Missions") == 1)
                return;
            Debug.Log("CALLED FOR SOUND");
            AudioClip clip = a[Random.Range(0, a.Length - 1)];
            playTime = Time.realtimeSinceStartup + clip.length + Random.Range(0, 3);
            GetComponent<AudioSource>().pitch = 1;
            GetComponent<AudioSource>().PlayOneShot(clip, 5);
        }
    }
    private void Update()
    {
        if (Time.realtimeSinceStartup < playTime)
        {
            GetComponent<AudioSource>().pitch = 1;
        }
    }
}
