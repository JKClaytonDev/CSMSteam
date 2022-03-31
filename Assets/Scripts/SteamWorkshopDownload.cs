using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using System.IO;
using UnityEngine.UI;
public class SteamWorkshopDownload : MonoBehaviour
{
    public int nItems;
    public bool installedStuff;
    uint folderSize;
    public List<string> levels;
    public string path;

    // Start is called before the first frame update
    void Start()
    {
        if (SteamManager.Initialized)
        {
            nItems = (int)SteamUGC.GetNumSubscribedItems();
            if (nItems > 0)
            {
                PublishedFileId_t[] PublishedFileID = new PublishedFileId_t[nItems];
                uint ret = SteamUGC.GetSubscribedItems(PublishedFileID, SteamUGC.GetNumSubscribedItems());

                foreach (PublishedFileId_t i in PublishedFileID)
                {
                    installedStuff = SteamUGC.GetItemInstallInfo(i, out ulong SizeOnDisk, out string Folder, 1024, out uint punTimeStamp);
                    Debug.Log("INSTALLEDSTUFF " + installedStuff);
                    if (installedStuff)
                    {
                        
                        string[] path = Directory.GetFiles(Folder);
                        
                        for (int f = 0; f < path.Length; f++)
                        {
                            string filename = Path.GetFileName(path[f]);
                            string fullPath = path[0];
                            string destPath = Application.persistentDataPath + "/" + filename;
                            if (fullPath.Contains(".ffwt"))
                            {
                                destPath = Application.persistentDataPath + "/CustomMap" + i.m_PublishedFileId + "/" + filename;
                                levels.Add(fullPath);
                            }

                            Debug.Log(fullPath);
                            System.IO.File.Copy(fullPath, destPath, true);
                        }
                    }

                }
            }
            path = Application.persistentDataPath;

            foreach (string file in System.IO.Directory.GetFiles(path))
            {
                string tempFile = System.IO.Path.GetFileName(file);
                if (string.Equals(tempFile, "Player.log") == false && string.Equals(tempFile, "Player-prev.log") == false)
                {
                    
                    
                }
            }
        }
    }

}
