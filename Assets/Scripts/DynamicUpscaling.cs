using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.SceneManagement;
public class DynamicUpscaling : MonoBehaviour
{
    bool start = true;
    public Toggle MotionBlur;
    public Toggle Lighting;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("BLUR IS " + PlayerPrefs.GetInt("MotionBlur"));
        GetComponent<Toggle>().isOn = PlayerPrefs.GetInt("DisableDynamic") != 1;
        MotionBlur.isOn = PlayerPrefs.GetInt("MotionBlur") == 1;
        Lighting.isOn = PlayerPrefs.GetInt("DisableLighting") != 1;
        start = false;
    }
    public void restartToggleLight(bool a)
    {
        if (start)
            return;
        string s = "DisableLighting";
        if (a)
            PlayerPrefs.SetInt(s, 0);
        else
            PlayerPrefs.SetInt(s, 1);
        PlayerPrefs.Save();
    }
    public void restartScene()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void restartToggleBlur(bool a)
    {
        if (start)
            return;
        string s = "MotionBlur";
        if (a)
            PlayerPrefs.SetInt(s, 1);
        else
            PlayerPrefs.SetInt(s, 0);
        //Debug.Log("BLUR IS " + PlayerPrefs.GetInt("MotionBlur"));
        PlayerPrefs.Save();
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
