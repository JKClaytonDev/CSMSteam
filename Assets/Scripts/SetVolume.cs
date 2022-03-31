using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SetVolume : MonoBehaviour
{
    public float Volume;
    public AudioClip clip;
    // Start is called before the first frame update
    void Awake()
    {
        transform.parent = FindObjectOfType<PlayerVoices>().gameObject.transform;
        transform.localPosition = new Vector3();
        if (PlayerPrefs.GetInt("Missions") == 0)
        {
            if (PlayerPrefs.GetInt("PlayedIntro" + SceneManager.GetActiveScene().name) == 0)
            {
                GetComponent<AudioSource>().PlayOneShot(clip, Volume * PlayerPrefs.GetFloat("VoiceVolume") / 100);
                PlayerPrefs.SetInt("PlayedIntro" + SceneManager.GetActiveScene().name, 1);
            }
        }
    }

}
