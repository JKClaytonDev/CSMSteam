using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerDoll : MonoBehaviour
{
    float speakTextLength;

    public AudioClip villagerSound;
    public AudioClip protoSound;
    public AudioClip playerSound;
    public AudioClip dollSound;

    string speakMessage;
    float talkMessageTime;
    string targetText;
    public AudioClip sound;
    int set;
    public GameObject SpeakText;
    public bool alwaysSpeak;
    Animator anim;
    public bool hideDoll;
    public GameObject doll;
    int textIndex = 0;
    public Sprite dollSprite;
    public Image changeImage;
    public Text changeText;
    public Sprite[] imageIndexes;
    public string[] textIndexes;
    public AudioClip[] sounds;
    void OnEnable()
    {
        speakMessage = "";
        if (GetComponent<AudioSource>())
        GetComponent<AudioSource>().PlayOneShot(sound);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!SpeakText)
            SpeakText = new GameObject();
        textIndex = 0;
        doll.SetActive(!hideDoll);

        if (!alwaysSpeak && PlayerPrefs.GetInt("DollSpokenLine" + textIndexes[0] + textIndexes[textIndexes.Length-1]) == 1)
        {
            FindObjectOfType<PlayerMovement>().dollActive = false;
            Destroy(gameObject);
            Time.timeScale = 1;
            return;
        }

        PlayerPrefs.SetInt("DollSpokenLine" + textIndexes[0] + textIndexes[textIndexes.Length - 1], 1);
        if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}
        anim = GetComponent<Animator>();
        FindObjectOfType<PlayerMovement>().dollActive = true;
        anim.Play("PullDoll");
        if (FindObjectOfType<MusicManager>())
        {
            FindObjectOfType<MusicManager>().GetComponent<AudioSource>().volume = 0.3f * PlayerPrefs.GetFloat("MusicVolume") / 100;
        }

        if (sounds[textIndex] && GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().Stop();
            if (SpeakText)
                SpeakText.SetActive(false);
            speakTextLength = sounds[textIndex].length / textIndexes[textIndex].Length/4;
            GetComponent<AudioSource>().PlayOneShot(sounds[textIndex], 3);
        }
    }

    // Update is called once per frame
    void Update()
    {

        
        if (textIndex >= imageIndexes.Length || PlayerPrefs.GetInt("Missions") == 1 || PlayerPrefs.GetInt("BossRush") == 1)
        {
            if (FindObjectOfType<MusicManager>())
            {
                FindObjectOfType<MusicManager>().GetComponent<AudioSource>().volume = 1.5f * PlayerPrefs.GetFloat("MusicVolume") / 100;
            }
            FindObjectOfType<PlayerMovement>().dollActive = false;
            Destroy(gameObject);
            Time.timeScale = 1;
        }

        if (((Input.GetKeyDown(KeyCode.Space) && textIndex >= sounds.Length) || (textIndex < sounds.Length && !GetComponent<AudioSource>().isPlaying)) && !GetComponent<AudioSource>().isPlaying)
        {
            textIndex++;
            targetText = textIndexes[textIndex];
            speakMessage = "";
            if (imageIndexes[textIndex] == dollSprite)
                anim.Play("DollTalk");
            if (GetComponent<AudioSource>() && !sounds[textIndex])
                GetComponent<AudioSource>().PlayOneShot(sound);
            speakTextLength = 0.01f;
            if (sounds.Length == textIndexes.Length)
            {
                if (sounds[textIndex] && GetComponent<AudioSource>())
                {
                    GetComponent<AudioSource>().Stop();
                    if (SpeakText)
                        SpeakText.SetActive(false);
                    speakTextLength = (sounds[textIndex].length/textIndexes[textIndex].Length)/2;
                    GetComponent<AudioSource>().PlayOneShot(sounds[textIndex], 3);
                }
            }

        }

        changeImage.sprite = imageIndexes[textIndex];
        changeText.text = speakMessage;

        if (Time.realtimeSinceStartup > talkMessageTime && speakMessage != textIndexes[textIndex])
        {
            speakMessage = textIndexes[textIndex].Substring(0, speakMessage.Length + 1);
            talkMessageTime = Time.realtimeSinceStartup + speakTextLength;
            float volume = 0.5f;
            if (set % 8 == 0)
            {
                if (imageIndexes[textIndex].name.Contains("Doll"))
                    GetComponent<AudioSource>().PlayOneShot(dollSound, volume);
                else if (imageIndexes[textIndex].name.Contains("Player"))
                    GetComponent<AudioSource>().PlayOneShot(playerSound, volume);
                else if (imageIndexes[textIndex].name.Contains("Proto"))
                    GetComponent<AudioSource>().PlayOneShot(protoSound, volume);
                else if (imageIndexes[textIndex].name.Contains("Villag"))
                    GetComponent<AudioSource>().PlayOneShot(villagerSound, volume);
            }
            set++;
        }
    }
}
