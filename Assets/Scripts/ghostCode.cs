using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostCode : MonoBehaviour
{
    closePlayer closePlayerObject;
    public GameObject player;
    public GameObject child;
    float startXscale;
    public GameObject gotcha;
    public AudioClip boosound;
    public Animator magicAnim;
    GameObject coldBreath;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            if (!GetComponent<closePlayer>())
                gameObject.AddComponent<closePlayer>();
            closePlayerObject = GetComponent<closePlayer>();
            coldBreath = GameObject.Find("ColdBreath");

            magicAnim = FindObjectOfType<PlayerAnimations>().magicAnim;
            startXscale = transform.localScale.x;
            player = FindObjectOfType<PlayerMovement>().gameObject;
            child.SetActive(false);
        }
        catch
        {
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (closePlayerObject)
            player = closePlayerObject.attachedPlayer;
        try
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 2)
            {

                Instantiate(gotcha);
                Destroy(gameObject);
                FindObjectOfType<PlayerVoices>().GhostGotcha();
            }
        }
        catch
        {
            return;
        }
        float lastY = transform.position.y;
        if (Vector3.Distance(transform.position, player.transform.position) < 45 && !child.activeInHierarchy)
        {
            coldBreath.GetComponent<DeactivateTime>().time = Time.realtimeSinceStartup + 1;
            coldBreath.SetActive(true);
        }
            if (Vector3.Distance(transform.position, player.transform.position) < 40 && !child.activeInHierarchy)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, Vector3.MoveTowards(transform.position, player.transform.position, 1) - transform.position, out hit);
            if (hit.transform.gameObject.GetComponent<PlayerMovement>())
            {
                GetComponent<AudioSource>().PlayOneShot(boosound);
                child.SetActive(true);
            }
        }
        if (child.activeInHierarchy)
        {
            Vector3 oldRot = transform.localEulerAngles;
            transform.LookAt(player.transform);
            transform.position += transform.forward * Time.deltaTime * 11f;
            transform.localEulerAngles = oldRot;
            if (magicAnim.GetCurrentAnimatorStateInfo(0).IsName("FlashHold") && Mathf.Abs((player.GetComponent<PlayerMovement>().mainCam.WorldToScreenPoint(transform.position).x - Screen.width / 2)) < 90)
            {
                transform.position -= transform.up * Time.deltaTime * 6;
                Vector3 lScale = transform.localScale;
                lScale.x /= 1 + Time.deltaTime * 3;
                transform.localScale = lScale;
            }
        }
        Vector3 pos = transform.position;
        pos.y = player.transform.position.y;
        transform.position = pos;
        if (transform.localScale.x < startXscale/3)
            Destroy(gameObject);
    }
}
