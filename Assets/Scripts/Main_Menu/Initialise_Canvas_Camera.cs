using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IS responsable with putting the canvas on the camera
public class Initialise_Canvas_Camera : MonoBehaviour
{
    [SerializeField] GameObject Camera;
    [SerializeField] Canvas Canvas;

    [SerializeField] float Offset_Plane_Distance = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        Canvas.worldCamera = Camera.gameObject.GetComponent<Camera>();
        Canvas.planeDistance = Camera.gameObject.GetComponent<Camera>().nearClipPlane + Offset_Plane_Distance;
    }
}
