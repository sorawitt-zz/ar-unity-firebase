using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class AuthGame : MonoBehaviour {

    private FirebaseAuth auth;
    public InputField email_input, password_input;
    public Button SignupButton, SigninButton, SigninAnonymous, okButton;
    public Text ErrorText;
    public GameObject popUp;

	// Use this for initialization
	void Start () {
        popUp.SetActive(false);
        auth = FirebaseAuth.DefaultInstance;

        email_input.text = "sorawit.trutsat@gmail.com";
        password_input.text = "1234567890";

        SignupButton.onClick.AddListener(() => Signup(email_input.text, password_input.text));
        SigninButton.onClick.AddListener(() => LoginAction(email_input.text, password_input.text));
        okButton.onClick.AddListener(okPressed);
        SigninAnonymous.onClick.AddListener(LoginWithAnonymous);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDestroy()
    {
        Debug.Log("OnDestroy");
    }


    public void Signup(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            //Error handling
            return;
        }

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync error: " + task.Exception);
                if (task.Exception.InnerExceptions.Count > 0) {
                    ErrorText.text = task.Exception.InnerExceptions[0].Message;
                    popUp.SetActive(true);
                }
                    
                return;
            }
            popUp.SetActive(true);
            FirebaseUser newUser = task.Result; // Firebase user has been created.
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId); 
            ErrorText.text = "Sign Up success!";
            popUp.SetActive(true);
        });
    }

    public void LoginAction(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync canceled.");
                ErrorText.text = "SignInWithEmailAndPasswordAsync canceled.";
                popUp.SetActive(true);
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync error: " + task.Exception);
                if (task.Exception.InnerExceptions.Count > 0)
                {
                    ErrorText.text = task.Exception.InnerExceptions[0].Message;
                    popUp.SetActive(true);
                }
                return;
            }

            FirebaseUser user = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                user.DisplayName, user.UserId);

            PlayerPrefs.SetString("LoginUser", user != null ? user.Email : "Unknown");

            SceneManager.LoadSceneAsync("ProfileScene");
        });
    }

    public void okPressed() {
        popUp.SetActive(false);
    }
    private void LoginWithAnonymous() {
        auth.SignInAnonymouslyAsync().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithAnonymousAsync canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithAnonymousAsync error: " + task.Exception);
                if (task.Exception.InnerExceptions.Count > 0)
                {
                    ErrorText.text = task.Exception.InnerExceptions[0].Message;
                    popUp.SetActive(true);
                }

                return;
            }
            FirebaseUser user = task.Result;
            PlayerPrefs.SetString("LoginUser", user != null ? user.Email : "Unknown");
            SceneManager.LoadSceneAsync("ProfileScene");
        });
    }
}
