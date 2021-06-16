using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// It is responsable with setting up the volume
public class Volume : MonoBehaviour
{
    [SerializeField] AudioMixer Game_Audio;
    // The volume value on the mixer is changed accordinglly with the slider
    public void Set_Volume(float Volume)
    {
        Game_Audio.SetFloat("Volume", Volume);
    }
}
