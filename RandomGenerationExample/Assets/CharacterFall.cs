using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFall : MonoBehaviour
{
	public GameObject[] scenes;
    private void Awake()
    {	
        if (PlayerPrefs.GetInt("CharacterSelected") == 1)
        {
        	scenes[0].SetActive(true);
        }
        if (PlayerPrefs.GetInt("CharacterSelected") == 2)
        {
        	scenes[1].SetActive(true);
        }
        if (PlayerPrefs.GetInt("CharacterSelected") == 3)
        {
        	scenes[2].SetActive(true);
        }
        if (PlayerPrefs.GetInt("CharacterSelected") == 4)
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
