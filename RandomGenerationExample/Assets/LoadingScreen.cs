using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{

    // Start is called before the first frame update
    private void Start()
    {
        EventManager.current.onDoneLoading += onFinishedLoading;
        EventManager.current.onStartLoading += onBeginLoading;
    }

    void onFinishedLoading(){

        gameObject.SetActive(false);
    }
    void onBeginLoading(){
        gameObject.SetActive(true);
    }
}
