using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// It is responsable with moving the platforms
public class Cubes_Movement : MonoBehaviour
{
    float Speed;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Gives a random speed
        Speed = Random.Range(50, 300);

        // Gives a random direction
        int i = Random.Range(0, 2);
        if(i == 0)
        {
            Speed *= -1;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Moves the platform horizntally based on the speed
        float Horizontal_Movement = Speed * Time.fixedDeltaTime;

        rb.velocity = new Vector3(Horizontal_Movement, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the platform hits the edge of the screen, then it changes direction
        if(collision.gameObject.tag == "Side_Limits")
        {
            Speed *= -1;
        }
    }
}
