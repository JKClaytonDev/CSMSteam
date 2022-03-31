using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(Random.Range(1, 15) == 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
