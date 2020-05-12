using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    public void loadOpeningMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void loadCharacterSelect()
    {
        SceneManager.LoadScene(1);
    }
    public void loadCharacterIntro()
    {
        SceneManager.LoadScene(2);
    }
    public void loadGameStart()
    {
        SceneManager.LoadScene(3);
    }
    public void loadCharacterDeath()
    {
        SceneManager.LoadScene(4);
    }
}
