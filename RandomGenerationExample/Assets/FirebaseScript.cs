using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseScript : MonoBehaviour
{

    DatabaseReference reference;

    // Start is called before the first frame update
    void Start()
    {
        // #1
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                //   app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });

        // #2
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("");

        // #3
        // Get the root reference location of the database.
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        //_________________________

        string DebugMsg = saveDataToFirebase(002, "Asal Alghamdi ", "Jeddah");
        Debug.Log(DebugMsg); // print message in console

    }

    public string saveDataToFirebase(int id, string name, string city) // نفرض اننا حابين نعمل قاعدة بيانات للموظفين كل موظف عنده بيانات مثل ID, Name, City
    {
        reference.Child(id.ToString()).Child("Name").SetValueAsync(name);
        reference.Child(id.ToString()).Child("City").SetValueAsync(city);
        return "Save data to firebase Done.";
    }
}