using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SceneMusicTrack : IComparable<SceneMusicTrack>
{
    public string scene;
    public AudioClip music;

    public SceneMusicTrack(string newScene, AudioClip newMusic)
    {
        scene = newScene;
        music = newMusic;
    }

    //This method is required by the IComparable
    //interface. 
    public int CompareTo(SceneMusicTrack other)
    {
        //Return the difference in power.
        return 0;
    }

}
