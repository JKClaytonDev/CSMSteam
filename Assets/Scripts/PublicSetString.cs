using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PublicSetString : MonoBehaviour
{
    public Text t;
    public string s;
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = GetComponent<RectTransform>().localPosition;
        pos.x = 750;
        pos.z = 0;
        GetComponent<RectTransform>().localPosition = pos;
        t.text = s;
    }
}
