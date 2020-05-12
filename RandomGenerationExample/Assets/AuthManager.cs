using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour
{
    public InputField EmailLoginInputField;
    public InputField PasswordLoginInputField;

    public InputField EmailRegisterInputField;
    public InputField PasswordRegisterInputField;
    public Text ErrorText;

    // Start is called before the first frame update
    void Start()
    {
        print("Auth Manager is working");
        //PlayerPrefs.SetInt("NumberOfUsers", 0);
    }
    public void LoginButtonHandler()
    {
        var userName = EmailLoginInputField.text;
        var password = PasswordLoginInputField.text;

        var auth = FirebaseAuth.DefaultInstance;

        auth.SignInWithEmailAndPasswordAsync(userName, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
        SceneManager.LoadScene(1);
    }
       
    public void RegisterButtonHandler()
    {
        //PlayerPrefs.SetString("Email" + PlayerPrefs.GetInt("NumberOfUsers"), EmailRegisterInputField.text);
        //PlayerPrefs.SetString("Password" + PlayerPrefs.GetInt("NumberOfUsers"), PasswordRegisterInputField.text);
        //PlayerPrefs.SetInt("NumberOfUsers", PlayerPrefs.GetInt("NumberOfUsers") + 1);
        //EmailRegisterInputField.text = "";
        //PasswordRegisterInputField.text = "";
        //SceneManager.LoadScene(1);

        var userName = EmailRegisterInputField.text;
        var password = PasswordRegisterInputField.text;

        var auth = FirebaseAuth.DefaultInstance;

        auth.CreateUserWithEmailAndPasswordAsync(userName, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (EmailRegisterInputField.text != null) 
        {
            //print(EmailRegisterInputField.text);
        } 
    }
}
