using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
public class playerRot : MonoBehaviour
{
    public Vector3 contacts;
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("FOUND CONTACTS");
        if (collision.collider.gameObject.GetComponent<MeshRenderer>())
        {
            Debug.Log("INSIDE FOUND CONTACTS" + collision.gameObject.name);
            contacts = collision.contacts[0].normal;

            foreach (ContactPoint contact in collision.contacts)
            {
                print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
                // Visualize the contact point
                Debug.DrawRay(contact.point, contact.normal, Color.white);
            }
        }
    }


}
