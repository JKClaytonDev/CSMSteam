using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartScreenLocation : MonoBehaviour
{
    public string scene;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Missions") != 1)
        {
            PlayerPrefs.SetString("CurrentDate", System.DateTime.Now.ToString("U"));
            PlayerPrefs.SetString("MenuWorldTitle", scene);
            PlayerPrefs.Save();
            FindObjectOfType<PlayerMovement>().checkTitle();
        }
    }

}
