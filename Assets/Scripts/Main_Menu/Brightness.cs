using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Brightness : MonoBehaviour
{
    [SerializeField] PostProcessVolume Post_Process_Volume;

   ColorGrading Color_Grading;
    //[SerializeField] Volume Post_Process_Volume;
    // Start is called before the first frame update
    void Start()
    {
        Post_Process_Volume.profile.TryGetSettings(out Color_Grading);
    }

    public void Set_Brightness(float Brightness)
    {
        Color_Grading.postExposure.value = Brightness;
    }
}
