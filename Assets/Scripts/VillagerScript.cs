using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class VillagerScript : MonoBehaviour
{
    Vector3[] positions;
    // Start is called before the first frame update
    void Start()
    {
        positions = new Vector3[FindObjectsOfType<VillagerScript>().Length];
        int index = 0;
        foreach (VillagerScript g in FindObjectsOfType<VillagerScript>())
        {
            positions[index] = g.transform.position;
            index++;
        }
        GetComponent<NavMeshAgent>().destination = transform.position;
        GetComponent<NavMeshAgent>().speed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, GetComponent<NavMeshAgent>().destination) < 2)
        GetComponent<NavMeshAgent>().destination = positions[Random.Range(0, positions.Length - 1)];
    }
}
