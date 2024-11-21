using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabLogin : MonoBehaviour
{
    public static Action onLoginSuccess = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        LoginWithCustomID();
    }

    // Example function to login using a custom ID (you can also use other methods like Facebook or Steam login)
    void LoginWithCustomID()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier, // Unique identifier for the user (e.g., device ID)
            CreateAccount = true // Create an account if it doesn't exist
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    // Callback for a successful login
    void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Login successful!");
        Debug.Log("PlayFab ID: " + result.PlayFabId);
        Debug.Log("Session Ticket: " + result.SessionTicket);
        onLoginSuccess();
    }

    // Callback for a failed login
    void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError("Error logging in: " + error.GenerateErrorReport());
    }
}

