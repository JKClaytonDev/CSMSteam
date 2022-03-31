using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class lifeCounter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Missions") == 1)
            PlayerPrefs.SetInt("Lives", 3);

        GetComponent<Text>().text = "x" + PlayerPrefs.GetInt("Lives");
        
        if (PlayerPrefs.GetInt("Lives") <= 0)
        {
            PlayerPrefs.SetInt("Lives", 5);
            if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}
            if (PlayerPrefs.GetInt("Mission") != 1)
                SceneManager.LoadScene("GameOver");
        }
    }
    public void checkLives()
    {
        if (PlayerPrefs.GetInt("Missions") == 1)
            PlayerPrefs.SetInt("Lives", 3);

        GetComponent<Text>().text = "x" + PlayerPrefs.GetInt("Lives");

        if (PlayerPrefs.GetInt("Lives") <= 0)
        {
            PlayerPrefs.SetInt("Lives", 5);
            if (PlayerPrefs.GetInt("Missions") != 1) { PlayerPrefs.Save(); }
            if (PlayerPrefs.GetInt("Mission") != 1)
                SceneManager.LoadScene("GameOver");
        }
    }
    
}
