using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderCode : MonoBehaviour
{
    closePlayer closePlayerObject;
    public GameObject spiderHB;
    public billboardOBJ obj;
    public Animator anim;
    public GameObject player;
    Rigidbody rb;
    Vector3 dir;
    public bool DontTargetPlayer;
    float snarkAngleTime;
    Vector3 angles;
    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<closePlayer>())
            gameObject.AddComponent<closePlayer>();
        closePlayerObject = GetComponent<closePlayer>();
        snarkAngleTime = Time.realtimeSinceStartup + 0.2f;
        angles = FindObjectOfType<PlayerMovement>().transform.localEulerAngles;
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
        if (DontTargetPlayer)
        {
            findObject();
            }
        }

    void findObject()
    {
        GameObject closestEnemy = null;
        float distance = float.MaxValue;
        enemyHealth[] list = FindObjectsOfType<enemyHealth>();

        for (int i = 0; i < list.Length; i++)
        {
            Debug.Log("Starting Test " + list[i].gameObject.name);
            float tempDistance = Vector3.Distance(transform.position, list[i].transform.position);
            if (list[i] != this.gameObject.GetComponent<enemyHealth>() && tempDistance < distance)
            {

                distance = tempDistance;
                Debug.Log("TESTING DISTANCE " + list[i].gameObject.name + " DISTANCE IS " + distance);
                closestEnemy = list[i].gameObject;
            }
            player = closestEnemy;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (closePlayerObject)
            player = closePlayerObject.attachedPlayer;
        if (player == null)
            findObject();
        if (DontTargetPlayer && Time.realtimeSinceStartup < snarkAngleTime)
        {
            transform.localEulerAngles = angles;
            transform.position += transform.forward*3*Time.deltaTime*15;
        }
        if (!spiderHB)
            Destroy(gameObject);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("SpiderSpawn"))
        {
            return;
        }
            float speed = 2;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("SpiderJumpAttack"))
        {
            obj.enabled = false;
            speed = 3.2f;
            if (Vector3.Distance(transform.position, player.transform.position) < 4)
            {
                if (DontTargetPlayer)
                {
                    player.GetComponent<enemyHealth>().currentHealth = -1;
                    Destroy(gameObject);
                }
                else
                {
                        FindObjectOfType<UniversalAudio>().bite();
                    FindObjectOfType<PlayerVoices>().Pain();
                    closePlayerObject.attachedPlayer.GetComponent<attachedHealth>().p.health -= 1;
                }
            }
        }
        else
        {
            obj.enabled = true;
            dir = Vector3.MoveTowards(transform.position, player.transform.position + Random.insideUnitSphere*3, 5) - transform.position;
        }
        dir.y = rb.velocity.y/speed;
        rb.velocity = dir * speed;
    }
}
