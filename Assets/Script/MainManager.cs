using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManu : MonoBehaviour
{
    UIManager uiManager;
    public void PlayTusu() {
        SceneManager.LoadScene(1);

        
    }
    public void Qu�tFNC()
    {
        Application.Quit();
        print("C�k�ld�");
    }
    
}
