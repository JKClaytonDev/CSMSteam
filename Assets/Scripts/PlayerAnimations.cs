using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerAnimations : MonoBehaviour
{
    bool missions;
    public bool whipFire;
    float whipTime;
    public int lockGun;
    int equippedSlot = 1;
    int currentSlot = 1;
    float scrollTime;

    public int unlockedWeapon1;
    public int unlockedWeapon2;
    public int unlockedWeapon3;
    public int unlockedWeapon4;
    Vector3 lPP;
    public PlayerMovement mov;
    public bool reverse;
    public GameObject player;
    public Animator gunAnim;
    public string gunName;
    public GameObject[] weaponOBJ;
    public GameObject strafeParent;
    public int wepIndex = 1;
    public AudioClip[] equipSounds;
    public GameObject light;

    float lightTime;
    string scene;
    PlayerMovement mvmt;
    public bool[] equippedWeapons;
    public bool goblin = false;
    public Animator magicAnim;
    float checkTime;
    bool running;
    public float firePauseTime;
    // Start is called before the first frame update
    void Start()
    {
        unlockedWeapon1 = PlayerPrefs.GetInt("UnlockedIndex1");
        unlockedWeapon2 = PlayerPrefs.GetInt("UnlockedIndex2");
        unlockedWeapon3 = PlayerPrefs.GetInt("UnlockedIndex3");
        unlockedWeapon4 = PlayerPrefs.GetInt("UnlockedIndex4");
        missions = (PlayerPrefs.GetInt("Missions") == 1) || PlayerPrefs.GetInt("BossRush") == 1;
        if (missions)
        {
            unlockedWeapon1 = -1;
            unlockedWeapon2 = -1;
            unlockedWeapon3 = -1;
            unlockedWeapon4 = -1;
            if (Random.Range(-3f, 3f) > 0)
                unlockedWeapon1 = 1;
            if (Random.Range(-3f, 3f) > 0)
                unlockedWeapon2 = 1;
            if (Random.Range(-3f, 3f) > 0)
                unlockedWeapon3 = 1;
            if (Random.Range(-3f, 3f) > 0)
                unlockedWeapon4 = 1;
        }

        if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}

        Physics.gravity = new Vector3(0, -29.81f, 0);
        for (int i = 0; i < equippedWeapons.Length; i++)
            equippedWeapons[i] = PlayerPrefs.GetInt("EquippedWeapon" + i) == 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mov = FindObjectOfType<PlayerMovement>();
        mvmt = FindObjectOfType<PlayerMovement>();
        scene = SceneManager.GetActiveScene().name;
        player.GetComponent<AudioSource>().PlayOneShot(equipSounds[Random.Range(0, equipSounds.Length)]);
        equipGun(1);
        if (lockGun != 0)
            equipGun(lockGun);

    }
    public void equip1()
    {
        player.GetComponent<AudioSource>().PlayOneShot(equipSounds[Random.Range(0, equipSounds.Length)]);
        wepIndex = 1;
        gunName = "Deagle";
        gunAnim.Play(gunName + "Equip");
    }
    public void slowMagic()
    {
        Time.timeScale = 0.7f;
        FindObjectOfType<playerHealth>().health = Mathf.Max(FindObjectOfType<playerHealth>().health , Mathf.Min(80, FindObjectOfType<playerHealth>().health + 30));
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(PlayerPrefs.GetString("WhipKeybind")) && !magicAnim.GetBool("FlashLight") && !Input.GetKey(PlayerPrefs.GetString("FlashlightKeybind")) && !magicAnim.GetCurrentAnimatorStateInfo(0).IsName("FlashHold") && Time.realtimeSinceStartup > whipTime)
        {
            whipTime = Time.realtimeSinceStartup + 1.5f;
            if (PlayerPrefs.GetInt("EquippedWeapon9") == 1 || PlayerPrefs.GetInt("Missions") == 1)
            {
                whipFire = true;
                gunAnim.Play("WhipFire");
            }
        }
        if (!mov)
            mov = player.GetComponent<PlayerMovement>();
        gunAnim.SetBool("Run", gunName != "RocketLauncher" && player.GetComponent<PlayerMovement>().finalRun && gunAnim.GetCurrentAnimatorStateInfo(0).IsName(gunName+"Idle") && Time.realtimeSinceStartup > mov.jumpTime);
        if (Time.realtimeSinceStartup > checkTime)
        {
            running = (Vector3.Distance(player.transform.position, lPP) > 1);
            lPP = player.transform.position;
            checkTime = Time.realtimeSinceStartup + 0.2f;
        }
        if (!gunAnim.GetCurrentAnimatorStateInfo(1).IsName("Knock"))
        {
            if (!gunAnim.GetCurrentAnimatorStateInfo(0).IsName(gunName + "Idle") || Time.realtimeSinceStartup < mov.jumpTime || !running)
                gunAnim.Play("GunIdle", 1);
        }
        if (goblin)
        {
            AnimatorStateInfo animInfo = gunAnim.GetCurrentAnimatorStateInfo(0);
            RaycastHit hit;
            if (Physics.Raycast(player.transform.position, player.transform.forward, out hit))
            {
                Vector3 angles = player.transform.localEulerAngles;
                Vector3 oldAngles = angles;
                angles.y = Mathf.Round(angles.y / 90) * 90;
                angles.x = 0;
                angles.z = 0;
                player.transform.localEulerAngles = angles;
                if (Physics.Raycast(player.transform.position, player.transform.forward, out hit))
                {
                    if (hit.distance < 5 && hit.collider.gameObject.GetComponent<ClimbWall>())
                    {
                        player.transform.position = (hit.point - player.transform.forward * 3);
                        player.GetComponent<Rigidbody>().velocity = Vector3.up * Input.GetAxis("Vertical") * 8 * Mathf.Abs(1) * 25;
                        gunAnim.speed = Mathf.Max(-1, Mathf.Min(1, Mathf.Ceil(Mathf.Abs(Input.GetAxis("Vertical")) / 2)));
                        if (!animInfo.IsName("GoblinClimb"))
                            gunAnim.Play("GoblinClimb");
                        return;
                    }
                }
                player.transform.localEulerAngles = oldAngles;
            }
            gunAnim.speed = 1;
            
            if (animInfo.IsName("DrinkPotion"))
                return;
            if (!(animInfo.IsName("GoblinIdle")|| animInfo.IsName("GoblinBomb") || animInfo.IsName("DrinkPotion")))
                gunAnim.Play("GoblinIdle");
            if (!animInfo.IsName("GoblinBomb") && Input.GetMouseButtonDown(0))
                gunAnim.Play("GoblinBomb");
            return;
        }
        light.SetActive(magicAnim.GetCurrentAnimatorStateInfo(0).IsName("FlashHold"));
        Vector2 strafePos = new Vector2();
        strafePos.x = Mathf.Cos(player.transform.position.x) + Mathf.Sin(player.transform.position.z/2);
        strafePos.y = Mathf.Sin(player.transform.position.x/2) + Mathf.Cos(player.transform.position.z) - 2;
        strafeParent.GetComponent<RectTransform>().localPosition = strafePos*3;
        bool gunReady = gunAnim.GetCurrentAnimatorStateInfo(0).IsName("Null") || gunAnim.GetCurrentAnimatorStateInfo(0).IsName(gunName + "Idle");
        bool magicReady = magicAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle");
        if (Time.timeScale != 1)
            Time.timeScale += 0.5f*Time.deltaTime;
        if (Time.timeScale > 1)
            Time.timeScale = 1;
        float my = (Time.timeScale - 0.1f)*1.7f;
        Vector3 pos = new Vector3();
        pos.y = my;
        if (wepIndex == 9 && !whipFire)
        {
            whipFire = false;
            equipGun(1);
        }
        if (magicReady)
        {
            if (Input.GetAxis("Magic") != 0 && PlayerPrefs.GetFloat("Healing") == 1)
            {
                magicAnim.Play("Magic" + Random.Range(1, 2));
            }
            if (Input.GetKey(PlayerPrefs.GetString("FlashlightKeybind")) || Time.realtimeSinceStartup < lightTime)
            {
                    lightTime = Time.realtimeSinceStartup + 0.2f;
                    magicAnim.Play("FlashLight");
            }
        }
        else if (gunReady)
        {
            gunAnim.speed = 0;
            gunAnim.transform.localPosition += Vector3.up * -1255 * Time.deltaTime;
        }
        magicAnim.SetBool("FlashLight", Input.GetKey(PlayerPrefs.GetString("FlashlightKeybind")) && !Input.GetKey(PlayerPrefs.GetString("WhipKeybind")) && !whipFire);
        if (gunReady && magicReady)
        {
                if (Input.GetKeyDown("1"))
                {
                    if (wepIndex != 3)
                        equipGun(3); //pisol
                    else
                        equipGun(1); //dual pisol
                    return;
                }
                if (Input.GetKeyDown("2"))
                {
                    if (unlockedWeapon1 == 1)
                        equipGun(2); //shotgun
                    if (unlockedWeapon1 == -1)
                        equipGun(4); //bow
                    return;
                }
                if (Input.GetKeyDown("3"))
                {
                    
                if (unlockedWeapon2 == 1)
                    equipGun(6); //rocket launcher
                if (unlockedWeapon2 == -1) //smg
                        equipGun(10);
                    return;
                }
            if (Input.GetKeyDown("4"))
            {
                if (unlockedWeapon3 == 1)
                    equipGun(11); //sniper
                if (unlockedWeapon3 == -1) //alien pistol
                    equipGun(5);
                return;
            }
            if (Input.GetKeyDown("5"))
                {
                    if (unlockedWeapon4 == 1)
                        equipGun(7); //axe 
                    if (unlockedWeapon4 == -1)
                        equipGun(8); //spider
                    return;
                }

            if (gunAnim.speed == 0)
                gunAnim.Play(gunName + "Equip");
            gunAnim.speed = 1.25f;
            if (mov.hasSpeedBoots)
                gunAnim.speed = 1.6f;
            if (mov.slowSpeedTime > Time.realtimeSinceStartup)
                gunAnim.speed = 0.75f;
            if (mov.nightTime)
                gunAnim.speed *= 0.9f;
            gunAnim.SetLayerWeight(1, 1);
            gunAnim.transform.localPosition = new Vector2(0, 0);
            if (Input.GetMouseButtonDown(0) || ((wepIndex == 3 || wepIndex == 10) && Input.GetMouseButton(0)))
            {
                if (Time.timeScale > 0.5f){
                if (FindObjectOfType<WeaponsAnim>().playerAmmo > 0)
                    gunAnim.Play(gunName + "Fire");
                Debug.Log("SHOOT");
                }
            }
            else if (gunName == "Sniper" && Input.GetKey(PlayerPrefs.GetString("MeleeKeybind")))
            {
                if (FindObjectOfType<SniperEnableObject>())
                {
                    foreach (SniperEnableObject o in FindObjectsOfType<SniperEnableObject>())
                    {
                        o.GetComponent<SniperEnableObject>().obj.SetActive(true);
                        Destroy(o);
                    }
                }
                gunAnim.SetLayerWeight(1, 0);
                if (FindObjectOfType<WeaponsAnim>().playerAmmo > 0 && Input.GetMouseButtonDown(0))
                    gunAnim.Play(gunName + "AimFire");
                else if (!gunAnim.GetCurrentAnimatorStateInfo(0).IsName(gunName + "AimFire"))
                    gunAnim.Play(gunName + "Aim");
            }
            else {
                gunAnim.Play(gunName + "Idle");
                Debug.Log("IDLE");
            }
        }
        gunAnim.SetBool("Fire", Input.GetMouseButton(0));
        gunAnim.SetBool("Scope", Input.GetKey(PlayerPrefs.GetString("MeleeKeybind")));
    }
    public void equipGun(int gun)
    {
        
        if (lockGun != 0)
            gun = lockGun;
        if (gun != 12)
        {
            if (PlayerPrefs.GetInt("EquippedWeapon" + gun) == 0 && gun != 1 && !missions)
            {

                return;
            }
        }
        string[] wepNames = { "NULL", "Deagle", "Shotgun", "DualPistols", "Crossbow", "AlienPistol", "RocketLauncher", "Axe", "Trident", "Whip", "SMG", "Sniper", "Voodoo"};
        player.GetComponent<AudioSource>().PlayOneShot(equipSounds[Random.Range(0, equipSounds.Length)]);
        wepIndex = gun;
        gunName = wepNames[gun];
        gunAnim.Play(gunName + "Equip");
        Debug.Log("LOCKING GUN " + gunName);
        return;
    }
}
