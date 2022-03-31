using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSelectScene : MonoBehaviour
{
    public string SceneName;
    public GameObject image;
    public LevelSelectScene[] enableOnComplete;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (PlayerPrefs.GetInt(SceneName) == 1)
        {
            image.SetActive(false);
            foreach (LevelSelectScene s in enableOnComplete)
                s.gameObject.SetActive(true);
        }
    }

    public void loadLevel()
    {
        SceneManager.LoadScene(SceneName);
        gameObject.SetActive(false);
    }

}
