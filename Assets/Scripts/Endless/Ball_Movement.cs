using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is responsible for the ball movement
public class Ball_Movement : MonoBehaviour
{
    [SerializeField] float Speed;

    Rigidbody rb;

    public bool On_Cube = false;
    
    float Horizontal_Movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //It positions the ball on top of the screen in the centre
        transform.position = new Vector3(0, 4, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Takes the inputs from the player and applies the movement to the ball
        Horizontal_Movement = Input.acceleration.x * Speed * Time.fixedDeltaTime;
        // It limits the amount of speed the ball can have
        if ((rb.velocity.magnitude <= 7f && On_Cube) || (!On_Cube && rb.velocity.magnitude <= 17f))
        {
            rb.AddForce(Horizontal_Movement, 0, 0);
        }
        // It rotates the ball on the moving direction
        transform.Rotate(Input.GetAxis("Horizontal") * Time.fixedDeltaTime, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Checks if the ball is on the platform
        if(collision.gameObject.tag == "Cubes")
        {
            On_Cube = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        // Checks if the ball is no longer on the platform
        if (collision.gameObject.tag == "Cubes")
        {
            On_Cube = false;
        }
    }
}
