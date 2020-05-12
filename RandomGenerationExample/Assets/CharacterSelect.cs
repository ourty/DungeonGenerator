using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("CharacterSelected", 1);
    }
    public void SelectedAdventurer()
    {
        PlayerPrefs.SetInt("CharacterSelected", 1);
    }
    public void SelectedRaider()
    {
        PlayerPrefs.SetInt("CharacterSelected", 2);
    }
    public void SelectedNinja()
    {
        PlayerPrefs.SetInt("CharacterSelected", 3);
    }
    public void SelectedSwindler()
    {
        PlayerPrefs.SetInt("CharacterSelected", 4);
    }
}
