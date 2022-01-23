using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Intro : MonoBehaviour
{
    [SerializeField] VideoPlayer Intro_Video;
    bool Change_Scene = true;
    // Update is called once per frame
    void Update()
    {
        Intro_Video.loopPointReached += EndReached;
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        if (Change_Scene)
        {
            SceneManager.LoadScene("Login");
            Change_Scene = false;
        }
    }
}
