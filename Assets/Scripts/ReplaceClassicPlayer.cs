using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReplaceClassicPlayer : MonoBehaviour
{
    public GameObject playerPrefab;
    PlayerMovement p;
    public GameObject[] replaceEnemies;
    public GameObject[] ammoBoxes;
    public string[] classicLevelStructure;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("ClassicMode") == 0)
        {
            Destroy(gameObject);
            return;
        }
        for (int i = 0; i<classicLevelStructure.Length; i++)
        {
            string s = classicLevelStructure[i];
            if (s == SceneManager.GetActiveScene().name)
            {
                FindObjectOfType<DoorLoadScene>().customScene = true;
                FindObjectOfType<DoorLoadScene>().scene = classicLevelStructure[i+1];
            }
        }
        foreach (AmmoBox k in FindObjectsOfType<AmmoBox>())
        {
            Vector3 ammoPos = k.transform.position;
            Destroy(k.gameObject);
            GameObject newAmmo = Instantiate(ammoBoxes[Random.Range(0, ammoBoxes.Length-1)]);
            newAmmo.transform.position = ammoPos;
            newAmmo.transform.localScale *= 2;
        }
        p = FindObjectOfType<PlayerMovement>();
        GameObject prefab = Instantiate(playerPrefab);
        prefab.transform.eulerAngles = new Vector3();
        Vector3 angles = FindObjectOfType<PlayerMovement>().gameObject.transform.localEulerAngles;
        angles.x = 0;
        angles.z = 0;
        FindObjectOfType<WolfMovement>().transform.localEulerAngles = angles;
        Vector3 pos = FindObjectOfType<PlayerMovement>().gameObject.transform.position;
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        FindObjectOfType<PlayerMovement>().gameObject.transform.parent = prefab.transform;
        FindObjectOfType<PlayerMovement>().gameObject.SetActive(false);
        prefab.transform.position = pos;
        prefab.transform.eulerAngles = new Vector3();
        FindObjectOfType<WolfMovement>().transform.parent = null;
        p.gameObject.SetActive(true);
        p.transform.parent = prefab.transform;
        p.transform.localScale = new Vector3();
        FindObjectOfType<PlayerMovement>().gameObject.transform.parent = FindObjectOfType<WolfMovement>().transform;
        p = FindObjectOfType<PlayerMovement>();
        for (int i = 0; i < p.transform.childCount; i++)
            p.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        p.enabled = false;
        foreach (enemyHealth g in FindObjectsOfType<enemyHealth>())
        {
            Vector3 posit = g.transform.position;
            Destroy(g.gameObject);
            GameObject f = Instantiate(replaceEnemies[Random.Range(0, replaceEnemies.Length - 1)]);
            f.transform.position = posit;
            f.transform.localScale *= 2;
        }
    }

    private void Update()
    {
        if (Time.realtimeSinceStartup > 1)
            p.enabled = false;
        p.transform.localPosition = new Vector3();
        p.skip = true;
    }
}
