using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("ShardCount") == 5)
            unlockGun1();
        else if (PlayerPrefs.GetInt("ShardCount") == 10)
            unlockGun2();
        else if (PlayerPrefs.GetInt("ShardCount") == 20)
            unlockGun3();
        else if (PlayerPrefs.GetInt("ShardCount") == 35)
            unlockGun4();
        for (int i = 0; i<10; i++)
        {
            Debug.Log("HAS GUN " + i + " = " + PlayerPrefs.GetInt("EquippedWeapon" + i));
        }
    }

    void unlockGun1()
    {
        if (!PlayerPrefs.HasKey("UnlockedIndex1"))
        {
            PlayerPrefs.SetInt("PickGun1", 2);
            PlayerPrefs.SetInt("PickGun2", 4);
            PlayerPrefs.SetInt("UnlockIndex", 1);
            if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}
            SceneManager.LoadScene("ChooseWeapon");
        }
    }
    void unlockGun2()
    {
        if (!PlayerPrefs.HasKey("UnlockedIndex2"))
        {
            PlayerPrefs.SetInt("PickGun1", 6);
            PlayerPrefs.SetInt("PickGun2", 10);
            PlayerPrefs.SetInt("UnlockIndex", 2);
            if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}
            SceneManager.LoadScene("ChooseWeapon");
        }
    }
    void unlockGun3()
    {
        if (!PlayerPrefs.HasKey("UnlockedIndex3"))
        {
            PlayerPrefs.SetInt("PickGun1", 11);
            PlayerPrefs.SetInt("PickGun2", 5);

            PlayerPrefs.SetInt("UnlockIndex", 3);
            if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}
            SceneManager.LoadScene("ChooseWeapon");
        }
    }
    void unlockGun4()
    {
        if (!PlayerPrefs.HasKey("UnlockedIndex4"))
        {
            PlayerPrefs.SetInt("PickGun1", 7);
            PlayerPrefs.SetInt("PickGun2", 8);
            PlayerPrefs.SetInt("UnlockIndex", 4);
            if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}
            SceneManager.LoadScene("ChooseWeapon");
        }
    }
}
