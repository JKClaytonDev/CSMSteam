using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
public class DynamicUpscaling : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Toggle>().isOn = PlayerPrefs.GetInt("DisableDynamic") != 1;
    }

    public void swapToggle()
    {
        if (GetComponent<Toggle>().isOn)
            PlayerPrefs.SetInt("DisableDynamic", 0);
        else
            PlayerPrefs.SetInt("DisableDynamic", 1);
        Camera c = Camera.main;
        if (FindObjectOfType<TextureModManager>())
            c = FindObjectOfType<TextureModManager>().playerCam;
        c.gameObject.GetComponent<HDAdditionalCameraData>().allowDynamicResolution = PlayerPrefs.GetInt("DisableDynamic") != 1;
        PlayerPrefs.Save();
    }
}
