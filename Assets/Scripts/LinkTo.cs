using System.Collections;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class LinkTo : MonoBehaviour
{
    [SerializeField] private WebViewObject webViewObject;
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            webViewObject?.GoBack();
        }
    }

    public void GoToLink(string url)
    {
        Permission.RequestUserPermission(Permission.Camera);
        Permission.RequestUserPermission(Permission.ExternalStorageRead);
        Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        webViewObject.Init();
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        webViewObject.bitmapRefreshCycle = 1;
#endif
        webViewObject.SetTextZoom(100);
        webViewObject.SetVisibility(true); 
        webViewObject.SetCameraAccess(true);
        webViewObject.SetMicrophoneAccess(true);
        webViewObject.LoadURL(url.Replace(" ", "%20"));
    }

}
