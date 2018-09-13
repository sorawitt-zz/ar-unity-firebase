using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using Firebase;

public class CourseController : MonoBehaviour
{
    public Text timeLabel;
    public Button stopButton;
    public Button backButton;

    private float timeCount = 0.0f;
    private bool stateCount = true;

    public AuthManager authManager;

    private void Start()
    {
        timeLabel.text = "Time: ";
    }

    private void Update()
    {
        var tmpTime = TimeCount();
        timeLabel.text = (tmpTime).ToString("0");
    }

    private float TimeCount()
    {
        if (stateCount) timeCount += Time.deltaTime;
        return timeCount;
    }
    
    public void OnStopPressed()
    {
        stateCount = false;
        SaveTimeToDatabase();
    }

    private void SaveTimeToDatabase()
    {
        FirebaseUser user = authManager.GetProfileName();
        DatabaseManager.sharedInstance.SaveTimeCount(user.UserId, timeCount);
    }

}
