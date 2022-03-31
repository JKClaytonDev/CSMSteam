using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PickWeapon : MonoBehaviour
{
    public string[] gunNames = { "NULL", "Deagle", "Shotgun", "DualPistols", "Crossbow", "AlienPistol", "RocketLauncher", "Axe", "Snark", "Whip", "SMG", "Sniper"};
    public Text text1;
    public Text text2;
    int unlockedIndex;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        text1.text = gunNames[PlayerPrefs.GetInt("PickGun1")];
        text2.text = gunNames[PlayerPrefs.GetInt("PickGun2")];
        unlockedIndex = PlayerPrefs.GetInt("UnlockIndex");
    }
    public void select(int i)
    {
        if (i == 1)
        {
            PlayerPrefs.SetInt("EquippedWeapon" + PlayerPrefs.GetInt("PickGun1"), 1);
            PlayerPrefs.SetInt("UnlockedIndex" + unlockedIndex, 1);
        }
        if (i == 2)
        {
            PlayerPrefs.SetInt("EquippedWeapon" + PlayerPrefs.GetInt("PickGun2"), 1);
            PlayerPrefs.SetInt("UnlockedIndex" + unlockedIndex, -1);
        }
        if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}
        SceneManager.LoadScene("WeaponTrial"+unlockedIndex);
    }

    
}
