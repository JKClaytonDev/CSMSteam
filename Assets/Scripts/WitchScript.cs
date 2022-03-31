using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchScript : MonoBehaviour
{
    public GameObject heal;
    public enemyHealth closestEnemy;
    float maxDistance;
    LineRenderer l;
    private void Start()
    {
        l = GetComponent<LineRenderer>();
    }
    void Update()
    {
        
        if (closestEnemy == null)
        {
            l.enabled = false;
            heal.SetActive(false);
            enemyHealth[] cE = FindObjectsOfType<enemyHealth>();
            maxDistance = 999;
            foreach (enemyHealth h in cE)
            {
                if (Vector3.Distance(transform.position, h.transform.position) < maxDistance && h != GetComponent<enemyHealth>())
                {
                    if (!h.gameObject.name.Contains("Zombie") && !h.gameObject.name.Contains("Blob"))
                    closestEnemy = h;
                    maxDistance = Vector3.Distance(transform.position, h.transform.position);
                }
            }
        }
        else if (maxDistance < 25)
        {
            l.enabled = true;
            heal.SetActive(true);
            heal.transform.position = closestEnemy.transform.position;
            closestEnemy.currentHealth = 1;
            l.SetPosition(0, transform.position);
            l.SetPosition(1, closestEnemy.transform.position);
        }
        else
        {
            l.enabled = false;
            heal.SetActive(false);
        }
    }
}
