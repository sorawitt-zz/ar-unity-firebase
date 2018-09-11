using UnityEngine;
using System.Collections;
using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;

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

}
