using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
public class SetAchievement : MonoBehaviour
{
    public string check;

    private void OnEnable()
    {
        bool tutorialCompleted;
        Steamworks.SteamUserStats.GetAchievement(check, out tutorialCompleted);
        if (tutorialCompleted == false)
        {
            Steamworks.SteamUserStats.SetAchievement(check);
        }
    }
}
