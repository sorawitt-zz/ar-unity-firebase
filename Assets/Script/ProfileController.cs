using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ProfileController : MonoBehaviour
{

    private FirebaseAuth auth;
    public Button SignOutButton, SaveDisplayName, GoToSaveDatabase;
    public Text ErrorText, EmailLabel, NameLabel;
    public InputField DisplayNameLabel, PhotoURL;
    Firebase.Auth.FirebaseUser user;
    public Image ProfileImg;

    // Use this for initialization
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        user = auth.CurrentUser;
        if (user == null)
        {
            SceneManager.LoadScene("LoginScene");
        }
        if (user.IsAnonymous)
        {
            EmailLabel.text = "User is Anonymous";
            NameLabel.text = "Mrs.Anonymous";
        }
        else
        {
            EmailLabel.text = "Email:" + user.Email;
            NameLabel.text = "Name:" + user.DisplayName;
        }
        SignOutButton.onClick.AddListener(SignOut);
        SaveDisplayName.onClick.AddListener(UpdateDisplayName);
        GoToSaveDatabase.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("SaveDatabaseScene");
        });
        StartCoroutine(loadSpriteIMG());


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SignOut()
    {
        auth.SignOut();
        user = auth.CurrentUser;
        if (user == null)
        {
            Debug.Log("SignOut Ja");
            SceneManager.LoadScene("LoginScene");
            //SceneManager.LoadSceneAsync("LoginScene");
        }
    }

    private void UpdateDisplayName()
    {
        String displayNameVal = DisplayNameLabel.text;
        Firebase.Auth.FirebaseUser userr = auth.CurrentUser;
        if (userr != null)
        {
            Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile();
            if (!string.IsNullOrEmpty(displayNameVal)) {
                profile.DisplayName = displayNameVal;
            }
            if (System.Uri.IsWellFormedUriString(PhotoURL.text, UriKind.Absolute))
            {
                profile.PhotoUrl = new System.Uri(PhotoURL.text);
            }

            userr.UpdateUserProfileAsync(profile).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("UpdateUserProfileAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
                    return;
                }
                NameLabel.text = "Name:" + userr.DisplayName;
                StartCoroutine(loadSpriteIMG());
                Debug.Log("User profile updated successfully.");
            });
        }
    }

    IEnumerator loadSpriteIMG()
    {
        user = auth.CurrentUser;
        string URL = "";

        if (!(user.PhotoUrl == null)) {
            URL = user.PhotoUrl.AbsoluteUri;
        }

        if (Application.internetReachability == NetworkReachability.NotReachable)
            yield return null;

        var www = new WWW(URL);
        Debug.Log("Download image on progress");

        yield return www;
        if (string.IsNullOrEmpty(www.text))
            Debug.Log("Download failed");
        else
        {
            Debug.Log("Download Success");
            Texture2D texture = new Texture2D(1, 1);
            www.LoadImageIntoTexture(texture);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                                          Vector2.one / 2);
            ProfileImg.sprite = sprite;
            //GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }
}
