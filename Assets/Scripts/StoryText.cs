using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StoryText : MonoBehaviour
{
    public Text WorldTitle;
    public Text CurrentDate;
    public RawImage background;
    public string[] titles;
    public Texture[] pictures;
    //public Text LevelTitle;
    // Start is called before the first frame update
    void Start()
    {
        WorldTitle.text = PlayerPrefs.GetString("MenuWorldTitle");
        CurrentDate.text = PlayerPrefs.GetString("CurrentDate");
        //PlayerPrefs.GetString("MenuLevelTitle");
        for (int i = 0; i<titles.Length; i++)
        {
            string s = titles[i];
            if (s == WorldTitle.text)
            {
                background.texture = pictures[i];
            }
        }
    }

}
