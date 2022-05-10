using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObjects : MonoBehaviour
{
    public PlayerMovement mvmt;
    public GameObject p2Spawn;
    public GameObject A;
    public GameObject cam;
    Vector3 spawnA;
    Vector3 spawnB;
    public RenderTexture a;
    public RenderTexture b;
    bool tf;
    public RenderTexture c;
    public Animator anim;
    public WeaponsAnim wAnim;
    public int anim1;
    public int anim2;
    public int W1;
    public int W2;
    public Camera cam2;
    Vector2 rot1;
    Vector2 rot2;

    // Start is called before the first frame update
    void Start()
    {
        spawnA = A.transform.position;
        spawnB = p2Spawn.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        char[] time = ("" + (int)Time.realtimeSinceStartup).ToCharArray();
        c = cam.GetComponent<Camera>().targetTexture;
        if (float.Parse("" + time[time.Length - 1]) > 5 == tf)
        {
            tf = !tf;
            //Debug.Log("RUNSCRIPT");
            if (tf)
            {
                /*
                W1 = wAnim.weaponNum;
                wAnim.weaponNum = W2;
                anim1 = anim.GetCurrentAnimatorStateInfo(0).fullPathHash;
                anim.Play(anim2);
                */
                rot2 = new Vector2(mvmt.MX, mvmt.MY);
                mvmt.MX = rot1.x;
                mvmt.MY = rot1.y;
                cam2.gameObject.transform.position = A.transform.position;
                cam2.gameObject.transform.rotation = A.transform.rotation;
                spawnA = A.transform.position;
                A.transform.position = spawnB;
                cam.GetComponent<Camera>().targetTexture = a;
                cam2.GetComponent<Camera>().targetTexture = b;
            }
            else
            {
                /*
                W2 = wAnim.weaponNum;
                wAnim.weaponNum = W1;
                anim2 = anim.GetCurrentAnimatorStateInfo(0).fullPathHash;
                anim.Play(anim1);
                */
                rot1 = new Vector2(mvmt.MX, mvmt.MY);
                mvmt.MX = rot2.x;
                mvmt.MY = rot2.y;
                cam2.gameObject.transform.position = A.transform.position;
                cam2.gameObject.transform.rotation = A.transform.rotation;
                spawnB = A.transform.position;
                A.transform.position = spawnA;
                cam.GetComponent<Camera>().targetTexture = b;
                cam2.GetComponent<Camera>().targetTexture = a;
            }
        }
        
    }
}
