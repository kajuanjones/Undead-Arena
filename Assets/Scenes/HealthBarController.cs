using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Slider HealthBar;
    private float Health = 150f;

    void Start()
    {
        HealthBar.maxValue = Health;
    }

    public void GetHit(float hitAmount)
    {
        Health -= hitAmount;
        if (Health <= 0) Health = 0;
        HealthBar.value = Health;
    }
}
