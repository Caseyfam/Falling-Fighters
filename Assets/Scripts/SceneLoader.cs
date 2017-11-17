using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class SceneLoader : MonoBehaviour
{
    GlobalVariables globalVars;
    bool checkOnce = false;

    void Start()
    {
        globalVars = GameObject.Find("GameLogic").GetComponent<GlobalVariables>();       
    }

    void Update()
    {
        if (globalVars.GetPlayerHasWon() && checkOnce == false) // Checks if a player has won so we can load the standings screen
        {
            StartCoroutine(WaitToTransition(5, "Current Standings"));
            checkOnce = true;
        }
        if (AirConsole.instance.GetActivePlayerDeviceIds.Count <= 0)
        {
            StartCoroutine(WaitToTransition(1, "Menu"));
        }
    }

    public IEnumerator WaitToTransition(int seconds, string sceneName)
    {
        yield return new WaitForSeconds(seconds);
        float fadeTime = GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(sceneName);
        GameObject.Find("GameLogic").GetComponent<SetPlayerCount>().SetThePlayerCount(); 
        
       
    }
}
