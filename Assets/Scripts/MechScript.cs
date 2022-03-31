using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechScript : MonoBehaviour
{
    GameObject player;
    Rigidbody rb;
    Vector3 playerPos;
    Vector3 playerPos2;
    public GameObject nade;
    public GameObject leftHand;
    public GameObject rightHand;
    AnimationEvent fireL;
    AnimationEvent fireR;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
        playerPos = player.transform.position;
        playerPos2 = player.transform.position;
    }
    public void fireLeft()
    {
        fire(leftHand);
    }
    public void fireRight()
    {
        fire(rightHand);
    }
    public void fire(GameObject g)
    {
        GameObject gn = Instantiate(nade);
        gn.transform.position = g.transform.position;
        gn.transform.LookAt(player.transform);
        gn.GetComponent<Rigidbody>().velocity = (transform.forward * (35 * Random.Range(1, 0.1f)) + transform.up * 3);
    }
    // Update is called once per frame
    void Update()
    {
        playerPos = Vector3.MoveTowards(playerPos, player.transform.position, Time.deltaTime* 10 * Mathf.Sin(Time.realtimeSinceStartup / 2) + 1);
        playerPos2 = Vector3.MoveTowards(playerPos2, playerPos, Time.deltaTime * 3 * Mathf.Sin(Time.realtimeSinceStartup/5)+1);

        Vector3 targetVel = (Vector3.MoveTowards(transform.position, playerPos2, 1) - transform.position) * 15;

        rb.velocity = targetVel;
        Vector3 dir = rb.velocity;
        dir.y = 0;

        transform.LookAt(transform.position+(dir*10));
    }
}
