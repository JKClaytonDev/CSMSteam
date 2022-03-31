using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuButtons : MonoBehaviour
{
    public AudioSource sound;
    public GameObject cont;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 100);
            Debug.Log("NO KEY");
        }
        if (!PlayerPrefs.HasKey("VoiceVolume"))
        {
            PlayerPrefs.SetFloat("VoiceVolume", 100);
            Debug.Log("NO KEY");
        }
        if (sound)
        {
            sound.volume = PlayerPrefs.GetFloat("MusicVolume") / 100;
        }
        PlayerPrefs.SetInt("Missions", 0);
        PlayerPrefs.Save();
        if (!PlayerPrefs.HasKey("MouseSens"))
            PlayerPrefs.SetFloat("MouseSens", 15);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (cont)
        cont.SetActive(true);
    }
    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void newGame()
    {
        
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Lives", 5);
        if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}
        SceneManager.LoadScene("IntroAnim");
    }
    public void masterGame()
    {

        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Lives", 5);
        PlayerPrefs.SetInt("MasterQuest", 1);
        if (PlayerPrefs.GetInt("Missions") != 1) { PlayerPrefs.Save(); }
        SceneManager.LoadScene("IntroAnim");
    }
    public void continueGame()
    {
        if (PlayerPrefs.HasKey("LockScene"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("LockScene"));
        }
    }
    public void quitGame()
    {
        Application.Quit();
    }
    
}
