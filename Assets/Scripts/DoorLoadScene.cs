using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorLoadScene : MonoBehaviour
{
    public bool setHub;
    public string customString;
    public string[] wepNames = { "NULL", "Deagle", "Shotgun", "DualPistols", "Crossbow", "AlienPistol", "RocketLauncher", "Axe", "Trident", "Whip", "SMG", "Sniper", "Voodoo" };
    public string scene;
    public bool hasWeaponEquip;
    public int weaponEquip;
    public bool customScene;
    public GameObject cam;
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.GetComponent<PlayerMovement>() || other.gameObject.name.Contains("Player"))
        {

            nextLevel();
        }
    }
    public void nextLevel()
    {
        foreach (enemyHealth h in FindObjectsOfType<enemyHealth>())
        {
            h.gameObject.SetActive(false);
        }
        foreach (billboardOBJ k in FindObjectsOfType<billboardOBJ>())
            k.enabled = false;
        if (PlayerPrefs.GetInt("Missions") == 1)
        {
            string scene = SceneManager.GetActiveScene().name;
            int finalChar = int.Parse(scene.ToCharArray()[scene .Length- 1] +"");
            string newScene = scene.Substring(0, scene.Length - 1) + (finalChar + 1);
            Debug.Log("NEW SCENE IS" + newScene);
            if (Application.CanStreamedLevelBeLoaded(newScene))
                SceneManager.LoadScene(newScene);
            else
                SceneManager.LoadScene("Menu");
            return;
        }
        PlayerPrefs.SetFloat("Money", FindObjectOfType<PlayerMovement>().money);
        if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name) == 0)
        {
            if (!SceneManager.GetActiveScene().name.Contains("Castle"))
                PlayerPrefs.SetInt("ShardCount", PlayerPrefs.GetInt("ShardCount") + 1);
        }
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
        if (customString != "")
        PlayerPrefs.SetInt(customString, 1);
        if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}

        if (!customScene || scene == "GetWeapon")
        {
            cam.transform.position = Camera.main.transform.position - Camera.main.transform.forward * 3;
            cam.transform.rotation = Camera.main.transform.rotation;
            foreach (Canvas h in FindObjectsOfType<Canvas>())
                Destroy(h.gameObject);
            foreach (enemyHealth h in FindObjectsOfType<enemyHealth>())
                Destroy(h.gameObject);
            cam.SetActive(true);
            Destroy(FindObjectOfType<PlayerMovement>().gameObject);
        }
        else
        {
            if (setHub)
            {
                if (PlayerPrefs.GetInt("Missions") != 1) { PlayerPrefs.SetString("HubScene", scene); PlayerPrefs.Save(); }

            }
            SceneManager.LoadScene(scene);
        }
    }
}
