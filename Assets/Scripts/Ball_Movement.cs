using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        transform.position = new Vector3(0, 4, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Horizontal_Movement = Input.GetAxis("Horizontal") * Speed * Time.fixedDeltaTime;
        if ((rb.velocity.magnitude <= 7f && On_Cube) || (!On_Cube && rb.velocity.magnitude <= 17f))
        {
            rb.AddForce(Horizontal_Movement, 0, 0);
        }

        transform.Rotate(Input.GetAxis("Horizontal") * Time.fixedDeltaTime, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Cubes")
        {
            On_Cube = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Cubes")
        {
            On_Cube = false;
        }
    }
}
