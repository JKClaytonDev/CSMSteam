using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHitbox : MonoBehaviour
{
    public playerHealth player;
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("JUST HIT " + other);
        if (other.gameObject.layer == 12)
            player.health -= 1;
    }

}
