using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParadox : MonoBehaviour
{
    public string setString;
    public int setValue;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt(setString, setValue);   
    }


}
