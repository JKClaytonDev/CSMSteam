using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class hintText : MonoBehaviour
{
    public void SetText(string s)
    {
        GetComponent<Text>().text = s;
    }
}
