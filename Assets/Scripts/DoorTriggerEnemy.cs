using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerEnemy : MonoBehaviour
{
    public float enemyTime;
    public GameObject door;
    Vector3 startPos;
    Vector3 endPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = door.transform.position;
        endPos = door.transform.position-Vector3.up*10;
    }
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("TRIGGERED BY OBJECT " + other.name);
        if (other.gameObject.GetComponent<enemyHealth>() || other.gameObject.GetComponent<enemyLink>())
            enemyTime = Time.realtimeSinceStartup + 1;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup > enemyTime)
            door.transform.position = Vector3.MoveTowards(door.transform.position, endPos, 20 * Time.deltaTime);
        else
            door.transform.position = Vector3.MoveTowards(door.transform.position, startPos, 20 * Time.deltaTime);
    }
}
