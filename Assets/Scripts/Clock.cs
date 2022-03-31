using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Clock : MonoBehaviour
{
    public string scene;
    // Start is called before the first frame update
    public void loadClockLevel()
    {
        Camera.main.transform.LookAt(gameObject.transform);
        Camera.main.fieldOfView = 10;
        SceneManager.LoadScene(scene);
    }
}
