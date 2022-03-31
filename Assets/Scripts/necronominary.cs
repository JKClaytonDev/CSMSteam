using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class necronominary : MonoBehaviour
{
    public int page;
    public Text title;
    public Text inside;
    public Image im;

    public Sprite[] worldImages;
    public string[] textImageIndex;
    public void nextPage()
    {
        page++;
        readPage();
    }
    public void lastPage()
    {
        page--;
        if (page < 0)
            page = 0;
        readPage();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("CurrentPage"))
            page = PlayerPrefs.GetInt("CurrentPage");
        readPage();
    }

    void readPage()
    {
        
        GetComponent<Text>().text = "Necronominary: Page " + page;
        PlayerPrefs.SetInt("CurrentPage", page);
        title.text = "Page Empty";
        inside.text = "This page has not been found";
        if (PlayerPrefs.HasKey("PageText" + page))
        {
            title.text = PlayerPrefs.GetString("PageTitle" + page);
            inside.text = PlayerPrefs.GetString("PageText" + page);
        }
        if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}
        im.sprite = null;
        for (int i = 0; i < textImageIndex.Length; i++)
        {
            
            if (textImageIndex[i] == title.text)
                im.sprite = worldImages[i];
        }
    }
}
