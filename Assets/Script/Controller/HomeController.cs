using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;

public class HomeController : MonoBehaviour {

    // Use this for initialization
    public AuthManager auth;


    public void OnProfilePressed()
    {
        SceneManager.LoadScene("Profilescene");
    }

    public void OnSignOutPressed()
    {
        auth.SignOut();
        SceneManager.LoadScene("LoginScene");
        
    }

    public void OnCoursePressed()
    {
        SceneManager.LoadScene("CouresScene");
    }
}
