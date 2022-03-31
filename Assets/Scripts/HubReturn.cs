using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HubReturn : MonoBehaviour
{
    private void OnEnable()
    {
        bool finished = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name) == 1;
        bool same = SceneManager.GetActiveScene().name != PlayerPrefs.GetString("HubScene");
        gameObject.SetActive(finished && same && PlayerPrefs.GetInt("Missions") != 1);
    }
}
