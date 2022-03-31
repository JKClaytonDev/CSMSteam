using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreScript : MonoBehaviour
{

    public GameObject healing;
    public GameObject flash;
    float money;
    public GameObject canvas;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            canvas.SetActive(true);
            FindObjectOfType<PlayerMovement>().enabled = false;
            FindObjectOfType<PlayerAnimations>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    void check()
    {
        money = FindObjectOfType<PlayerMovement>().money;

        healing.SetActive(PlayerPrefs.GetFloat("Healing") != 1);
        flash.SetActive(PlayerPrefs.GetFloat("Flash") != 1);
    }
    // Update is called once per frame
    void Update()
    {
        check();
        
    }
    public void close()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        FindObjectOfType<PlayerMovement>().enabled = true;
        FindObjectOfType<PlayerAnimations>().enabled = true;
        canvas.SetActive(false);
    }
    public void buyFlashlight()
    {
        check();
        if (money > 300)
        {
            PlayerPrefs.SetFloat("SpeedBoots", 1);
            money -= 300;
        }
        FindObjectOfType<PlayerMovement>().money = money;
        if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}
        FindObjectOfType<PlayerMovement>().checkPowerUps();
    }
    public void buyHealing()
    {
        check();
        if (money > 600)
        {
            PlayerPrefs.SetFloat("Healing", 1);
            money -= 600;
        }
        FindObjectOfType<PlayerMovement>().money = money;
        if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}
        FindObjectOfType<PlayerMovement>().checkPowerUps();
    }
}
