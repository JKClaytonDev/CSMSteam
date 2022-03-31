using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadIntoMap : MonoBehaviour
{
    public string Directory;
    // Start is called before the first frame update
    public void SelectMap()
    {
        PlayerPrefs.SetInt("Missions", 1);
        PlayerPrefs.SetString("CustomMapDirectory", Directory);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MapEditor");
    }
    public void PlayMap()
    {
        PlayerPrefs.SetInt("Missions", 1);
        PlayerPrefs.SetString("CustomMapDirectory", Directory);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MapPlayer");
    }
}
