using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    [SerializeField] float Speed;

    Ball_Movement Ball;

    [SerializeField] Transform Side_Limits;

    // Start is called before the first frame update
    void Start()
    {
        Ball = FindObjectOfType<Ball_Movement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Ball.On_Cube)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, Ball.transform.position.y - 2, -10), Speed * Time.fixedDeltaTime);
        }

        Side_Limits.position = new Vector3(Side_Limits.position.x, transform.position.y, Side_Limits.position.z);
    }
}
