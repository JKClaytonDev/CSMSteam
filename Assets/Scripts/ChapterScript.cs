using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChapterScript : MonoBehaviour
{
    public Text t;
    public Text baseLabel;
    public string lastString;
    ButtonShardSelect b;
    public string scene;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Dropdown>().options[1].text = "ASDF";
        b = FindObjectOfType<ButtonShardSelect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (t.text != lastString)
        {
            
            lastString = t.text;
            GetComponent<Dropdown>().options[0].text = "N/A";
            GetComponent<Dropdown>().options[1].text = "N/A";
            GetComponent<Dropdown>().options[2].text = "N/A";
            GetComponent<Dropdown>().options[3].text = "N/A";
            GetComponent<Dropdown>().options[0].text = b.levelTitles[System.Array.IndexOf(b.levelNames, lastString+"1")];
            baseLabel.text = GetComponent<Dropdown>().options[0].text;
            GetComponent<Dropdown>().options[1].text = b.levelTitles[System.Array.IndexOf(b.levelNames, lastString + "2")];
            GetComponent<Dropdown>().options[2].text = b.levelTitles[System.Array.IndexOf(b.levelNames, lastString + "3")];
            GetComponent<Dropdown>().options[3].text = b.levelTitles[System.Array.IndexOf(b.levelNames, lastString + "4")];
            
        }
    }
    public string getSceneName()
    {
        return b.levelNames[System.Array.IndexOf(b.levelTitles, baseLabel.text)];
    }
}
