using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.HighDefinition;


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
        child.GetComponent<HDAdditionalCameraData>().allowDynamicResolution = PlayerPrefs.GetInt("DisableDynamic") != 1;
        Physics.gravity = Vector3.up * -99;
        if (PlayerPrefs.GetFloat("Mouse") == 0)
            PlayerPrefs.SetFloat("Mouse", 1);
       
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void OnCollisionStay(Collision collision)
    {
        //Debug.Log(collision.gameObject.name + "ASF");
        if (collision.gameObject.layer == 12 || collision.gameObject.name == "LAVA")
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
            return;
        child.GetComponent<Camera>().fieldOfView = PlayerPrefs.GetFloat("FOV");
        if (ammo < 0)
            ammo = 0;
        idle.gameObject.GetComponent<RectTransform>().localPosition = new Vector2((Mathf.Sin(transform.position.x/ 5) + Mathf.Cos(transform.position.z/5))*30, -386 + (Mathf.Cos(transform.position.x/10) + Mathf.Sin(transform.position.z/10) - 2) * 10);
        fire.gameObject.GetComponent<RectTransform>().localPosition = idle.GetComponent<RectTransform>().localPosition;
        float speed = 1;
       
            speed = 1.5f;
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
        float sens = PlayerPrefs.GetFloat("MouseSens");
        float MSensM = 0.5f;
        if (Input.GetMouseButton(1))
            MSensM = 0.3f;
        int rv = 1;
        int flipped = 1;
        float MY = -Input.GetAxis("Mouse Y") * 6 * MSensM * rv * sens / 15;
        float MX = Input.GetAxis("Mouse X") * 6 * MSensM * rv * flipped * sens / 15;

        transform.Rotate(0, MX * Time.timeScale, 0);
        child.transform.Rotate(MY * Time.timeScale, 0, 0);
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
