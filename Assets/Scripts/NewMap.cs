using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
public class NewMap : MonoBehaviour
{
    public Text title;
    public Text creator;
    public void newMap()
    {
        string baseDir = Application.dataPath + "\\Addons\\Maps\\";
        baseDir = baseDir + "\\" + title.text;
        Directory.CreateDirectory(baseDir);
        File.WriteAllText(baseDir + "\\info.txt", creator.text);
        string finalString = "Castillo Custom Map Format\n\ngeometry\n{\n";
        finalString += "\n}";
        File.WriteAllText(baseDir + "\\mapdata.ffwt", finalString);
        PlayerPrefs.SetString("CustomMapDirectory", baseDir);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MapEditor");
    }
}
