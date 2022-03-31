using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGunTrigger : MonoBehaviour
{
    public int index;
    public bool dontEquip;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            PlayerPrefs.SetInt("EquippedWeapon" + index, 1);
            if (PlayerPrefs.GetInt("Missions") != 1){PlayerPrefs.Save();}
            if (!dontEquip)
            FindObjectOfType<PlayerAnimations>().equipGun(index);
            Destroy(gameObject);
        }
    }
    
}
