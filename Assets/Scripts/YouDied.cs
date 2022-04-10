using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class YouDied : MonoBehaviour
{
    public Canvas attached;
    public GameObject sceneImage;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = Random.insideUnitSphere;
        GetComponent<Rigidbody>().AddTorque(Random.insideUnitSphere);
    }
    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("HardcoreMode") == 1)
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
            SceneManager.LoadScene("Menu");
        }
        if (SceneManager.GetActiveScene().name == "FinalCastle1")
            SceneManager.LoadScene("Hub1");
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c!= attached)
            {
                c.enabled = false;
            }
        }
        foreach (billboardOBJ k in FindObjectsOfType<billboardOBJ>())
            k.enabled = false;
        foreach (enemyHealth k in FindObjectsOfType<enemyHealth>())
            k.gameObject.SetActive(false);
        if (PlayerPrefs.GetInt("Mission") == 0)
        {
            PlayerPrefs.SetInt("Lives", PlayerPrefs.GetInt("Lives") - 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Lives") == 0)
        {
            PlayerPrefs.SetInt("Lives", 5);
            if (PlayerPrefs.GetInt("Missions") != 1)
            {
                PlayerPrefs.Save();
                SceneManager.LoadScene("GameOver");
            }

        }
        else if (Input.GetKey(KeyCode.R))
        {
            if (PlayerPrefs.GetInt("Mission") == 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                sceneImage.SetActive(true);
                this.enabled = false;
            }
            if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}
            if (PlayerPrefs.GetInt("Lives") <= 0)
            {
                PlayerPrefs.SetInt("Lives", 5);
                if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();
                    SceneManager.LoadScene("GameOver");
                }
                
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            sceneImage.SetActive(true);
            this.enabled = false;
        }
    }
}
