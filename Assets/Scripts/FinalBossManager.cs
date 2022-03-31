using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossManager : MonoBehaviour
{
    public GameObject rotateAround;
    public GameObject[] ammo;
    public GameObject[] enemies;
    float spawnTime;
    public ArrayList activeEnemies;
    public GameObject light;
    public float timer = 7;
    bool toggle;
    // Start is called before the first frame update
    void Start()
    {
        activeEnemies = new ArrayList();
        spawnTime = Time.realtimeSinceStartup + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup > spawnTime)
        {
            if (activeEnemies.ToArray().Length > 0)
            {
                foreach (GameObject g in activeEnemies)
                {
                    g.gameObject.SetActive(true);
                    activeEnemies.Remove(g.gameObject);
                }
                activeEnemies.Clear();
            }
            spawnTime += timer;
            toggle = !toggle;
            for (int i = 0; i < 2; i++)
            {
                if (toggle)
                {
                    GameObject a = Instantiate(ammo[Random.Range(0, ammo.Length - 1)]);
                    a.transform.parent = null;
                    a.SetActive(true);
                    a.transform.position = transform.position;
                }
                GameObject k = Instantiate(enemies[Random.Range(0, enemies.Length - 1)]);
                k.SetActive(true);
                k.transform.position = transform.position;
                foreach (Transform g in k.transform)
                {
                    GameObject l = Instantiate(light);
                    l.transform.parent = null;
                    l.transform.position = g.transform.position;
                    l.gameObject.SetActive(true);
                    g.gameObject.SetActive(false);
                    Destroy(l, timer * 1.5f);
                    g.transform.parent = null;
                    activeEnemies.Add(g.gameObject);
                }
                Destroy(k);
            }
        }
    }
}
