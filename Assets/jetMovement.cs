using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jetMovement : MonoBehaviour
{
    public float amp = 0.01f;
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate( -20 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(20 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -50 * Time.deltaTime, 10 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 50 * Time.deltaTime, -10 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, 0, 30 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, 0, -30 * Time.deltaTime);
        }
        if (Input.GetMouseButtonDown(0))
        {
            amp *= 2;
        }
        if (Input.GetMouseButtonDown(1))
        {
            amp /= 2;
        }
        
        transform.position += amp * transform.forward ;

        

    }
}
