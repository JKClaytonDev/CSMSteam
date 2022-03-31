using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class introAnim : MonoBehaviour
{
    public Canvas canvas1;
    public Canvas canvas2;
    AnimationEvent e;
    AnimationEvent e2;
    Vector3 startAngles;
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Hub1")
        {
            gameObject.SetActive(false);
            return;
        }
        transform.parent = null;
        Vector3 angles = transform.localEulerAngles;
        angles.x = 0;
        angles.z = 0;
        transform.localEulerAngles = angles;
        startAngles = FindObjectOfType<PlayerMovement>().transform.eulerAngles;
        string scene = SceneManager.GetActiveScene().name;
        gameObject.SetActive(scene.ToCharArray()[scene.Length-1] == '1' || scene.ToCharArray()[scene.Length - 1] == '4');
        if (scene.ToCharArray()[scene.Length - 1] == '4')
            GetComponent<Animator>().speed = 3;
        e = new AnimationEvent();
        e.functionName = "enableCanvas";
        e2 = new AnimationEvent();
        e2.functionName = "disableCanvas";
    }
    public void enableCanvas()
    {
        canvas1.enabled = true;
        canvas2.enabled = true;
        FindObjectOfType<PlayerMovement>().transform.eulerAngles = startAngles;
    }
    public void disableCanvas()
    {
        canvas1.enabled = false;
        canvas2.enabled = false;
    }
}
