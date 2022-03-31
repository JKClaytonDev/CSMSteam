using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VampireAnim : MonoBehaviour
{
    closePlayer closePlayerObject;
    AnimationEvent destroy;
    public AudioClip[] sounds;
    GameObject player;
    public GameObject spawn;
    float frames;
    int textNum;
    public string text;
    float startTime;
    public Text textObject;
    float soundTime;
    public bool play;
    public Canvas c;
    PlayerMovement mvmt;
    PlayerAnimations anim;
    WeaponsAnim wep;
    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<closePlayer>())
            gameObject.AddComponent<closePlayer>();
        closePlayerObject = GetComponent<closePlayer>();
        destroy = new AnimationEvent();
        destroy.functionName = "DestroyVampire";
        mvmt = FindObjectOfType<PlayerMovement>();
        anim = FindObjectOfType<PlayerAnimations>();
        wep = FindObjectOfType<WeaponsAnim>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
        
    }
    public void DestroyVampire()
    {
        player.GetComponent<Rigidbody>().velocity = new Vector3();
        GetComponent<Animator>().Play("VampExit");
        this.enabled = false;
        c.enabled = false;
        mvmt.enabled = true;
        anim.enabled = true;
        wep.enabled = true;
        Destroy(gameObject);
        player.transform.LookAt(transform.position);
    }
    // Update is called once per frame
    void Update()
    {
        if (closePlayerObject)
            player = closePlayerObject.attachedPlayer;
        c.enabled = play;
        frames++;
        if (frames == 3)
        {
            mvmt.enabled = false;
            anim.enabled = false;
            wep.enabled = false;
        }
        player.transform.position = spawn.transform.position;
        
        player.transform.LookAt(transform.position);
        if (play)
        {
            textNum = ((int)((Time.realtimeSinceStartup - startTime) * 25));
            if (textNum < text.Length)
                textObject.text = text.Substring(0, textNum);
            if (textNum < text.Length && Time.realtimeSinceStartup > soundTime)
            {
                
                soundTime = Time.realtimeSinceStartup + 0.2f;
                GetComponent<AudioSource>().PlayOneShot(sounds[Random.Range(0, sounds.Length - 1)], 3);
            }
            if (textNum > text.Length && !GetComponent<AudioSource>().isPlaying)
            {
                player.GetComponent<Rigidbody>().velocity = new Vector3();
                GetComponent<Animator>().Play("VampExit");
                this.enabled = false;
                c.enabled = false;
                mvmt.enabled = true;
                anim.enabled = true;
                wep.enabled = true;
                Destroy(gameObject, 2);
                player.transform.LookAt(transform.position);

            }
        }
        else
        {
            startTime = Time.realtimeSinceStartup;
        }
    }
}
