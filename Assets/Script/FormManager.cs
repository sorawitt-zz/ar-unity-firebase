using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using UnityEngine.SceneManagement;

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
        authManager.authCallback += HandleAuthCallback;
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

    IEnumerator HandleAuthCallback(Task<Firebase.Auth.FirebaseUser> task, string operatoin)
    {
        if (task.IsFaulted || task.IsCanceled)
        {
            UpdateState("Sorry, creating new user error" + task.Exception);
        }
        else if (task.IsCompleted)
        {
            Firebase.Auth.FirebaseUser newUser = task.Result;
            UpdateState("Loading the game scene");
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("ProfileScene");

        }
    }

    private void OnDestroy()
    {
        authManager.authCallback -= HandleAuthCallback;
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
