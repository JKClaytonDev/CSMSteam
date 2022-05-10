using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
public class SetAchievement : MonoBehaviour
{
    public string check;

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("Missions") != 1 && PlayerPrefs.GetInt(check) != 1)
        {
            PlayerPrefs.SetInt(check, 1);
            bool tutorialCompleted;
            Steamworks.SteamUserStats.GetAchievement(check, out tutorialCompleted);
            if (tutorialCompleted == false)
            {
                Steamworks.SteamUserStats.SetAchievement(check);
                Steamworks.SteamUserStats.RequestCurrentStats();
            }
        }
    }
}
