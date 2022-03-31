using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveMoney : MonoBehaviour
{
    private void OnEnable()
    {
        FindObjectOfType<PlayerMovement>().money -= 30;
        if (FindObjectOfType<PlayerMovement>().money < 0)
        {
            FindObjectOfType<PlayerMovement>().money = 0;
        }
    }
}
