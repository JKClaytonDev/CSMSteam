using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class WolfMovement : MonoBehaviour
{
    [HideInInspector] public bool[] equippedWeapons = { true, false, false };
    [HideInInspector] public float health = 100;
    [HideInInspector] public float ammo = 100;
    public Text healthText;
    public Text ammoText;
    public GameObject menu;
    public GameObject bullet;
    public GameObject child;
    public Animator gun;
    public Sprite[] idleMats;
    public Sprite[] fireMats;
    public AudioClip[] gunSounds;
    public int[] spread;
    public float mouseSensitivity;
    public int[] bulletCount;
    public float[] bulletdrop;
    public Image idle;
    public Image fire;
    public Material[] bulletSprites;


    [HideInInspector] public int equippedWeapon = 0;
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = Vector3.up * -99;
        if (PlayerPrefs.GetFloat("Mouse") == 0)
            PlayerPrefs.SetFloat("Mouse", 1);
       
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void OnCollisionStay(Collision collision)
    {
        Debug.Log(collision.gameObject.name + "ASF");
        if (collision.gameObject.layer == 12 || collision.gameObject.name == "LAVA")
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // Update is called once per frame
    void Update()
    {
        child.GetComponent<Camera>().fieldOfView = 90;
        if (ammo < 0)
            ammo = 0;
        idle.gameObject.GetComponent<RectTransform>().localPosition = new Vector2((Mathf.Sin(transform.position.x/ 5) + Mathf.Cos(transform.position.z/5))*30, -386 + (Mathf.Cos(transform.position.x/10) + Mathf.Sin(transform.position.z/10) - 2) * 10);
        fire.gameObject.GetComponent<RectTransform>().localPosition = idle.GetComponent<RectTransform>().localPosition;
        float speed = 1;
       
            speed = 1.5f;
        mouseSensitivity = PlayerPrefs.GetFloat("Mouse");
        if (menu.GetComponent<Menu>().active)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            return;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;

        }
        if (health <= 0)
        {
            health = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        healthText.text = "Health: " + health;
        ammoText.text = "Ammo: " + ammo;
        int lastweapon = equippedWeapon;
            if (Input.GetKeyDown("1"))
                equippedWeapon = 0;
            if (Input.GetKeyDown("2"))
                equippedWeapon = 1;
            if (Input.GetKeyDown("3"))
                equippedWeapon = 2;

        if (!equippedWeapons[(int)equippedWeapon])
            equippedWeapon = lastweapon;
        fire.sprite = fireMats[equippedWeapon];
        idle.sprite = idleMats[equippedWeapon];

        transform.Rotate(0, Input.GetAxis("Mouse X")*mouseSensitivity, 0);
        child.transform.Rotate(-Input.GetAxis("Mouse Y")* mouseSensitivity, 0, 0);
        GetComponent<Rigidbody>().velocity = (Input.GetAxis("Vertical") * transform.forward + Input.GetAxis("Horizontal") * transform.right)*(10 * speed) + GetComponent<Rigidbody>().velocity.y * transform.up;
        if (Input.GetMouseButtonDown(0) && ammo > 0)
        {
            GetComponent<AudioSource>().PlayOneShot(gunSounds[equippedWeapon]);
            gun.Play("GunFire");
            bullet.GetComponent<bullet>().sprite.material = bulletSprites[equippedWeapon];
            bullet.GetComponent<bullet>().drop = bulletdrop[equippedWeapon];
            if (bulletCount[equippedWeapon] == 1)
            {
                bullet.GetComponent<bullet>().offset = new Vector3(0, -0, 0);
                Instantiate(bullet);
            }
            else
            {
                float bcount = bulletCount[equippedWeapon];
                float rot = -spread[equippedWeapon];

                for (int i = 0; i < bcount; i++) {
                    rot += (float)((double)spread[equippedWeapon] * (double)2 / (double)(bcount+1));
                    bullet.GetComponent<bullet>().offset = new Vector3(0, rot, 0);
                    Instantiate(bullet);
            }
                
            }
        }
    }

    
}
