using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManBatScript : MonoBehaviour
{
    closePlayer closePlayerObject;
    public float distance = 50;
    public float randomTimeSlot = 4;
    public bool spread = false;
    public bool move = true;
    AnimationEvent evt;
    GameObject player;
    float randomTime;
    public GameObject BallObject;
    public GameObject parent;
    public AudioClip activateSound;
    float fireBallTime;
    bool activated = false;
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<closePlayer>())
            gameObject.AddComponent<closePlayer>();
        closePlayerObject = GetComponent<closePlayer>();
        startPos = transform.parent.position;
        evt = new AnimationEvent();
        evt.functionName = "fireBall";
        randomTime = Random.Range(1, 3);
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
            player = FindObjectOfType<PlayerMovement>().gameObject;
        if (closePlayerObject)
            player = closePlayerObject.attachedPlayer;
        if (move)
        parent.transform.position = startPos + new Vector3(Mathf.Cos(Time.realtimeSinceStartup)*3, 0, Mathf.Sin(Time.realtimeSinceStartup) * 3);
        if (Vector3.Distance(transform.position, player.transform.position) < distance && Time.realtimeSinceStartup > randomTime) {
            if (activated)
            {
                activated = true;
                GetComponent<AudioSource>().PlayOneShot(activateSound);
            }
            randomTime += randomTimeSlot;
            if (!spread)
            {
                //Debug.Log("BAT ATTACK");
                GetComponent<Animator>().Play("ManBatAttack");
            }
            else
            {
                transform.Rotate(0, -70, 0);
                fireBall();
                transform.Rotate(0, 70, 0);
                fireBall();
                transform.Rotate(0, 70, 0);
                fireBall();
                transform.Rotate(0, -70, 0);
            }
        }
    }

    void fireBall()
    {
        if (Time.realtimeSinceStartup > fireBallTime)
        {
            fireBallTime = Time.realtimeSinceStartup + 0.5f;
            GameObject ballObj = Instantiate(BallObject);
            ballObj.transform.position = transform.position + transform.forward;
            ballObj.transform.eulerAngles = transform.eulerAngles;
            ballObj.transform.Rotate(-90, 0, 0);
            ballObj.transform.parent = null;
        }
    }
}
