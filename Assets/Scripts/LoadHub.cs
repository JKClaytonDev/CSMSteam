using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadHub : MonoBehaviour
{
   public void loadHubScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("HubScene"));
    }
}
