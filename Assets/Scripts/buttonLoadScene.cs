using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class buttonLoadScene : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1;
    }
    public void sceneLoad(string scene)
    {
        PlayerPrefs.SetInt("EquippedWeapon" + PlayerPrefs.GetInt("UnlockWeapon"), 1);
        SceneManager.LoadScene(scene);
    }
}
