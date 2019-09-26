using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Temp : MonoBehaviour
{
    public void PlaySwordSlash(){
        SFX_II.instance.Play("SwordSlash");
    }

    public AudioSource footstep;

    public void PlayFootStep(){
        footstep.Play();
    }

    public void PlayClothFoley(){
        SFX_II.instance.Play("ClothFoley");
    }

    public void PlayLanding(){
        SFX_II.instance.Play("Landing");
    }

    public void PlayJump(){
        SFX_II.instance.Play("Jump");
    }
}
