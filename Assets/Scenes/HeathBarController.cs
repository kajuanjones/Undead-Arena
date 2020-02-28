using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathBarController : MonoBehaviour
{
    private Slider healthbar;
    private int currentHP = 100;
    
    void Awake()
    {
        healthbar = GetComponent<Slider>();
       
    }

    void Update()
    {
        healthbar.value = currentHP;
    }

    public void changeHP(int dHP)
    {
        currentHP += dHP;
    }

}
