using UnityEngine;
using System.Collections;
using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;
using System;

public class AuthManager : MonoBehaviour
{

    // FIrebase API varibles

    Firebase.Auth.FirebaseAuth auth;

    // Delegates
    public delegate IEnumerator AuthCallBack(Task<Firebase.Auth.FirebaseUser> task, string operation);
    public event AuthCallBack authCallback;


    void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void SignUpNewUser(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            StartCoroutine(authCallback(task, "sign_up"));
        });
    }

    public void SignInExistingUser(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            StartCoroutine(authCallback(task, "sign_in"));
        });
    }


    public void SignUpNewUserWithCallBack(string email, string password, Action<FirebaseUser, bool, string> callback)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                callback(null, false, "canceled sign up");
                return;
            }

            if (task.IsFaulted)
            {
                callback(null, false, "error" + task.Exception);
                return;
            }

            if (task.IsCompleted)
            {
                callback(task.Result, true, "Signed up successfully");
                return;
            }
            //StartCoroutine(authCallback(task, "sign_up"));
        });
    }

    public void SignInExistingUserWithCallBack(string email, string password, Action<bool, string> callback)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            //StartCoroutine(authCallback(task, "sign_in"));
        });
    }

}
