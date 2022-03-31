using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runAtPlayer : MonoBehaviour
{
    public bool Raycast;
    public float PunchTime;
    [HideInInspector] public float seenTime;
    Vector3 startPos;
    public float seenDelay;
    public bool alzheimers;
    bool found;
    public bool flying;
    public float speed;
    bool check;
    GameObject player;
    public Material[] sprites;
    public string[] spriteTags;
    public bool rot;
    string activeTag;
    public MeshRenderer MeshRender;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        found = false;
        check = false;
        player = FindObjectOfType<WolfMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (flying)
        {
            transform.position += transform.up * ((player.transform.position.y + 1 + Mathf.Sin(Time.realtimeSinceStartup)) - transform.position.y) * Time.deltaTime;
            
        }
        activeTag = "idle";
        
        {
            
            check = true;
            if (Raycast)
            {
                RaycastHit hit;
                transform.LookAt(player.transform);
                Quaternion rot2 = transform.rotation;
                rot2.x = 0;
                if (rot)
                transform.rotation = rot2;
                if (Physics.Raycast(transform.position, transform.forward, out hit))
                {
                    if (hit.transform.gameObject != player)
                        check = false;
                    if (check && !found)
                    {
                        seenTime = Time.realtimeSinceStartup + seenDelay;
                        Debug.Log("PLAYER SEEN!\n TIME IS" + Time.realtimeSinceStartup + "\nSEEN DELAY IS " + seenTime);
                    }
                    if (check)
                        found = true;
                }
                else
                    check = false;
                
            }
        }
        if (found && !alzheimers)
            check = true;
            if (check)
            {
            activeTag = "running";
            if (Mathf.Round(Time.frameCount/50)%2 == 0)
                activeTag = "running2";
            Vector3 target = player.transform.position;
            target.y = transform.position.y;
                GetComponent<Rigidbody>().velocity = (Vector3.MoveTowards(new Vector3(), target-transform.position, 8*speed) + (transform.up * GetComponent<Rigidbody>().velocity.y));
            }
        if (Time.realtimeSinceStartup < PunchTime)
            activeTag = "attack";
        if (Time.realtimeSinceStartup < seenTime)
        {
            activeTag = "idle";
            GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);
        }
        Material mat = sprites[0];
        for (int i = 0; i<sprites.Length; i++)
        {
            if (spriteTags[i] == activeTag)
                mat = sprites[i];
        }
        MeshRender.material = mat;
    }
}
