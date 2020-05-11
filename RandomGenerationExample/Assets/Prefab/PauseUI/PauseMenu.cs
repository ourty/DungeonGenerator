using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject pausebutton;
    public LoadingScreen isloading;
    public GameObject HpBar;

    void Start () 
    {
        //Pause button and HpBar is suppose to be not active when loading screen is shown
        pausebutton.SetActive(true);
        pauseMenuUI.SetActive(false);
        HpBar.SetActive(true);
    }
    void Update()
    {
        if (isloading.GetComponent<LoadingScreen>().enabled == false)
        {
            pausebutton.SetActive(true);
            HpBar.SetActive(true);
        } 
    }
    public void pausemenu()
    {
        
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
    }

    public void Resume()
    {
        pausebutton.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        pausebutton.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
