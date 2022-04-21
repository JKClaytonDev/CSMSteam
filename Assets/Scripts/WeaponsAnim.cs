//
// MIT License
//
// Copyright (c) 2022 Jackson Kenneth Clayton
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponsAnim : MonoBehaviour
{
    public GameObject noAmmo;
    PlayerAnimations p;
    public GameObject whip;
    PlayerMovement player;
    public AudioClip hitmarkerSound;
    public AudioClip killSound;
    public AudioClip[] gunSounds;
    float whipTime;
    public GameObject playerScript;
    public int[] bulletCount;
    public GameObject hitPrefab;
    public float hitPrefabTime;
    public GameObject bullet;
    public GameObject bomb;
    public int weaponNum;
    public LineRenderer R;
    public Canvas cVAS;
    public bool reverse;
    public GameObject cam;
    public float screenShake;
    public GameObject snark;
    public int[] startAmmo;
    public int[] ammo;
    public Text ammoText;
    public int playerAmmo;
    PlayerAnimations anim;
    // Start is called before the first frame update
    public void Start()
    {
        playerScript = FindObjectOfType<PlayerMovement>().gameObject;
        player = playerScript.GetComponent<PlayerMovement>();
        anim = FindObjectOfType<PlayerAnimations>();
        if (!p)
            p = FindObjectOfType<PlayerAnimations>();
        refillAmmo();
        // new event created
        AnimationEvent evt;
        evt = new AnimationEvent();
        evt.functionName = "shoot";
        AnimationEvent evt2;
        evt2 = new AnimationEvent();
        evt.functionName = "drink";
        GetComponent<Animator>().speed = 4;
    }
    public void drink()
    {
        Debug.Log("DRINK");
        FindObjectOfType<PlayerAnimations>().goblin = true;
    }
    public void refillAmmo()
    {
        for (int i = 0; i < startAmmo.Length; i++)
            ammo[i] = startAmmo[i];
    }
    void Update()
    {
        weaponNum = anim.wepIndex;
        noAmmo.SetActive(playerAmmo <= 0);
        playerAmmo = (Mathf.CeilToInt(((float)ammo[weaponNum] / (float)startAmmo[weaponNum]) * 10));
        ammoText.text = "";
        if (weaponNum == 9)
        {
            playerAmmo = 1;
        }
        else
        {
            for (int i = 0; i < playerAmmo; i++)
                ammoText.text += "|";
        }
        cam.transform.localEulerAngles = 3*new Vector3(Random.Range(-screenShake, screenShake), Random.Range(-screenShake, screenShake), Random.Range(-screenShake, screenShake));
        screenShake /= 1 + Time.deltaTime*5;
    }
    public void shoot()
    {
        if (player.GetComponent<PlayerMovement>().dollActive)
            return;
        if (!FindObjectOfType<PlayerAnimations>().whipFire && !FindObjectOfType<PlayerAnimations>().gunAnim.GetCurrentAnimatorStateInfo(0).IsName("WhipFire"))
        {
            if (p.goblin)
            {
                GameObject k = Instantiate(bomb);
                k.transform.position = playerScript.transform.position + playerScript.transform.forward * 3;
                k.GetComponent<Rigidbody>().velocity = playerScript.transform.forward * 75;
                return;
            }
            if (playerAmmo > 0 && !(weaponNum == 9 || FindObjectOfType<PlayerAnimations>().whipFire))
                ammo[weaponNum]--;
            else if (!(playerAmmo > 0))
            {
                FindObjectOfType<PlayerVoices>().NoAmmo();
                return;
            }
            if (!cVAS.enabled)
                return;
        }
        FindObjectOfType<PlayerAnimations>().whipFire = false;


        if (weaponNum == 2)
        {
            screenShake = 0.1f;
            player.transform.Rotate(0, -5, 0);
            fire(new Vector2(0.3f, 0.1f));
            player.transform.Rotate(0, 5, 0);
            fire(new Vector2(0.3f, 0.1f));
            player.transform.Rotate(0, 5, 0);
            fire(new Vector2(0.3f, 0.1f));
            player.transform.Rotate(0, -5, 0);
            return;
        }
        if (weaponNum == 8)
        {
            screenShake = 0.1f;
            player.transform.Rotate(0, -1, 0);
            fire(new Vector2(0.3f, 0.1f));
            player.transform.Rotate(0, 1, 0);
            fire(new Vector2(0.3f, 0.1f));
            player.transform.Rotate(0, 1, 0);
            fire(new Vector2(0.3f, 0.1f));
            player.transform.Rotate(0, -1, 0);
            return;
        }
        if (weaponNum == 3)
        {
            
            fire(new Vector2(0.3f, 0.1f));
            fire(new Vector2(-0.3f, 0.1f));
            return;
        }
        if (weaponNum == 4)
        {
            screenShake = 0.1f;
            player.transform.Rotate(0, -5, 0);
            fire(new Vector2(0.3f, 0.1f));
            player.transform.Rotate(0, 5, 0);
            fire(new Vector2(0.3f, 0.1f));
            player.transform.Rotate(0, 5, 0);
            fire(new Vector2(0.3f, 0.1f));
            player.transform.Rotate(0, -5, 0);
            player.transform.Rotate(0, -10, 0);
            fire(new Vector2(0.3f, 0.1f));
            player.transform.Rotate(0, 10, 0);
            fire(new Vector2(0.3f, 0.1f));
            player.transform.Rotate(0, 10, 0);
            fire(new Vector2(0.3f, 0.1f));
            player.transform.Rotate(0, -10, 0);
            return;
        }
        if (weaponNum == 5)
        {
            screenShake = 0.3f;
            fire(new Vector2(0.3f, 0.1f));

            return;
        }
        if (weaponNum == 6)
        {
            screenShake = 1;
            fire(new Vector2(0.3f, 0.1f));

            return;
        }
        if (weaponNum == 7)
        {
            screenShake = 2;
            fire(new Vector2(0.3f, 0.1f));

            return;
        }
        if (weaponNum == 12)
        {
            screenShake = 0.1f;
            player.transform.Rotate(0, -5, 0);
            fire(new Vector2(0.3f, 0.1f));
            player.transform.Rotate(0, 5, 0);
            fire(new Vector2(0.3f, 0.1f));
            player.transform.Rotate(0, 5, 0);
            fire(new Vector2(0.3f, 0.1f));
            player.transform.Rotate(0, -5, 0);
            player.transform.Rotate(0, -10, 0);
            fire(new Vector2(0.3f, 0.1f));
            player.transform.Rotate(0, 10, 0);
            fire(new Vector2(0.3f, 0.1f));
            player.transform.Rotate(0, 10, 0);
            fire(new Vector2(0.3f, 0.1f));
            player.transform.Rotate(0, -10, 0);
            return;
        }
        fire(new Vector2(0.3f, 0.1f));

    }
    void fire(Vector2 cbp)
    {
        if (PlayerPrefs.GetInt("MasterQuest") == 1 && PlayerPrefs.GetInt("Missions") != 1)
            cbp.x *= -1;
        if (Time.realtimeSinceStartup < whipTime)
            return;
        Debug.Log("FIRE");
        if (weaponNum == 9 || FindObjectOfType<PlayerAnimations>().whipFire || FindObjectOfType<PlayerAnimations>().gunAnim.GetCurrentAnimatorStateInfo(0).IsName("WhipFire"))
        {
            whipTime = Time.realtimeSinceStartup + 0.1f;
            GetComponent<AudioSource>().PlayOneShot(gunSounds[9]);
            FindObjectOfType<PlayerAnimations>().whipFire = false;
            GameObject k = Instantiate(whip);
            k.transform.position = player.transform.position + player.transform.forward - Vector3.up / 9;
            k.transform.rotation = player.transform.rotation;
            k.transform.Rotate(0, 0, 45);
            Destroy(k, 1);
            Debug.Log("WHIP CHECK");
            Collider[] hitColliders = Physics.OverlapSphere(player.transform.position + player.transform.forward * 4, 8);
            foreach (Collider c in hitColliders)
            {
                Debug.Log("WHIP ENEMY INSIDE");
                if (c.transform.gameObject.GetComponent<enemyHealth>())
                {
                    c.transform.gameObject.GetComponent<enemyHealth>().stunTime = Time.realtimeSinceStartup + 0.2f;
                    c.transform.gameObject.GetComponent<enemyHealth>().colorTime = Time.realtimeSinceStartup + 0.3f;
                    Debug.Log("ENEMY WHIP");
                    if (c.transform.gameObject.GetComponent<ZombieScript>())
                    {
                        if (c.transform.gameObject.GetComponent<ZombieScript>().skullFrag)
                        {
                            c.transform.gameObject.GetComponent<enemyHealth>().currentHealth /= 2;
                            if (c.transform.gameObject.GetComponent<enemyHealth>().currentHealth < 0.2f)
                            {
                                GameObject skullFrag = Instantiate(c.transform.gameObject.GetComponent<ZombieScript>().skullFrag);
                                skullFrag.transform.position = c.transform.position;
                                Destroy(c.transform.gameObject);
                                return;
                            }
                        }
                    }
                    c.transform.position -= transform.forward;
                    Debug.Log("bullet hit");
                    if (c.transform.gameObject.GetComponent<enemyHealth>().hitSound)
                        FindObjectOfType<UniversalAudio>().playSound(c.transform.gameObject.GetComponent<enemyHealth>().hitSound);
                    Debug.Log("HIT ENEMY");
                    enemyHealth he = c.transform.gameObject.GetComponent<enemyHealth>();
                    he.currentHealth -= he.damages[1] * 2;
                    if (he.currentHealth >= 0)
                        player.GetComponent<AudioSource>().PlayOneShot(hitmarkerSound, 1);
                    else
                        player.GetComponent<AudioSource>().PlayOneShot(killSound, 1);
                    if (he.bloodObj)
                    {
                        GameObject f = Instantiate(he.bloodObj);
                        Destroy(f, he.bloodTime);
                    }
                }
            }
            return;
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(gunSounds[weaponNum]);

            Debug.Log("firing");
            GameObject g = Instantiate(bullet);


            RaycastHit hit;
            if (!Physics.Raycast(player.transform.position, player.transform.forward, out hit, 150))
                return;
            if (weaponNum == 4)
            {
                g.GetComponent<playerBullet>().arrow = true;
                cbp.y = 1f;
            }
            if (weaponNum == 5)
            {
                g.GetComponent<playerBullet>().AlienBullet = true;
            }
            if (weaponNum == 6)
            {
                g.GetComponent<playerBullet>().RocketLauncher = true;
            }
            if (weaponNum == 7)
            {
                g.GetComponent<playerBullet>().Axe = true;
            }
            if (weaponNum == 8)
            {
                g.GetComponent<playerBullet>().water = true;
                g.GetComponent<Rigidbody>().useGravity = true;
                g.GetComponent<SphereCollider>().radius *= 3;
            }
            if (weaponNum == 11)
            {
                g.GetComponent<playerBullet>().sniper = true;
            }
            if (weaponNum == 12)
            {
                g.GetComponent<playerBullet>().doll = true;
            }
            g.transform.position = Vector3.MoveTowards(player.transform.position + (player.transform.right * cbp.x) - (player.transform.up * cbp.y), hit.point, 1);
            g.GetComponent<playerBullet>().vel = (g.transform.position - (player.transform.position + (player.transform.right * cbp.x) - (player.transform.up * cbp.y))) * 10;
            g.GetComponent<playerBullet>().player = this;
            if (reverse)
            {
                g.GetComponent<playerBullet>().reverse = true;
                g.GetComponent<playerBullet>().startPos = hit.point;
            }
            GameObject h = Instantiate(hitPrefab);
            h.transform.parent = null;
            h.transform.position = hit.point - transform.forward * 0.1f;
            Destroy(h, hitPrefabTime);
        }
    }
}
