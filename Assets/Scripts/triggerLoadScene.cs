using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class triggerLoadScene : MonoBehaviour
{
    public string scene;
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<WolfMovement>())
        {
            string scenee = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene(scene);
        }
    }

}
