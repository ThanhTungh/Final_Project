using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabManager : PersistentSingleton<PlayFabManager>
{
    private string playFabID;

    public static string coin;

    public static string playerData;

    private string keyCrr;
    public void SavePlayerData(string key, string value)
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { key, value }
            }
        };

        PlayFabClientAPI.UpdateUserData(request,
 OnSaveDataSuccess, OnSaveDataFailure);
    }

    private void OnSaveDataSuccess(UpdateUserDataResult result)
    {
        Debug.Log("Data saved successfully!");
    }

    private void OnSaveDataFailure(PlayFabError error)
    {
        Debug.LogError("Error saving data: " + error.ErrorMessage);

    }

    public void LoadPlayerData(string key)
    {
        keyCrr = key;
        var request = new GetUserDataRequest { Keys = new List<string> { key } };

        PlayFabClientAPI.GetUserData(request, OnLoadDataSuccess, OnLoadDataFailure);
    }

    private void OnLoadDataSuccess(GetUserDataResult result)
    {
        if (result.Data.ContainsKey(keyCrr))
        {
            string value = result.Data[keyCrr].Value;
            Debug.Log("Loaded value: " + value);
            if (keyCrr.Equals("Coins"))
            {
                int coinConvertString = int.Parse(value);
                CoinManager.Coins = coinConvertString;
                Debug.LogError(coinConvertString + "-----");
                return;
            }
            MenuManager.jsonString = value;
            
        }
        else
        {
            Debug.Log("Data not found for key: " + keyCrr);
            string key = "Coins";
            SavePlayerData(key, "1000");
        }
    }

    private void OnLoadDataFailure(PlayFabError error)
    {
        Debug.LogError("Error loading data: " + error.ErrorMessage);


    }
}
