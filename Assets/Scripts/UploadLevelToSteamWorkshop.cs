using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using Steamworks;
using UnityEngine.UI;
using System.IO;

public class UploadLevelToSteamWorkshop : MonoBehaviour
{
    public uint appID = 480;//1870970;
    public string title;
    public string description;
    private SteamAPICall_t createItemCall;
    private CallResult<CreateItemResult_t> createCallRes;
    private UGCUpdateHandle_t updateHandle;
    private string path;
    public AudioSource negativeTone;
    public string previewImagePath;
    private bool klikd;
    private bool properImageSelected;
    private void Start()
    {
        
        SteamAPI.Init();
        appID = (uint)SteamUtils.GetAppID();
        title = PlayerPrefs.GetString("CustomMapDirectory");
        path = PlayerPrefs.GetString("CustomMapDirectory") + "\\mapdata.ffwt";
        string dataPath = Application.dataPath + "\\AddOns\\Map_";
        title = title.Substring(Application.dataPath.Length+12, title.Length- Application.dataPath.Length-12);

        description = "Custom Content";
        previewImagePath = path + "\\thumbnail.jpg";
        previewImagePath = PlayerPrefs.GetString("CustomMapDirectory") + "\\thumbnail.jpg";
    }
    public void SubmitToWorkshop()
    {
        appID = (uint)SteamUtils.GetAppID();
        //Debug.Log("UPLOADING TO WORKSHOP");
        if (File.Exists(path))
        {
            //Debug.Log("FILE EXISTS");
            if (SteamManager.Initialized)
            {
                //Debug.Log("STEAMMANAGER INITIALIZED");
                createCallRes = CallResult<CreateItemResult_t>.Create(OnCreateItem);
                SteamAPICall_t handle = SteamUGC.CreateItem(new AppId_t(appID), EWorkshopFileType.k_EWorkshopFileTypeCommunity);
                createCallRes.Set(handle);
            }
        }
    }

    void OnCreateItem(CreateItemResult_t pCallback, bool bIOFaliure)
    {
        //Debug.Log("CREATING ITEM");
        if (!pCallback.m_bUserNeedsToAcceptWorkshopLegalAgreement)
        {
            updateHandle = SteamUGC.StartItemUpdate(new AppId_t(appID), pCallback.m_nPublishedFileId);
            SteamUGC.SetItemTitle(updateHandle, title);
            SteamUGC.SetItemDescription(updateHandle, description);
            //Debug.Log("PATH IS + " + path);
            SteamUGC.SetItemContent(updateHandle, path);
            //Debug.Log("SENT");
            if (File.Exists(previewImagePath))
                SteamUGC.SetItemPreview(updateHandle, previewImagePath);
            SteamUGC.SetItemVisibility(updateHandle, 0);
            SteamUGC.SubmitItemUpdate(updateHandle, "New workshop item");
            SteamFriends.ActivateGameOverlayToWebPage("steam://url/CommunityFilePage/" + pCallback.m_nPublishedFileId);
            //Debug.Log("CONFIRMED " + SteamUGC.GetItemInstallInfo(pCallback.m_nPublishedFileId, out ulong SizeOnDisk, out string Folder, 1024, out uint punTimeStamp));
        }
        else
        {
            redirectToLegal();
        }
    }
    public void redirectToLegal()
    {
        SteamFriends.ActivateGameOverlayToWebPage("https://steamcommunity.com/sharedfiles/workshoplegalagreement");
    }
}
