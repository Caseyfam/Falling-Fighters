using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class MenuConnectText : MonoBehaviour {

    UnityEngine.UI.Text consoleText;
    private string consoleTextString;

    Dictionary<int, string> playerNicknames;

    void Awake()
    {
        AirConsole.instance.onConnect += OnConnect;
        AirConsole.instance.onDisconnect += OnDisconnect;
        playerNicknames = new Dictionary<int, string>();
    }

    void OnConnect(int deviceID)
    {
        playerNicknames.Add(deviceID, AirConsole.instance.GetNickname(deviceID));
        consoleTextString += "\n" + playerNicknames[deviceID] + " has connected!";
        try
        {
            StartCoroutine(WaitForSpace(5f));
        }
        catch
        {

        }
    }
    
    void OnDisconnect(int deviceID)
    {
        consoleTextString += "\n" + playerNicknames[deviceID] + " has disconnected!";
        playerNicknames.Remove(deviceID);
    }

    // Use this for initialization
    void Start ()
    {
        consoleText = GameObject.Find("ConnectConsole").GetComponent<UnityEngine.UI.Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        consoleText.text = consoleTextString;
	}

    IEnumerator WaitForSpace(float time)
    {
        yield return new WaitForSeconds(time);
        consoleTextString += " \n";
    }
}
