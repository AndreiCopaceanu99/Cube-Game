using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Materials : MonoBehaviour
{
    [SerializeField] GameObject Ball;
    [SerializeField] GameObject[] Platforms;
    [SerializeField] Image Background;
    [SerializeField] Image[] Button;

    [SerializeField] Material[] Ball_Materials;
    [SerializeField] Material[] Platform_Materials;
    [SerializeField] Sprite[] Background_Images;

    [SerializeField] UI.UI_Elements[] ui;

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
        Background.sprite = ui[Persistent_Data.PD.My_Background].Background;
        foreach (Image button in Button)
        {
            button.sprite = ui[Persistent_Data.PD.My_Background].Long_Button;
        }
    }
}
