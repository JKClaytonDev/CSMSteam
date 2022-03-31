using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipWeapon : MonoBehaviour
{
    public GameObject[] weapons;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        weapons[PlayerPrefs.GetInt("UnlockWeapon")].SetActive(true);
        
    }


}
