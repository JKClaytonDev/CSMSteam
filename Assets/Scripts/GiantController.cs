using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantController : MonoBehaviour
{
    Animator anim;
    AnimationEvent evt1;
    AnimationEvent evt2;
    AnimationEvent evt3;
    public GameObject throwRock;
    public GameObject RockArm;
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        evt1 = new AnimationEvent();
        evt2 = new AnimationEvent();
        evt3 = new AnimationEvent();
        evt1.functionName = "RockUp";
        evt2.functionName = "RockDown";
        evt2.functionName = "RockThrow";
        startPos = transform.position;
        anim = GetComponent<Animator>();
    }
    public void rockThrow()
    {
        GameObject i = Instantiate(throwRock);
        i.transform.position = RockArm.transform.position;
        i.transform.parent = null;
        i.SetActive(true);
        Destroy(i, 3);
        i.transform.LookAt(Camera.main.transform);
        i.GetComponent<Rigidbody>().velocity = i.transform.forward*5;
        
    }
    public void RockUp()
    {
        foreach (BossRock b in FindObjectsOfType<BossRock>())
        {
            if (Random.Range(1, 3) != 3)
                b.throwRock();
        }
    }
    public void RockDown()
    {
        FindObjectOfType<BossRockManager>().AboveRocks();
    }
    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("Random", Random.Range(1, 5));
        transform.position = startPos + (Vector3.right * Mathf.Sin(Time.realtimeSinceStartup / 5) + Vector3.forward * Mathf.Cos(Time.realtimeSinceStartup/5))*10;
    }
}
