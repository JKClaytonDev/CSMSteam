using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDoor : MonoBehaviour
{
    public GameObject canvas;
    public GameObject image;
    public GameObject door;
    public GameObject spawnEnemy1;
    public GameObject spawnEnemy2;
    public GameObject spawnEnemy3;
    public GameObject spawnEnemy4;
    float doorOpenAmount;
    float playerInsideTime;
    float spawnTime;
    public float spawnTimeStep;
    private void OnTriggerStay(Collider other)
    {
        playerInsideTime = Time.realtimeSinceStartup + 0.1f;
    }
    // Start is called before the first frame update
    void Start()
    {
        spawnEnemy1.SetActive(false);
        spawnEnemy2.SetActive(false);
        spawnEnemy3.SetActive(false);
        spawnEnemy4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        canvas.SetActive(Time.realtimeSinceStartup < playerInsideTime && doorOpenAmount <= 100);
        if (Time.realtimeSinceStartup < playerInsideTime && doorOpenAmount <= 100)
        {
            doorOpenAmount += 10 * Time.deltaTime;
            if (Time.realtimeSinceStartup > spawnTime)
            {
                spawnTime = Time.realtimeSinceStartup + spawnTimeStep;
                GameObject selectedZombie = null;
                int randomNumber = Random.Range(1, 4);
                if (randomNumber == 1)
                    selectedZombie = spawnEnemy1;
                if (randomNumber == 2)
                    selectedZombie = spawnEnemy2;
                if (randomNumber == 3)
                    selectedZombie = spawnEnemy3;
                if (randomNumber == 4)
                    selectedZombie = spawnEnemy4;
                GameObject k = Instantiate(selectedZombie);
                k.transform.parent = null;
                k.SetActive(true);
                k.transform.position = selectedZombie.transform.position;
                k.transform.localScale = selectedZombie.transform.lossyScale;
                k.transform.position = selectedZombie.transform.position;

            }
        }
        else if (doorOpenAmount < 100)
        {
            spawnTime = Time.realtimeSinceStartup + 1;
            doorOpenAmount = 0;
        }
        if (doorOpenAmount > 100)
        {
            door.transform.position -= Vector3.up * 20 * Time.deltaTime;
        }
        image.transform.localScale = new Vector3(doorOpenAmount / 100, 1, 1);
    }
}
