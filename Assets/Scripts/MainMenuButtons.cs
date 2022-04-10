using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuButtons : MonoBehaviour
{
    public AudioSource sound;
    public GameObject cont;
    public Toggle Classic;
    public Toggle Hardcore;
    public Toggle BossRush;
    public Toggle Mirrors;
    public Toggle RandomMonsters;
    // Start is called before the first frame update
    void Start()
    {
        Classic.isOn = false;
        Hardcore.isOn = false;
        BossRush.isOn = false;
        Mirrors.isOn = false;
        RandomMonsters.isOn = false;
        if (!PlayerPrefs.HasKey("vol"))
        {
            PlayerPrefs.SetFloat("vol", 100);
            Debug.Log("NO KEY");
        }
        AudioListener.volume = PlayerPrefs.GetFloat("vol") / 100;
        Debug.Log("GET VOLUME " + PlayerPrefs.GetFloat("vol"));
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
        if (!PlayerPrefs.HasKey("FOV"))
            PlayerPrefs.SetFloat("FOV", 90);
        if (!PlayerPrefs.HasKey("MeleeKeybind"))
        {
            PlayerPrefs.SetString("JumpKeybind", "space");
            PlayerPrefs.SetString("RunKeybind", "left shift");
            PlayerPrefs.SetString("FlashlightKeybind", "e");
            PlayerPrefs.SetString("GhostKeybind", "f");
            PlayerPrefs.SetString("WhipKeybind", "v");
            PlayerPrefs.SetString("MeleeKeybind", "mouse 1");
        }
        if (!PlayerPrefs.HasKey("ToggleRun"))
        {
            PlayerPrefs.SetInt("ToggleRun", 0);
            PlayerPrefs.SetInt("ShiftWalk", 0);
        }
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
    public void resetAll()
    {
        string oldRunKeybind = PlayerPrefs.GetString("RunKeybind");
        string oldJumpKeybind = PlayerPrefs.GetString("JumpKeybind");
        string oldFlashlightKeybind = PlayerPrefs.GetString("FlashlightKeybind");
        string oldGhostKeybind = PlayerPrefs.GetString("GhostKeybind");
        string oldWhipKeybind = PlayerPrefs.GetString("WhipKeybind");
        string oldMeleeKeybind = PlayerPrefs.GetString("MeleeKeybind");
        float oldmusicvol = PlayerPrefs.GetFloat("MusicVolume");
        float oldvoicevol = PlayerPrefs.GetFloat("VoiceVolume");
        float oldvol = PlayerPrefs.GetFloat("vol");
        float oldFOV = PlayerPrefs.GetFloat("FOV");
        int toggleRun = PlayerPrefs.GetInt("ToggleRun");
        int shiftWalk = PlayerPrefs.GetInt("ShiftWalk");

        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Lives", 5);

        PlayerPrefs.SetString("RunKeybind", oldRunKeybind);
        PlayerPrefs.SetString("JumpKeybind", oldJumpKeybind);
        PlayerPrefs.SetString("FlashlightKeybind", oldFlashlightKeybind);
        PlayerPrefs.SetString("GhostKeybind", oldGhostKeybind);
        PlayerPrefs.SetString("WhipKeybind", oldWhipKeybind);
        PlayerPrefs.SetString("MeleeKeybind", oldMeleeKeybind);
        PlayerPrefs.SetFloat("FOV", oldFOV);
        PlayerPrefs.SetFloat("MusicVolume", oldmusicvol);
        PlayerPrefs.SetFloat("VoiceVolume", oldvoicevol);
        PlayerPrefs.SetFloat("vol", oldvol);
        PlayerPrefs.SetInt("ToggleRun", toggleRun);
        PlayerPrefs.SetInt("ShiftWalk", shiftWalk);
        if (PlayerPrefs.GetInt("Missions") != 1) { PlayerPrefs.Save(); }
        
    }
    public void newGame()
    {
        resetAll();
        PlayerPrefs.Save();
        if (Classic.isOn)
        {
            PlayerPrefs.SetInt("ClassicMode", 1);
        }
        if (Hardcore.isOn)
        {
            PlayerPrefs.SetInt("HardcoreMode", 1);
        }
        if (RandomMonsters.isOn)
        {
            PlayerPrefs.SetInt("RandomMonsters", 1);
        }
        if (Mirrors.isOn)
        {
            PlayerPrefs.SetInt("MasterQuest", 1);
        }
        if (BossRush.isOn)
        {
            PlayerPrefs.SetInt("Lives", 5);
            PlayerPrefs.SetInt("Missions", 1);
            PlayerPrefs.SetInt("BossRush", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Castle3");
        }
        else
        {
            SceneManager.LoadScene("IntroAnim");
        }
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
