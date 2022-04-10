using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBindings : MonoBehaviour
{
    string inputName;
    public void setInputName(string inp)
    {
        inputName = inp;
    }
    public void SetKeyBinding(string inp)
    {
        try
        {
            Input.GetKey(inp.ToLower());
            PlayerPrefs.SetString(inputName, inp.ToLower());
        }
        catch
        {
            return;
        }
        
    }
    public void resetKeys()
    {
        PlayerPrefs.SetString("JumpKeybind", "space");
        PlayerPrefs.SetString("RunKeybind", "left shift");
        PlayerPrefs.SetString("FlashlightKeybind", "e");
        PlayerPrefs.SetString("GhostKeybind", "f");
        PlayerPrefs.SetString("WhipKeybind", "v");
        PlayerPrefs.SetString("MeleeKeybind", "mouse 1");
        foreach (SetKeyBinding e in FindObjectsOfType<SetKeyBinding>())
            e.Reset();
    }
}
