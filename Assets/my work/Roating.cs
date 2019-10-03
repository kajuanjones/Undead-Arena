using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roating : MonoBehaviour
{
    public int Directionmulti;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
            RotateLeft();
    }
    void RotateLeft()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            //rotate counterclockwise
            transform.Rotate(Vector3.back);
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            //rotate clockwise
            transform.Rotate(Vector3.forward);
        }
           
    }
}
