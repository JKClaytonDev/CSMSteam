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
    public float underHealth = 1;
    public float timeSkip = 1;
    public bool mini;
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
        if (GetComponent<enemyHealth>().currentHealth > underHealth)
            return;
        GameObject gn = Instantiate(nade);
        gn.transform.position = g.transform.position;
        gn.transform.LookAt(player.transform);
        gn.GetComponent<Rigidbody>().velocity = (transform.forward * (35 * Random.Range(1, 0.1f)) + transform.up * 3);
        Destroy(gn, 2);
    }
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<enemyHealth>().currentHealth < 0.1f)
        GetComponent<Animator>().speed = 1;
        else if (GetComponent<enemyHealth>().currentHealth < 0.5f)
            GetComponent<Animator>().speed = 0.4f;
        else
            GetComponent<Animator>().speed = 0.2f;
        if (mini)
            GetComponent<Animator>().speed = 0.1f;
        playerPos = Vector3.MoveTowards(playerPos, player.transform.position, Time.deltaTime* 10 * Mathf.Sin(Time.realtimeSinceStartup / 2) + 1);
        playerPos2 = Vector3.MoveTowards(playerPos2, playerPos, Time.deltaTime * 3 * Mathf.Sin(Time.realtimeSinceStartup/5)+1);
        float speed = 10;
        if (mini)
            speed = 1;
        if (GetComponent<enemyHealth>().currentHealth < 0.1f)
            speed = 20;
        if (GetComponent<enemyHealth>().currentHealth < 0.4f)
            speed = 15;
        Vector3 targetVel = (Vector3.MoveTowards(transform.position, playerPos2, 1) - transform.position) * speed;

        rb.velocity = targetVel;
        Vector3 dir = rb.velocity;
        dir.y = 0;

        transform.LookAt(transform.position+(dir*10));
    }
}
