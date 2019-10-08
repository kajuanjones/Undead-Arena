using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float speedDampening = .5f;

    // Update is called once per frame
    void Update()
    {
        speed = player.GetComponent<JumpMan>().movement.velocity.x * speedDampening;
        Rotate();
    }
    void Rotate()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            //rotate clockwise
            transform.Rotate(Vector3.back * speed);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            //rotate counterclockwise
            transform.Rotate(Vector3.forward * -speed);
        }
           
    }
}
