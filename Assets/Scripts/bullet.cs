using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    GameObject player;
    Vector3 dir;
    public Vector3 offset;
    public Renderer sprite;
    public float drop;
    // Start is called before the first frame update
    void Start()
    {
        
        Transform cam = FindObjectOfType<Camera>().gameObject.transform;
        player = FindObjectOfType<WolfMovement>().gameObject;
        transform.position = player.transform.position+cam.forward*2;
        transform.rotation = cam.rotation;
        transform.Rotate(offset);
        dir = transform.forward;
        player.GetComponent<WolfMovement>().ammo--;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player || collision.gameObject.GetComponent<bullet>())
            return;
        if (collision.gameObject.GetComponent<EnemyHealth>())
        {
            EnemyHealth h = collision.gameObject.GetComponent<EnemyHealth>();
            collision.gameObject.GetComponent<AudioSource>().PlayOneShot(h.damageSounds[Random.Range(0, h.damageSounds.Length)]);
            collision.gameObject.GetComponent<EnemyHealth>().health--;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.MoveTowards(transform.position, player.transform.position, 15) - transform.position, ForceMode.VelocityChange);
        }
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += (((dir*35) + (transform.up * drop)) )* Time.deltaTime;
    }
}
