using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockGun : MonoBehaviour
{
    public int unlockedWeaponNum;
    int index;
    public int optionA;
    public int optionB;
    public bool skip;
    // Start is called before the first frame update
    private void Awake()
    {
        if (!skip)
        {
            unlockedWeaponNum = PlayerPrefs.GetInt("UnlockedIndex" + unlockedWeaponNum);
            index = optionA;
            if (unlockedWeaponNum == -1)
                index = optionB;
            FindObjectOfType<PlayerAnimations>().lockGun = index;
            FindObjectOfType<PlayerAnimations>().equipGun(index);
        }
        else
        {
            FindObjectOfType<PlayerAnimations>().equippedWeapons[index] = true;
            index = optionA;
            FindObjectOfType<PlayerAnimations>().lockGun = index;
            FindObjectOfType<PlayerAnimations>().equipGun(optionA);
            FindObjectOfType<PlayerAnimations>().equipGun(optionB);
        }
        
    }


    
}
