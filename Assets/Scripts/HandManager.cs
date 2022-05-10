using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public GameObject[] hands;
    float handTime;
    // Start is called before the first frame update
    void Start()
    {
        handTime = Time.realtimeSinceStartup+3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup > handTime)
        {
            //Debug.Log("HAND");
            handTime = Time.realtimeSinceStartup + 10;
            GameObject hand = hands[Random.Range(0, hands.Length - 1)];
            GameObject h = Instantiate(hand);
            h.transform.parent = null;
            h.transform.position = hand.transform.position;
            h.SetActive(true);
        }
    }
}
