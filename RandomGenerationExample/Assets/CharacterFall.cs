using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFall : MonoBehaviour
{
	public GameObject[] scenes;
    private void Awake()
    {	
        if (GameSceneManager.playerCharacter == 1)
        {
        	scenes[0].SetActive(true);
        }
        if (GameSceneManager.playerCharacter == 2)
        {
        	scenes[1].SetActive(true);
        }
        if (GameSceneManager.playerCharacter == 3)
        {
        	scenes[2].SetActive(true);
        }
        if (GameSceneManager.playerCharacter == 4)
        {
        	scenes[3].SetActive(true);
        }
    }
    void Start()
     {
     	Time.timeScale = 1f;
        StartCoroutine(LoadScene(1.27f));
     }

    IEnumerator LoadScene(float delay)
    {
    yield return new WaitForSeconds(delay);
    gameObject.GetComponent<SceneLoading>().loadGameStart();
    }
}
