using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BossManager : MonoBehaviour
{
    public GameObject phase1;
    public GameObject phase2;
    public GameObject enemy1;
    public GameObject phase3;
    public GameObject enemy2;
    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup < 45+startTime)
        {
            if (!phase1.activeInHierarchy)
            {
                foreach (enemyHealth h in FindObjectsOfType<enemyHealth>())
                {
                    if (!h.unkillable)
                        Destroy(h);
                }
            }
            phase1.SetActive(true);
        }
        else
        {
            phase1.SetActive(false);
            phase2.SetActive(enemy1 != null);
            phase3.SetActive(enemy1 == null && enemy2 != null);
            if (enemy1 == null && enemy2 == null)
            {
                SceneManager.LoadScene("FinalBossEndScene");
            }
        }
    }
}
