using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareCrowAI : MonoBehaviour
{
    closePlayer closePlayerObject;
    public LineRenderer ln;
    GameObject player;
    public bool hooked = false;
    public bool usinghook = false;
    float playerGrappleDist = 0.5f;
    public Material idle;
    public Material up;
    public Material hooking;
    public Material hookedsprite;
    float lastCheckTime;
    MeshRenderer ren;
    Vector3 startpos;
    Rigidbody rb;
    public GameObject pole;
    public AudioClip[] sounds;
    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<closePlayer>())
            gameObject.AddComponent<closePlayer>();
        closePlayerObject = GetComponent<closePlayer>();
        startpos = transform.position;
        rb = GetComponent<Rigidbody>();
       ren = GetComponent<MeshRenderer>();
        try
        {
            player = FindObjectOfType<PlayerMovement>().gameObject;
        }
        catch
        {

        }
        ln.SetPosition(0, transform.position);
        ln.SetPosition(1, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (closePlayerObject)
            player = closePlayerObject.attachedPlayer;
        GetComponent<Rigidbody>().velocity = new Vector3();
        Vector3 scale = transform.localScale;
        scale.x = 0.25f;
        transform.localScale = scale;
        if (!hooked)
        {
            GetComponent<enemyHealth>().enabled = false;
            scale.x = 0.5f;
            transform.localScale = scale;
            ren.material = idle;
            usinghook = false;
            GetComponent<enemyHealth>().currentHealth = 1;
        }
        else
            ren.material = up;
            GetComponent<enemyHealth>().enabled = true;
        try
        {
            if (!hooked && lastCheckTime < Time.realtimeSinceStartup - 1 && Vector3.Distance(transform.position, player.transform.position) < 25)
            {
                lastCheckTime = Time.realtimeSinceStartup;

                //Debug.Log("SCARECHECK");
                RaycastHit hit;
                Physics.Raycast(transform.position, Vector3.MoveTowards(transform.position, player.transform.position, 1) - transform.position, out hit);
                if (hit.transform.gameObject.GetComponent<PlayerMovement>())
                {
                    GameObject f = Instantiate(pole);
                    GetComponent<AudioSource>().PlayOneShot(sounds[0]);
                    pole.transform.position = transform.position + transform.forward * 0.2f;
                    hooked = true;
                }
            }
            Vector3 ppos = player.transform.position;
            ppos.y = transform.position.y;
        }
        catch
        {

        }
        
        if (hooked)
        {
            if (lastCheckTime < Time.realtimeSinceStartup - 0.5f)
            {
                lastCheckTime = Time.realtimeSinceStartup;
                if (Random.Range(1, 3) == 1)
                    return;
                usinghook = true;
                GetComponent<AudioSource>().PlayOneShot(sounds[1]);
            }
            
        }
        if (hooked && usinghook)
        {
            ln.enabled = true;
            ren.material = hooking;
            ln.SetPosition(0, ln.transform.position);
            Vector3 pos = ln.GetPosition(1);
            if (Vector3.Distance(pos, player.transform.position - Vector3.up) < 10)
            {
                if (!FindObjectOfType<WeaponsAnim>().GetComponent<AudioSource>().isPlaying)
                FindObjectOfType<WeaponsAnim>().GetComponent<AudioSource>().PlayOneShot(sounds[2]);
                ren.material = hookedsprite;
                pos = player.transform.position - Vector3.up;
                if (Vector3.Distance(transform.position, player.transform.position) > 5 )
                player.transform.position = Vector3.MoveTowards(player.transform.position, transform.position, Time.deltaTime * 6);
                if (player.transform.position.y < transform.position.y)
                player.transform.position += Vector3.up * (transform.position.y- player.transform.position.y);
            }
            pos = Vector3.MoveTowards(pos, (player.transform.position - Vector3.up), Time.deltaTime * 75);
            ln.SetPosition(1, pos);
        }
        try
        {
            if (Vector3.Distance(transform.position, player.transform.position) > 35)
            {
                hooked = false;
                ln.enabled = false;
            }
            GetComponent<enemyHealth>().enabled = true;
        }
        catch
        {

        }
    }
}
