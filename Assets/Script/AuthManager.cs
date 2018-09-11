using UnityEngine;
using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;

public class AuthManager : MonoBehaviour
{

    // FIrebase API varibles

    Firebase.Auth.FirebaseAuth auth;

    void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void SignUpNewUser(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Sorry, creating new user error" + task.Exception);
            } else if (task.IsCompleted) {
                Firebase.Auth.FirebaseUser newUser = task.Result;
                Debug.LogFormat("Welcome to FireQ {0}", newUser.Email);
            }
        });
    }

}
