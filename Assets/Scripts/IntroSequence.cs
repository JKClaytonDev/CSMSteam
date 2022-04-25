using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using Steamworks;
public class IntroSequence : MonoBehaviour
{

    bool play = false;
    private void Start()
    {
        DeleteSteamAppID();
    }

    private void DeleteSteamAppID()
    {
        // If we are actually testing the game it is OK to have a steam_appid.txt, 
        // then do nothing
        if (Application.isEditor) return;

        if (File.Exists("steam_appid.txt"))
        {
            try
            {
                File.Delete("steam_appid.txt");
            }
            catch { Debug.Log("ERROR"); }
        }
            if (File.Exists("steam_appid.txt"))
            {
                Debug.LogError("Cannot delete steam_appid.txt. Quitting...");
                Application.Quit();
            }

            bool m_bInitialized = SteamAPI.Init();
            if (!m_bInitialized)
            {
                Debug.LogError("[Steamworks.NET] SteamAPI_Init() failed. Refer to Valve's documentation or the comment above this line for more information.", this);

                return;
            }
            m_bInitialized = SteamAPI.Init();
            if (!m_bInitialized)
            {
                Debug.LogError("[Steamworks.NET] SteamAPI_Init() failed. Refer to Valve's documentation or the comment above this line for more information.", this);

                Application.Quit();

                return;
            }
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<VideoPlayer>().isPlaying)
            play = true;
        if (play && !GetComponent<VideoPlayer>().isPlaying) {
            SceneManager.LoadScene("Menu");
            this.enabled = false;
        }
    }
}
