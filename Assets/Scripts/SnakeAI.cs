using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnakeAI : MonoBehaviour
{
    closePlayer closePlayerObject;
    public GameObject player;
    public GameObject[] bones;
    public Vector3[] bonePos;
    public float[] distances;
    public Vector3[] boneAng;
    public GameObject[] target;
    public int changeTime;
    Vector3 pos;
    float boneDistance;
    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<closePlayer>())
            gameObject.AddComponent<closePlayer>();
        closePlayerObject = GetComponent<closePlayer>();
        pos = transform.position;
        player = FindObjectOfType<PlayerMovement>().gameObject ;
        for (int i = 0; i < bones.Length; i++)
        {
            bonePos[i] = bones[i].transform.position;
            boneAng[i] = bones[i].transform.eulerAngles;
            if (i > 0)
            distances[i] = Vector3.Distance(bones[i].transform.position, bones[i - 1].transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (closePlayerObject)
            player = closePlayerObject.attachedPlayer;
        GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        
        float speed = GetComponent<NavMeshAgent>().speed;
        for (int i = 0; i < bones.Length; i++)
        {
            bones[i].transform.position = bonePos[i];
            bones[i].transform.eulerAngles = boneAng[i];
            Vector3 target;
            if (i == 0)
                target = transform.position;
            else
                target = bonePos[i - 1];

            if (Vector3.Distance(bonePos[i], target) > distances[i])
            {
                //Debug.Log("MOVING");
                bonePos[i] = Vector3.MoveTowards(bonePos[i], target, Vector3.Distance(bonePos[i], target) - distances[i]);
            }
            
            
            if (i == 0)
            {
                

                bones[i].transform.localEulerAngles = new Vector3(90, 0, 0);
                bones[i].transform.localPosition = new Vector3();
            }
            else
            {
                bones[i].transform.LookAt(target);
                bones[i].transform.Rotate(-90, 0, 0);
            }
        }
    }
}
