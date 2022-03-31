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
        if (SceneManager.GetActiveScene().name.Contains("Menu"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (PlayerPrefs.GetFloat("vol") == 0)//volume
            PlayerPrefs.SetFloat("vol", 1);
        AudioListener.volume = PlayerPrefs.GetFloat("vol");
        menu.name = "asdfasd fasdfasdfasdfasdfsda fasdf sadf asdf sadf asdfsad f";
        menu.SetActive(false);
        active = false;

    }
    public void loadScene(string s)
    {
        SceneManager.LoadScene(s);
    }
    public void setSens(System.Single value)
    {
        PlayerPrefs.SetFloat("Mouse", value);
    }
    public void setVol(System.Single value)
    {
        PlayerPrefs.SetFloat("vol", value);
        AudioListener.volume = value;
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //SceneManager.LoadScene("ClassicMenu");
        }
    }
}
