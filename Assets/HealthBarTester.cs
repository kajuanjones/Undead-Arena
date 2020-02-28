using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarTester : MonoBehaviour
{
    private HealthBarTester healthBar;
    
    void Awake()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBarController>();

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            healthBar.changeHP(1);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            healthBar.change(-1);
        }
    }
}
