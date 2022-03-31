using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SetHub : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        if (PlayerPrefs.GetInt("Missions") != 1){ PlayerPrefs.SetString("HubScene", SceneManager.GetActiveScene().name); PlayerPrefs.Save();}
    }

}
