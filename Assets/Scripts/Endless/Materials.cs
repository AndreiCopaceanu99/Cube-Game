using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Materials : MonoBehaviour
{
    [SerializeField] GameObject Ball;
    [SerializeField] GameObject[] Platforms;
    [SerializeField] Image Background;

    [SerializeField] Material[] Ball_Materials;
    [SerializeField] Material[] Platform_Materials;
    [SerializeField] Sprite[] Background_Images;

    Manager Manager;

    // Start is called before the first frame update
    void Start()
    {
        Manager = FindObjectOfType<Manager>();

        Ball.GetComponent<MeshRenderer>().material = Ball_Materials[Persistent_Data.PD.My_Ball];
        foreach(GameObject GO in Platforms)
        {
            GO.GetComponent<MeshRenderer>().material = Platform_Materials[Persistent_Data.PD.My_Platform];
        }
        Background.sprite = Background_Images[Persistent_Data.PD.My_Background];
    }
}
