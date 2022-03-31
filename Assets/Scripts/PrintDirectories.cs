using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class PrintDirectories : MonoBehaviour
{
    public Dropdown Content;
    public Text title;
    public Text description;
    public GameObject button;
    public RawImage I;
    List<string> ContentOptions;
    string baseDir;
    // Start is called before the first frame update
    void Start()
    {

        Content.ClearOptions();

        baseDir = Application.dataPath + "\\Addons\\";
        Debug.Log("BASE" + baseDir.Length);
        ContentOptions = new List<string>();

        foreach (string file in Directory.GetDirectories(@baseDir))
        {
            string newFile = file.Substring(baseDir.Length, file.Length - baseDir.Length);
            Debug.Log("FILE" + newFile.Substring(0, 4));
            if (newFile.Substring(0, 4) == "Mod_")
            {
                ContentOptions.Add(newFile.Substring(4, newFile.Length-4));
            }
            
        }
        Content.AddOptions(ContentOptions);
        SelectOption("Default");
    }
    public void OnSelect(int i)
    {
        SelectOption(ContentOptions[i]);
    }
    public void SelectOption(string ContentOption)
    {
        title.text = ContentOption;
        string fileName = baseDir + "\\Mod_" + ContentOption + "\\ModIcon.jpg";
        Debug.Log("FILE NAME " + fileName);
        byte[] bytes = File.ReadAllBytes(fileName);
        Texture2D LoadedImage = new Texture2D(2, 2);
        if (LoadedImage.LoadImage(bytes))
        {
            Debug.Log(LoadedImage.ToString());
            I.texture = LoadedImage;
        }
        string descriptionName = baseDir + "\\Mod_" + ContentOption + "\\ShortDescription.txt";
        description.text = System.Text.Encoding.UTF8.GetString(File.ReadAllBytes(descriptionName));
        PlayerPrefs.SetString("GivenDirectory", baseDir + "\\" + ContentOption);
        PlayerPrefs.Save();
    }

}
