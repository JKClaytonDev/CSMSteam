using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRockManager : MonoBehaviour
{
    public GameObject rock;
    public GameObject aboveRock;
    public float rockTime;
    Vector3 startPos;
    // Start is called before the first frame update
    private void Start()
    {
        startPos = transform.position;
    }
    public void AboveRocks()
    {

            GameObject f = Instantiate(aboveRock);
            Vector3 sphere = aboveRock.transform.position + Vector3.right * Random.Range(-15, 15) + Vector3.forward * Random.Range(-15, 15);
            f.transform.position = sphere;
            f.SetActive(true);
            Destroy(f, 3);

    }
    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup > rockTime)
        {
            GameObject f = Instantiate(rock);
            Vector3 sphere = startPos + Vector3.right * Random.Range(-30, 30) + Vector3.forward * Random.Range(-30, 30);
            f.transform.position = sphere;
            f.SetActive(true);
            rockTime = Time.realtimeSinceStartup + 1;
        }
    }
}
