using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
    public GameObject dollObject;
    public bool doll;
    public int savedWep;
    public GameObject canvas;
    public LayerMask layers;
    public bool sniper;
    public bool SMG;
    public bool reverse;
    public Vector3 startPos;
    public bool arrow;
    public bool AlienBullet;
    public bool RocketLauncher;
    public GameObject explode;
    public bool Axe;
    public Vector3 vel;
    public WeaponsAnim player;
    public PlayerMovement playerParent;
    public MeshRenderer bulletMR;
    public MeshRenderer arrowMR;
    public GameObject AlienBulletOBJ;
    public MeshRenderer RocketLauncherMR;
    public GameObject AxeRenderer;
    public GameObject axeLeaves;
    Rigidbody rb;
    public bool returnAxe;
    float axeTime;
    public GameObject waterObject;
    float startTime;
    public GameObject flashCanvas;
    public bool water;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Start()
    {
        startTime = Time.realtimeSinceStartup;
        if (Time.timeSinceLevelLoad < 0.5f || Time.timeScale < 0.5f)
            Destroy(gameObject);
        Camera.main.gameObject.GetComponent<Animator>().Play("recoil");
        canvas.SetActive(!(arrow || AlienBullet || Axe || SMG));
        if (Axe)
        {
            FindObjectOfType<WeaponsAnim>().GetComponent<Animator>().SetBool("hasAxe", false);
            axeTime = Time.realtimeSinceStartup+4;
        }
        playerParent = FindObjectOfType<PlayerMovement>();
        savedWep = FindObjectOfType<WeaponsAnim>().weaponNum;
        rb = GetComponent<Rigidbody>();
        AlienBulletOBJ.SetActive(AlienBullet);
        RocketLauncherMR.enabled = RocketLauncher;
        arrowMR.gameObject.SetActive(arrow);
        AxeRenderer.gameObject.SetActive(Axe);
        //bulletMR.enabled = (!arrow && !AlienBullet && !RocketLauncher && !Axe);
        if (RocketLauncher)
        {
            GetComponent<SphereCollider>().radius = 0.4f;
        }
        if (AlienBullet)
            GetComponent<SphereCollider>().radius = 0.5f;
        if (arrow)
        {
            vel *= 2;
            bulletMR.enabled = false;
            arrowMR.enabled = true;
        }
        if (reverse)
        {
            vel *= -1;
            transform.position = startPos;
        }
    }
    void Update()
    {
        dollObject.SetActive(doll);
        waterObject.SetActive(water);
        if (Axe && (Vector3.Distance(transform.position, playerParent.transform.position) < 3 && returnAxe || Time.realtimeSinceStartup > axeTime))
        {
            Debug.Log("HAS AXE");
            FindObjectOfType<WeaponsAnim>().GetComponent<Animator>().SetBool("hasAxe", true);
            Destroy(gameObject);
        }
        else if (Vector3.Distance(transform.position, playerParent.transform.position) > 40 && Axe && !returnAxe)
        {
            returnAxe = true;
        }
        if (returnAxe)
        {
            rb.velocity = new Vector3();
            transform.position = Vector3.MoveTowards(transform.position, playerParent.transform.position, Time.deltaTime * 55);
        }
        else if (Axe)
            rb.velocity = vel * 9;
        else if (explode)
            rb.velocity = vel * 6;
        else if (AlienBullet)
            rb.velocity = vel * 4;
        else if (sniper)
            rb.velocity = vel * 30;
        else
            rb.velocity = vel * 3;
    }
    private void OnTriggerEnter(Collider collision)
    {
        checkTrigger(collision.gameObject);
    }
    public void checkTrigger(GameObject collision)
    {
        if (collision.gameObject.GetComponent<triggerenable>() || collision.gameObject.GetComponent<triggerAnim>() || collision.gameObject.GetComponent<triggerSound>() || collision.gameObject.GetComponent<enableTrigger>())
            return;
        if (collision.gameObject.name.Contains("Player"))
            return;
        if (collision.layer == layers)
            return;
        Debug.Log("TRIGGER PASS " + collision.gameObject.name);
        Debug.Log("HIT " + collision.gameObject);
        if (collision.gameObject.name == "Player")
            return;
        if (Axe && returnAxe)
            return;
        Debug.Log("bullet triggered");
        if (reverse)
        {
            if (collision.gameObject.name.Contains("Player"))
                Destroy(gameObject);
            return;
        }
        Debug.Log("HIT " + collision.gameObject.name);
        if (collision.gameObject.GetComponent<PlayerMovement>() || collision.gameObject.GetComponent<playerBullet>() || collision.gameObject.GetComponent<NullTrigger>())
            return;
        GameObject hit = collision.gameObject;
        if (RocketLauncher)
        {
            GameObject k = Instantiate(explode);
            k.transform.position = transform.position;
            k.transform.parent = null;
            if (collision.gameObject.GetComponent<explodableObject>())
                Destroy(collision.gameObject);
        }
        if (Axe)
        {
            GameObject f = Instantiate(axeLeaves);
            f.transform.position = transform.position;
            Destroy(f, 2);
            foreach (Collider c in Physics.OverlapSphere(transform.position, 15))
            {
                if (c.GetComponent<enemyHealth>() && !c.GetComponent<enemyHealth>().unkillable)
                {
                    c.GetComponent<enemyHealth>().currentHealth -= c.GetComponent<enemyHealth>().damages[1] * 10;
                    Destroy(c.gameObject);
                }
            }
            returnAxe = true;
            return;
        }
        else
        {
            if (hit.transform.gameObject.GetComponent<enemyHealth>())
            {
                hitEnemy(hit.transform.gameObject);
            }

        }
        if (!AlienBullet)
            Destroy(gameObject);
    }
    void hitEnemy(GameObject hit)
    {
        hit.transform.gameObject.GetComponent<enemyHealth>().colorTime = Time.realtimeSinceStartup + 0.3f;
        playerParent.hitSound();
        Debug.Log("bullet hit");
        if (hit.transform.gameObject.GetComponent<enemyHealth>().hitSound)
            FindObjectOfType<UniversalAudio>().playSound(hit.transform.gameObject.GetComponent<enemyHealth>().hitSound);
        Debug.Log("HIT ENEMY");
        enemyHealth he = hit.transform.gameObject.GetComponent<enemyHealth>();
        float damage = 0;
        if (doll)
            damage = he.damages[1] * 1;
        else if (sniper)
            damage = he.damages[1] * 15;
        else if (explode)
            damage = he.damages[savedWep] * 25;
        else if (AlienBullet)
            damage = he.damages[savedWep] * 3;
        else
            damage = he.damages[savedWep] * 2;
        if (FindObjectOfType<PlayerMovement>().nightTime)
            damage /= 2;
        he.currentHealth -= damage;
        if (he.currentHealth >= 0)
            player.GetComponent<AudioSource>().PlayOneShot(player.hitmarkerSound, 1);
        else
        {
            player.GetComponent<AudioSource>().PlayOneShot(player.killSound, 1);
        }
        if (he.bloodObj)
        {
            GameObject f = Instantiate(he.bloodObj);
            Destroy(f, he.bloodTime);
        }
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        checkTrigger(collision.gameObject);
        Debug.Log("COLLISION");
        if (collision.gameObject.GetComponent<PlayerMovement>() || collision.gameObject.GetComponent<playerBullet>() || collision.gameObject.GetComponent<NullTrigger>())
            return;
        if (collision.gameObject.name == "Player")
            return;
        if (Axe && returnAxe)
            return;
        Destroy(gameObject);
    }

}
