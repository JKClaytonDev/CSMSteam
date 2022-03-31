using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire : MonoBehaviour
{
    closePlayer closePlayerObject;
    GameObject player;
    Vector3 origin;
    float shootTime;
    public GameObject bullet;
    public bool lghost;
    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<closePlayer>())
            gameObject.AddComponent<closePlayer>();
        closePlayerObject = GetComponent<closePlayer>();
        shootTime = Time.realtimeSinceStartup + 1;
        origin = transform.position;
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (closePlayerObject)
            player = closePlayerObject.attachedPlayer;
        Vector3 pos = origin - player.transform.position + origin;
        pos.y = player.transform.position.y+Mathf.Abs(Time.realtimeSinceStartup)/3;
        if (!lghost)
        {
            float timeScale = (Time.realtimeSinceStartup + Mathf.Sin(Time.realtimeSinceStartup) / 2) / 5 + Mathf.Cos(Time.realtimeSinceStartup / 2);
            pos = origin + (Vector3.right * Mathf.Sin(timeScale) + Vector3.forward * Mathf.Cos(timeScale)) * 25;
        }
        transform.position = pos;
        pos = transform.localPosition;
        pos.y = 0;
        transform.localPosition = pos;
        if (Time.realtimeSinceStartup > shootTime)
        {
            shootTime += 0.5f;
            
            GameObject b = Instantiate(bullet);
            b.transform.position = transform.position + transform.forward-Vector3.up*Mathf.Sin(Time.realtimeSinceStartup+transform.position.x);
            b.transform.LookAt(player.transform);
            b.transform.parent = null;
            b.gameObject.SetActive(true);
            Destroy(b, 5);
        }
    }
}
