using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class CurrentStandingsController : MonoBehaviour {

    DeclareWinner declareWinner;
    private bool canPressButton = false;

    void Awake()
    {
        AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.onAdComplete += OnAdComplete;
        declareWinner = GetComponent<DeclareWinner>();
    }

    void Start()
    {
        StartCoroutine(CanPressAndLeave(3f));
    }

    public void ClearControllers()
    {
        AirConsole.instance.onMessage -= OnMessage;
        AirConsole.instance.onAdComplete -= OnAdComplete;
    }

    bool loadOnce = false;

    void Update()
    {
        if (AirConsole.instance.GetActivePlayerDeviceIds.Count <= 0)
        {
            if (!loadOnce)
            {
                StartCoroutine(DelayedLoadScene("Menu"));
                loadOnce = true;
            }
        }
    }

    void OnMessage (int deviceID, JToken data)
    {
        int activePlayer = AirConsole.instance.ConvertDeviceIdToPlayerNumber(deviceID);

        if (activePlayer != -1)
        {
            try
            {
                if ((bool)data["A"]["pressed"] && declareWinner.GetPlayerHasWon() && canPressButton)
                {
                    // Game is over
                    Destroy(GameObject.Find("PointSystem")); // Need fresh points and playercounts, which Menu scene provides
                    Destroy(GameObject.Find("PlayerCount"));
                    PlayAd();
                }
                else if ((bool)data["A"]["pressed"] && !declareWinner.GetPlayerHasWon() && canPressButton)
                {
                    switch (Random.Range(0, 3))
                    {
                        case 0:
                            StartCoroutine(DelayedLoadScene("Map1")); // Castle
                            break;
                        case 1:
                            StartCoroutine(DelayedLoadScene("Map2")); // Volcano
                            break;
                        case 2:
                            StartCoroutine(DelayedLoadScene("Map3"));
                            break;
                        default:
                            StartCoroutine(DelayedLoadScene("Map1"));
                            Debug.Log("Random Map Loader glitched to default");
                            break;
                    }
                }
            }
            catch
            {
                // If not caught, don't do anything
            }
        }
    }

    void LoadMap(string mapName)
    {
        ClearControllers();
        try
        {
            SceneManager.LoadScene(mapName);
        }
        catch
        {
            Debug.Log("Either this map doesn't exist or you spelled its name wrong.");
        }
    }

    void OnAdComplete(bool ad_was_shown)
    {
        Debug.Log("Ad finished playing");
        StartCoroutine(DelayedLoadScene("Menu"));
    }

    // StartCoroutine(DelayedLoadScene("Menu"));
    void PlayAd()
    {
        GetComponent<AudioSource>().Pause();
        AirConsole.instance.ShowAd();
        
    }

    IEnumerator DelayedLoadScene(string sceneName)
    {
        float fadeTime = GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        LoadMap(sceneName);
    }

    IEnumerator CanPressAndLeave(float time)
    {
        yield return new WaitForSeconds(time);
        canPressButton = true;
    }

}
