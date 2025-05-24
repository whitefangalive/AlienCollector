using System.Runtime.InteropServices;
using UnityEngine;

public static class BrowserUtils
{
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void Browser_GoBack();
    [DllImport("__Internal")]
    private static extern void Browser_Reload();
#endif

    /// <summary>
    /// “Exit” by going back in history (like the back button).
    /// </summary>
    public static void GoBack()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Browser_GoBack();
#else
        Application.Quit();
#endif
    }

    /// <summary>
    /// Reloads the current page in-place.
    /// </summary>
    public static void Reload()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Browser_Reload();
#else
        Application.Quit();
#endif
    }
}
