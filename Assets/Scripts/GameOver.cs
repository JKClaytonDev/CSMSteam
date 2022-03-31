using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Missions") == 1)
        {
            PlayerPrefs.SetInt("Lives", 5);
            PlayerPrefs.Save();
            SceneManager.LoadScene("WorldSelect");
            this.enabled = false;
            return;
        }
        if (Input.GetKey(KeyCode.R))
        {
            {
                PlayerPrefs.DeleteKey("SpawnX");
                PlayerPrefs.DeleteKey("SpawnY");
                PlayerPrefs.DeleteKey("SpawnZ");
                SceneManager.LoadScene(PlayerPrefs.GetString("HubScene"));
                this.enabled = false;
            }
        }
    }
}
