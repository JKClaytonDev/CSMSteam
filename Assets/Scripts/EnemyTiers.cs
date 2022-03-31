using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyTiers : MonoBehaviour
{
    public GameObject[] tier1;
    public GameObject[] tier2;
    public GameObject[] tier3;
    public GameObject[] tier4;
    public GameObject[] tier5;
    public GameObject[] tier6;
    public GameObject[] allEnemies;
    // Start is called before the first frame update
    void Start()
    {
        if (!(PlayerPrefs.GetInt("MasterQuest") == 1 && PlayerPrefs.GetInt("Missions") != 1))
        {
            Destroy(gameObject);
            return;
        }
        if (SceneManager.GetActiveScene().name.Contains("4"))
        {

            Destroy(gameObject);
            return;
        }
        if (PlayerPrefs.GetInt("Missions") != 1)
        {
            Destroy(gameObject);
            return;
            
        }
        allEnemies = FindObjectsOfType<GameObject>();
        foreach (GameObject h in allEnemies)
        {
            Debug.Log("REPLACED");
            GameObject[] pool = FindEnemy(h);
            if (pool.Length > 0)
            {
                GameObject k = Instantiate(pool[Random.Range(0, pool.Length - 1)]);
                k.transform.position = h.transform.position;
                Destroy(h.gameObject);
                k.transform.parent = null;
                k.name = "JEFF";
            }
        }
    }

    public GameObject[] FindEnemy(GameObject h)
    {
        Debug.Log("FINDING ENEMY");
        GameObject[] selected = { };
        if (searchEnemy(h, tier1))
            selected = tier1;
        else if (searchEnemy(h, tier2))
            selected = tier2;
        else if (searchEnemy(h, tier3))
            selected = tier3;
        else if (searchEnemy(h, tier4))
            selected = tier4;
        else if (searchEnemy(h, tier5))
            selected = tier5;
        else if (searchEnemy(h, tier6))
            selected = tier6;
        return selected;
    }
    public bool searchEnemy(GameObject h, GameObject[] g)
    {
        foreach (GameObject k in g)
        {
                if (h.gameObject.name.Contains(k.name))
                {
                    Debug.Log("FOUND");
                    return true;
                }
        }
        return false;
    }
}
