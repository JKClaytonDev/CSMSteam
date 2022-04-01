using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EscapeMenu : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(PlayerPrefs.GetString("FlashlightKeybind")))
        {
            SceneManager.LoadScene("Menu");
            this.enabled = false;
        }
    }
}
