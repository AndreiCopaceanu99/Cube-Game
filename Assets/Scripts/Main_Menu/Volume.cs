using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
    [SerializeField] AudioMixer Game_Audio;
    public void Set_Volume(float Volume)
    {
        Game_Audio.SetFloat("Volume", Volume);
    }
}
