using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attachedHealth : MonoBehaviour
{
    public playerHealth p;
    private void Start()
    {
        p = FindObjectOfType<playerHealth>();
    }
}
