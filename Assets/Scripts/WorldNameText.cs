using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WorldNameText : MonoBehaviour
{
    float textTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().enabled = Time.realtimeSinceStartup < textTime;
    }
    public void setText(string text)
    {
        GetComponent<Text>().text = text;
        textTime = Time.realtimeSinceStartup + 0.1f;
    }
}
