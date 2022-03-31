using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonShardSelect : MonoBehaviour
{
    public Image image;
    public Text t2;
    public GameObject i;
    public Image[] worldImages;
    public string[] levelNames;
    public string[] levelTitles;

    public string[] worldNames;
    public string[] worldTitles;
    int setWorld;

    public Text t;
    private void Start()
    {
        
        for (int i = 0; i<worldNames.Length; i++)
        {
            if (worldNames[i] == PlayerPrefs.GetString("CurrentWorld"))
            {
                t.text = worldTitles[i];
                PlayerPrefs.SetString("CurrentDate", System.DateTime.Now.ToString("U"));
                PlayerPrefs.SetString("MenuWorldTitle", t.text);
            }
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public void loadScene(string index)
    {
        i.SetActive(true);
        
        PlayerPrefs.Save();
        SceneManager.LoadScene(PlayerPrefs.GetString("CurrentWorld") + index);
    }
}
