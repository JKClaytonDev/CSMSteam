using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadManager : MonoBehaviour
{
    public float padTime;
    public AudioClip sound;
    public AudioClip landSound;
    bool attached;
    GameObject player;
    Vector3 currentPos;
    public GameObject target;
    float distance;
    public float calcDistance;
    public float fullDistance;
    public float playerDistance;
    Vector3 targetPos;
    public GameObject activePad;
    public GameObject disabledPad;
    public GameObject volume;
    // Start is called before the first frame update
    void Start()
    {
        if (target) {
            if (!target.GetComponent<JumpPadManager>().target)
                target.GetComponent<JumpPadManager>().target = gameObject;
                    }
    }

    // Update is called once per frame
    void Update()
    {
        activePad.SetActive(Time.realtimeSinceStartup > padTime);
        disabledPad.SetActive(Time.realtimeSinceStartup < padTime);
        volume.SetActive(attached);
        if (attached)
        {
            Vector3 fw = Vector3.MoveTowards(transform.position, target.transform.position, 0);
            currentPos = Vector3.MoveTowards(currentPos, target.transform.position, Time.deltaTime*100);
            player.GetComponent<Rigidbody>().velocity = new Vector3();
            playerDistance = Vector3.Distance(currentPos, target.transform.position);
            calcDistance = (fullDistance / 2)-Mathf.Abs((playerDistance- fullDistance/2));
            player.transform.position = currentPos + Vector3.up* calcDistance * 2;
            if (Vector3.Distance(currentPos, target.transform.position) < 1)
            {
                attached = false;
                target.GetComponent<JumpPadManager>().padTime = Time.realtimeSinceStartup + 25;
                player.GetComponent<AudioSource>().PlayOneShot(landSound);
                player.GetComponent<Rigidbody>().velocity = Physics.gravity;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!player)
            player = FindObjectOfType<PlayerMovement>().gameObject;
        if (Time.realtimeSinceStartup < padTime)
            return;
        if (other.gameObject.name.Contains("Player"))
        {
            if (sound && player.GetComponent<AudioSource>())
                player.GetComponent<AudioSource>().PlayOneShot(sound);
            padTime = Time.realtimeSinceStartup + 25;
            
            attached = true;
            player = FindObjectOfType<PlayerMovement>().gameObject;
            currentPos = player.transform.position;
            fullDistance = Vector3.Distance(player.transform.position, target.transform.position);

        }
    }
}
