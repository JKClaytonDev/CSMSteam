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
        PlayerPrefs.SetString(inputName, inp.ToLower());
    }
    public void resetKeys()
    {
        PlayerPrefs.SetString("JumpKeybind", "space");
        PlayerPrefs.SetString("RunKeybind", "left shift");
        PlayerPrefs.SetString("FlashlightKeybind", "e");
        PlayerPrefs.SetString("GhostKeybind", "f");
        PlayerPrefs.SetString("WhipKeybind", "v");
        foreach (SetKeyBinding e in FindObjectsOfType<SetKeyBinding>())
            e.Reset();
    }
}
