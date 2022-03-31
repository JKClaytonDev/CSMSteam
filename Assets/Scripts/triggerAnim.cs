using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerAnim : MonoBehaviour
{
    public Animator a;
    public string aName;
    public bool justDisable;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            a.Play(aName);
            if (!justDisable)
                Destroy(gameObject);
        }
    }
}
