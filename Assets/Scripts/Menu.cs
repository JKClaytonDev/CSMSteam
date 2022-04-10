using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public bool active;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("MeleeKeybind"))
        {
            PlayerPrefs.SetString("JumpKeybind", "space");
            PlayerPrefs.SetString("RunKeybind", "left shift");
            PlayerPrefs.SetString("FlashlightKeybind", "e");
            PlayerPrefs.SetString("GhostKeybind", "f");
            PlayerPrefs.SetString("WhipKeybind", "v");
            PlayerPrefs.SetString("MeleeKeybind", "mouse 1");
        }
        if (SceneManager.GetActiveScene().name.Contains("Menu"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (!PlayerPrefs.HasKey("vol"))//volume
            PlayerPrefs.SetFloat("vol", 1);
        menu.name = "asdfasd fasdfasdfasdfasdfsda fasdf sadf asdf sadf asdfsad f";
        menu.SetActive(false);
        active = false;

    }
    public void loadScene(string s)
    {
        SceneManager.LoadScene(s);
    }
    public void setSens(float value)
    {
        PlayerPrefs.SetFloat("Mouse", value);
    }
    public void setVol(float value)
    {
        PlayerPrefs.SetFloat("vol", value);
    }
    public void fullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    public void quit()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
    }
}
