using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSoundPlayer : MonoBehaviour
{

    public string soundToPlay;

    public void PlaySound(){
        SFX_II.instance.Play(soundToPlay);
    }
}
