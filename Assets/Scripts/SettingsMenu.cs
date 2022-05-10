using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SettingsMenu : MonoBehaviour
{
    public GameObject player;
    public GameObject settings;
    bool toggle = false;
    Canvas wepCanvas;
    public Canvas MoneyCanvas;
    public Canvas crosshairCanvas;

    private void Start()
    {
        transform.parent = null;
        wepCanvas = GameObject.Find("WeaponsCanvas").GetComponent<Canvas>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
        settings.SetActive(false);
        transform.position = player.transform.position + transform.up * 5;
        transform.rotation = player.transform.rotation;
    }
    public void loadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    // Update is called once per frame
    public void resume()
    {
        if (!player)
        {
            gameObject.SetActive(false);
        }
        toggle = !toggle;
        if (toggle)
        {
            Time.timeScale = 0;
            if (wepCanvas)
            wepCanvas.enabled = false;
            settings.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            if (MoneyCanvas)
                MoneyCanvas.enabled = false;
            if (crosshairCanvas)
                crosshairCanvas.enabled = false;
        }
        else
        {
            Time.timeScale = 1;
            if (wepCanvas)
                wepCanvas.enabled = true;
            Time.timeScale = 1;
            player.SetActive(true);
            settings.SetActive(false);
            Cursor.visible = false;
            if (MoneyCanvas)
                MoneyCanvas.enabled = true;
            if (crosshairCanvas)
                crosshairCanvas.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            resume();
        }
        if (toggle)
            Time.timeScale = 0;
    }
}
