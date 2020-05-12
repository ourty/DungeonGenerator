using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Mute()
    {
        AudioListener.pause = !AudioListener.pause;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
