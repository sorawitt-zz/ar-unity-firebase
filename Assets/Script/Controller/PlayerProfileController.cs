using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;

public class PlayerProfileController : MonoBehaviour
{
    public Text emailLabel;
    public Text displayLabel;

    public AuthManager auth;

    private void Awake()
    {
        emailLabel.text = getDisplayName();
        emailLabel.text = getEmail();
    }

    private string getDisplayName()
    {
        FirebaseUser user = auth.GetProfileName();
        return user.DisplayName;
    }

    private string getEmail()
    {
        FirebaseUser user = auth.GetProfileName();
        return user.Email;
    }
}
