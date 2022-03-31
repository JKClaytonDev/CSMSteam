using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MissionSelectScene : MonoBehaviour
{
    public Text t;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void loadScene()
    {
        PlayerPrefs.SetInt("Missions", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(FindObjectOfType<ChapterScript>().getSceneName());
    }
    
}
