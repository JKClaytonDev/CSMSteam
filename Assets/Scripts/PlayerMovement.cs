using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    float checkTime;
    public bool skip;
    public SoundManager s;
    public bool finalRun;
    bool running;
    public GameObject title;
    public Vector3 startSpawnPos;
    public GameObject flip;
    public int flipped = 1;
    public float jumpTime;
    public float groundDistance;
    public bool onGround;
    public bool dollActive;
    public Animator weaponsAnim;
    public GameObject flashCanvas;
    public GameObject wepCanvas;
    public Animator flashAnim;
    public playerHealth p;
    float healTime;
    public GameObject healImage;
    public GameObject deadEnemyParticles;
    public AudioClip EnemyhitSound;
    public AudioClip enemyPushSound;
    bool jumpHeld;
    public bool noClip;
    public bool hasSpeedBoots = true;
    public bool reverse;
    public AudioClip[] footstepSounds;
    float footstepTime;
    public GameObject col;
    public Rigidbody rb;
    public float MY;
    public GameObject hitmarker;
    float hitmarkerTime;
    public float MX;
    public float speed;
    bool alreadyBelowZero;
    float stamina;
    bool justPressed;
    public Text enemyText;
    float startTime;
    public Text healthText;
    float foundTime;
    public GameObject header;
    public float money;
    public Text moneyText;
    public float slowSpeedTime;
    public gameConsole c;
    public bool nightTime;
    public Canvas[] hideCanvas;
    bool lastNoClip;
    public bool inMud;
    public bool inIce;
    public GameObject[] enableStart;
    public Canvas[] trailerCanvas;
    public float sens;
    public Camera mainCam;
    Vector3 velocity;
    float knockCooldown;

    private Transform myTransform;

    public void checkTitle()
    {
        Debug.Log("TITLE IS " + PlayerPrefs.GetString("MenuWorldTitle"));
        if (title && PlayerPrefs.GetInt("Missions") != 1 && !PlayerPrefs.HasKey("Entered" + PlayerPrefs.GetString("MenuWorldTitle")))
        {
            Instantiate(title);
            PlayerPrefs.SetInt("Entered" + PlayerPrefs.GetString("MenuWorldTitle"), 1);
            FindObjectOfType<FadeTitleText>().t.text = PlayerPrefs.GetString("MenuWorldTitle");
            PlayerPrefs.Save();
            Destroy(title, 6);
        }
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        Time.timeScale = 1;
        myTransform = transform;
        if (FindObjectOfType<billboardOBJ>())
        {
            foreach (billboardOBJ o in FindObjectsOfType<billboardOBJ>()) {
                o.initBBJ();
                o.setPlayer(gameObject);
        }
        }
        flipped = 1;
        if (PlayerPrefs.GetInt("MasterQuest") == 1 && !(PlayerPrefs.GetInt("Missions") == 1 && PlayerPrefs.GetInt("BossRush") == 0))
            flipped = -1;
        flip.SetActive(PlayerPrefs.GetInt("MasterQuest") == 1 && !(PlayerPrefs.GetInt("Missions") == 1 && PlayerPrefs.GetInt("BossRush") == 0));
        sens = PlayerPrefs.GetFloat("MouseSens") * flipped;

        foreach (AudioReverbZone z in FindObjectsOfType<AudioReverbZone>())
            Destroy(z);
        if (s)
            s.init();
        myTransform.position += Vector3.up * 2;
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 100);
            Debug.Log("NO KEY");
        }
        if (!PlayerPrefs.HasKey("VoiceVolume"))
        {
            PlayerPrefs.SetFloat("VoiceVolume", 100);
            Debug.Log("NO KEY");
        }
        PlayerPrefs.Save();
        FindObjectOfType<PlayerVoices>().updateSounds();

        PlayerPrefs.SetFloat("LevelStartTime", Time.realtimeSinceStartup);
        PlayerPrefs.SetFloat("LevelStartCash", 0);
        PlayerPrefs.SetFloat("LevelKills", 0);
        PlayerPrefs.Save();

        knockCooldown = 0;
        if (!PlayerPrefs.HasKey("MouseSens"))
            PlayerPrefs.SetFloat("MouseSens", 10);
        startSpawnPos = myTransform.position;
        

        if (SceneManager.GetActiveScene().name == "MapEditor")
            PlayerPrefs.SetInt("Missions", 1);
        startTime = Time.realtimeSinceStartup + 0.1f;
        if (PlayerPrefs.GetInt("Missions") != 1 && SceneManager.GetActiveScene().name != "MapEditor")
        {
            PlayerPrefs.SetString("LockScene", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();


            if (PlayerPrefs.GetInt("PlayedLevel" + SceneManager.GetActiveScene().name) != 1)
            {
                PlayerPrefs.SetInt("PlayedLevel" + SceneManager.GetActiveScene().name, 1);
                if (PlayerPrefs.GetInt("Missions") != 1) { PlayerPrefs.Save(); }
            }
        }
        
        mainCam = Camera.main;
        foreach (GameObject g in enableStart)
        {
            g.SetActive(true);
        }
        checkPowerUps();
        MX = myTransform.localEulerAngles.y;
        MY = 0;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        checkTitle();
    }
    public void ToggleTrailerCanvas()
    {
        foreach (Canvas c in trailerCanvas)
            c.enabled = !c.enabled;
    }
    public void checkPowerUps()
    {
        hasSpeedBoots = PlayerPrefs.GetFloat("SpeedBoots") == 1;
        money = PlayerPrefs.GetFloat("Money");
    }
    public void hitSound()
    {
        hitmarkerTime = Time.realtimeSinceStartup + 0.1f;
        GetComponent<AudioSource>().PlayOneShot(EnemyhitSound);
    }
    public void refillHealth()
    {
        healTime = Time.realtimeSinceStartup + 0.2f;
        FindObjectOfType<playerHealth>().health += 10;
        if (FindObjectOfType<playerHealth>().health > PlayerPrefs.GetFloat("MaxHealth"))
            FindObjectOfType<playerHealth>().health = PlayerPrefs.GetFloat("MaxHealth");
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup > checkTime)
        {
            AudioListener.volume = PlayerPrefs.GetFloat("vol") / 100;
        }
        if (skip)
            return;
        
            if (noClip)
        {
            if (hideCanvas.Length != 0)
            {
                foreach (Canvas c in hideCanvas)
                    c.enabled = false;
            }
            GetComponent<Rigidbody>().velocity = 25 * (transform.forward * Input.GetAxis("Vertical") + myTransform.right * Input.GetAxis("Horizontal") * flipped);
            GetComponent<Rigidbody>().useGravity = false;
            myTransform.localEulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")) * PlayerPrefs.GetFloat("MouseSens") * flipped;
            return;
        }
        if (Time.realtimeSinceStartup > checkTime)
        {
            sens = PlayerPrefs.GetFloat("MouseSens");
            checkTime = Time.realtimeSinceStartup + 0.1f;
        }
        if (!Input.GetKey(PlayerPrefs.GetString("JumpKeybind")))
            jumpHeld = false;
        if (Time.realtimeSinceStartup < startTime + 0.5f)
        {
            if (SceneManager.GetActiveScene().name == "Hub1" && PlayerPrefs.HasKey("SpawnX") && PlayerPrefs.GetInt("ShardCount") != 0 && PlayerPrefs.GetFloat("SpawnX") != 0)
                myTransform.position = new Vector3(PlayerPrefs.GetFloat("SpawnX"), PlayerPrefs.GetFloat("SpawnY"), PlayerPrefs.GetFloat("SpawnZ"));
        }
        if (Input.GetKey(PlayerPrefs.GetString("JumpKeybind")) && !jumpHeld)
        {
            if (Physics.Raycast(transform.position, Vector3.down, groundDistance + 0.2f))
            {
                if (!FindObjectOfType<WeaponsAnim>().GetComponent<Animator>().GetCurrentAnimatorStateInfo(2).IsName("Jump"))
                {
                    jumpHeld = true;
                    jumpTime = Time.realtimeSinceStartup + 0.6f;
                    Vector3 vel = GetComponent<Rigidbody>().velocity;
                    vel.y = 10;
                    FindObjectOfType<WeaponsAnim>().GetComponent<Animator>().Play("Jump", 2);
                    GetComponent<Rigidbody>().velocity = vel;
                }
            }
        }
        
        if (dollActive)
        {
            wepCanvas.transform.localScale = new Vector3(0, 0, 0);
            flashCanvas.SetActive(false);
            Time.timeScale = 0.00012f;
            return;
        }
        else if (Time.timeScale == 0.00012f)
            Time.timeScale = 1;
        wepCanvas.transform.localScale = new Vector3(0, 0, 0);
        if (flashAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            wepCanvas.transform.localScale = new Vector3(1, 1, 1);
        flashCanvas.SetActive(!flashAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle"));
        healImage.SetActive(Time.realtimeSinceStartup < healTime);
        if (Input.GetKey(PlayerPrefs.GetString("MeleeKeybind")) && knockCooldown > 0)
        {
            knockCooldown -= 20;
            FindObjectOfType<WeaponsAnim>().GetComponent<Animator>().Play("Knock", 1);
            Collider[] hitColliders = Physics.OverlapSphere(transform.position + myTransform.forward * 4, 8);
            foreach (Collider c in hitColliders)
            {
                Debug.Log("WHIP ENEMY INSIDE");
                if (c.transform.gameObject.GetComponent<Rigidbody>() && !c.gameObject.GetComponent<InstaKill>())
                {
                    if (c.transform.gameObject.GetComponent<enemyHealth>())
                    {
                        FindObjectOfType<PlayerVoices>().Push();
                        c.transform.gameObject.GetComponent<enemyHealth>().stunTime = Time.realtimeSinceStartup + 0.2f;
                        c.transform.gameObject.GetComponent<enemyHealth>().colorTime = Time.realtimeSinceStartup + 0.3f;
                        if (c.transform.gameObject.GetComponent<enemyHealth>().currentHealth > 0.7f)
                            c.transform.gameObject.GetComponent<enemyHealth>().currentHealth = 0.7f;
                    }
                    GetComponent<AudioSource>().PlayOneShot(enemyPushSound);
                    GameObject c2 = c.transform.gameObject;
                    Vector3 push = Vector3.MoveTowards(transform.position, c.gameObject.transform.position, 0.1f);
                    push.y = 0;
                    
                    c2.GetComponent<Rigidbody>().AddForce(transform.forward*35, ForceMode.Impulse);
                }
            }
        }
        if (knockCooldown < 40)
            knockCooldown += Time.deltaTime * 10;
        hitmarker.SetActive(Time.realtimeSinceStartup < hitmarkerTime);
        if (weaponsAnim.GetCurrentAnimatorStateInfo(0).IsName("SniperAim") || weaponsAnim.GetCurrentAnimatorStateInfo(0).IsName("SniperAimFire"))
            mainCam.fieldOfView = PlayerPrefs.GetFloat("FOV")/3;
        else
            mainCam.fieldOfView = PlayerPrefs.GetFloat("FOV") + ((transform.position.y-mainCam.transform.position.y)*-5)/3;
        if (c.gameObject.activeInHierarchy)
        {

            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            return;
        }
        if (!noClip && lastNoClip)
        {
            if (hideCanvas.Length != 0)
            {
                foreach (Canvas c in hideCanvas)
                    c.enabled = true;
            }
            GetComponent<Rigidbody>().useGravity = true;
        }
        lastNoClip = noClip;
        if (money == 0)
            moneyText.text = "";
        else
            moneyText.text = "$" + money;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, myTransform.forward, out hit)) {
            GameObject g = hit.transform.gameObject;
            if (g.GetComponent<enemyHealth>())
            {
                if (!g.GetComponent<enemyHealth>().dontshow)
                {
                    string name = g.name;
                    if (g.GetComponent<enemyHealth>().enemyName != "")
                        name = g.GetComponent<enemyHealth>().enemyName;
                    if (name != "")
                    {
                        enemyText.text = name;
                        int health = (int)(g.GetComponent<enemyHealth>().currentHealth * 100);
                        healthText.text = "" + health;
                        foundTime = Time.realtimeSinceStartup + 1;
                        if (health <= 0)
                        {
                            healthText.text = "";
                            foundTime = Time.realtimeSinceStartup - 1;
                        }

                        header.SetActive(true);
                    }
                }
            }
        }
        if (Time.realtimeSinceStartup > foundTime)
        {
            enemyText.text = "";
            header.SetActive(false);
        }
        int rv = 1;
        if (reverse)
        {
            rv = -1;
        }
        col.transform.eulerAngles = new Vector3();
        Vector3 angles = myTransform.localEulerAngles;
        myTransform.Rotate(-transform.localEulerAngles.x, 0, 0);
        float vely = GetComponent<Rigidbody>().velocity.y;
        speed = 1;
        if (PlayerPrefs.GetInt("ToggleRun") == 1)
        {
            if (Input.GetKeyDown(PlayerPrefs.GetString("RunKeybind")))
            running = !running;
        }
        else
        {
            running = Input.GetKey(PlayerPrefs.GetString("RunKeybind"));
        }
            finalRun = running;
        if (PlayerPrefs.GetInt("ShiftWalk") == 1)
            finalRun = !running;
        if (finalRun)
        {
            justPressed = true;
               speed = 2;
            stamina -= 0;
        }
        else
        {
            if (justPressed)
                stamina = 0;
                justPressed = false;
                stamina += Time.deltaTime / 2;
        }
        speed *= 1.5f;
        if (hasSpeedBoots)
            speed *= 1.5f;
        if (inMud)
            speed /= 5;
        if (inIce)
            speed *= 1.2f;
        angles = new Vector3(0, MX, 0);
        myTransform.localEulerAngles = angles;
        Vector3 moveDir = (Input.GetAxis("Vertical") * myTransform.forward + Input.GetAxis("Horizontal") * flipped * myTransform.right);
        moveDir.y = 0;
        if (moveDir != new Vector3() && Time.realtimeSinceStartup > footstepTime)
        {
            footstepTime = Time.realtimeSinceStartup + 1.3f/(speed/1.5f);
            GetComponent<AudioSource>().pitch = Random.Range(0.95f, 1.15f);
            GetComponent<AudioSource>().PlayOneShot(footstepSounds[Random.Range(1, footstepSounds.Length)]);
        }
        moveDir = Vector3.MoveTowards(new Vector3(), moveDir, 1);
        if (Time.realtimeSinceStartup < slowSpeedTime)
            moveDir /= 2;
        moveDir = (moveDir * 5) * speed * rv;
        GetComponent<Rigidbody>().velocity= moveDir;
        GetComponent<Rigidbody>().velocity += Vector3.up * vely;
        float MSensM = 0.5f;
        if (Input.GetMouseButton(1))
            MSensM = 0.3f;
        MY += -Input.GetAxis("Mouse Y") * 6 * MSensM * rv * sens/15;
        MX += Input.GetAxis("Mouse X") * 6 * MSensM * rv * flipped * sens / 15;
        MY = Mathf.Min(60, Mathf.Max(-60, MY));
        angles = new Vector3(MY, MX, -Input.GetAxis("Horizontal"));
        myTransform.localEulerAngles = angles;
        
    }
}
