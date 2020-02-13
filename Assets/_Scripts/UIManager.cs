using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level Select");
    }

    public void CastleSelect()
    {
        SceneManager.LoadScene("Game Play");
        
       
    }

    public void ArenaSelect()
    {
        SceneManager.LoadScene("Arena");
    }

   
}

   