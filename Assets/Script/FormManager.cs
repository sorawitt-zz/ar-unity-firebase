using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Text.RegularExpressions;

public class FormManager : MonoBehaviour
{

    //UI
    public InputField emailInput;
    public InputField password;

    public Button signUpButton;
    public Button loginButton;

    public Text statusText;

    public AuthManager authManager;

    void Awake()
    {
        ToggleButtonState(false);
    }

    public void OnSignUp()
    {
        authManager.SignUpNewUser(emailInput.text, password.text);
        Debug.Log("Sign Up");
    }

    public void OnLogin() {
        Debug.Log("LOGIN");

    }

    /// <summary>
    /// Validates the email input
    /// </summary>

    public void ValidateEmail()
    {
        string email = emailInput.text;
        if (string.IsNullOrEmpty(email))
        {
            ToggleButtonState(false);
        }
        else
        {
            ToggleButtonState(true);
        }
    }

    //Utilities
    private void ToggleButtonState(bool toState)
    {
        signUpButton.interactable = toState;
        loginButton.interactable = toState;
    }

    private void UpdateState(string message)
    {
        statusText.text = message;
    }
}
