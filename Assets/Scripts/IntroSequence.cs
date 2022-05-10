using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
public class IntroSequence : MonoBehaviour
{

    bool play = false;
    

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<VideoPlayer>().isPlaying)
            play = true;
        if (play && !GetComponent<VideoPlayer>().isPlaying) {
            SceneManager.LoadScene("Menu");
            this.enabled = false;
        }
    }
}
