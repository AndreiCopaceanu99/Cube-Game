using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

// It is responable with changing the brightness in the game
public class Brightness : MonoBehaviour
{
    [SerializeField] PostProcessVolume Post_Process_Volume;

    ColorGrading Color_Grading;
    void Start()
    {
        Post_Process_Volume.profile.TryGetSettings(out Color_Grading);
    }

    // Sets the post exposure value accordingly with the slider
    public void Set_Brightness(float Brightness)
    {
        Color_Grading.postExposure.value = Brightness;
    }
}
