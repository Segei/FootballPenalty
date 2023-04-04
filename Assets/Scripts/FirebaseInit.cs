using Firebase;
using Firebase.Analytics;
using UnityEngine;

public class FirebaseInit : MonoBehaviour
{

    private void Start()
    {
        _ = FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(e =>
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        });

    }
}
