using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetKeyBinding : MonoBehaviour
{
    public string bind;
    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        GetComponent<InputField>().text = PlayerPrefs.GetString(bind);
    }
}
