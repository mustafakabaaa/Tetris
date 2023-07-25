using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public bool oyunDurdumu=false;

    public GameObject pausePanel;

    private GameMAnager gameM;

    private void Awake()
    {
        gameM=FindObjectOfType<GameMAnager>();

    }
    private void Start()
    {
        if (pausePanel)
        {
            pausePanel.SetActive(false);
        }
    }
    public void QuýtFNC()
    {
        Application.Quit();
        print("Cýkýldý");
    }
    private void Update()
    {
        if(Input.GetKeyDown("f"))
        {
            PausePaneAcKapa();
        }
    }
    public void PausePaneAcKapa()
    {
        if (gameM.gameOver)
        {
            return;
        }
        oyunDurdumu = !oyunDurdumu;
        if (pausePanel)
        {
            pausePanel.SetActive(oyunDurdumu);
            if (SoundManager.instance)
            {
                SoundManager.instance.SesEfektiCilar(0);
                
                Time.timeScale=(oyunDurdumu ? 0 : 1);
            }
        }
    }
    
    public void YenidenOynaFNC()
    {
        SoundManager.instance.SesEfektiCilar(0);
        
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenuTusu()
    {
        SceneManager.LoadScene(1);
        


    }
}
