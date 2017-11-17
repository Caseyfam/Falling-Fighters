using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class PlayerSelect : MonoBehaviour {

    public bool playerSelectActive = false;

    public GameObject playerSelectGroup;
    public MenuControls menuControls;
    public UnityEngine.UI.Image red, orange, yellow, green, blue, purple, black, white, title;
    JToken data1, data2, data3, data4, data5, data6, data7, data8;
    public TokenMovement[] tokens;
    List<int> correlations;

    int activePlayer;
    int readyNum = 0;

    private bool redSelect = false, orangeSelect = false, yellowSelect = false, greenSelect = false, blueSelect = false, purpleSelect = false, blackSelect = false, whiteSelect = false;
    private bool startOnce = true;

    void Awake()
    {
        AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.onConnect += OnConnect;
        AirConsole.instance.onDisconnect += OnDisconnect;
    }

    void OnConnect(int deviceID)
    {
        if (playerSelectActive)
        {
            SetSpecificPlayerActive(deviceID);
            correlations.Add(deviceID);
        }
    }

    void OnDisconnect (int deviceID)
    {
        GameObject.Find("P" + (AirConsole.instance.ConvertDeviceIdToPlayerNumber(deviceID) + 1) + "Token").SetActive(false);
    }

    public void ClearControllers()
    {
        AirConsole.instance.onMessage -= OnMessage;
        AirConsole.instance.onConnect -= OnConnect;
        AirConsole.instance.onDisconnect -= OnDisconnect;
    }

    void OnMessage(int deviceID, JToken data)
    {
        activePlayer = AirConsole.instance.ConvertDeviceIdToPlayerNumber(deviceID);
        switch (activePlayer + 1)
        {
            case 1:
                tokens[0].SendTokenData(data);
                break;
            case 2:
                tokens[1].SendTokenData(data);
                break;
            case 3:
                tokens[2].SendTokenData(data);
                break;
            case 4:
                tokens[3].SendTokenData(data);
                break;
            case 5:
                tokens[4].SendTokenData(data);
                break;
            case 6:
                tokens[5].SendTokenData(data);
                break;
            case 7:
                tokens[6].SendTokenData(data);
                break;
            case 8:
                tokens[7].SendTokenData(data);
                break;
        }
    }

    public void GrayOutPlayer(int menuNumber)
    {
        readyNum++;
        switch (menuNumber)
        {
            default:
                break;
            case 1:
                red.color = new Color32(0x3A, 0x3A, 0x3A, 0xFF);
                redSelect = true;
                break;
            case 2:
                orange.color = new Color32(0x3A, 0x3A, 0x3A, 0xFF);
                orangeSelect = true;
                break;
            case 3:
                yellow.color = new Color32(0x3A, 0x3A, 0x3A, 0xFF);
                yellowSelect = true;
                break;
            case 4:
                green.color = new Color32(0x3A, 0x3A, 0x3A, 0xFF);
                greenSelect = true;
                break;
            case 5:
                blue.color = new Color32(0x3A, 0x3A, 0x3A, 0xFF);
                blueSelect = true;
                break;
            case 6:
                purple.color = new Color32(0x3A, 0x3A, 0x3A, 0xFF);
                purpleSelect = true;
                break;
            case 7:
                black.color = new Color32(0x3A, 0x3A, 0x3A, 0xFF);
                blackSelect = true;
                break;
            case 8:
                white.color = new Color32(0x3A, 0x3A, 0x3A, 0xFF);
                whiteSelect = true;
                break;
        }
    }

    public bool PlayerIsSelected (int menuNumber)
    {
        switch(menuNumber)
        {
            default:
                return true;
            case 1:
                return redSelect;
            case 2:
                return orangeSelect;
            case 3:
                return yellowSelect;
            case 4:
                return greenSelect;
            case 5:
                return blueSelect;
            case 6:
                return purpleSelect;
            case 7:
                return blackSelect;
            case 8:
                return whiteSelect;
        }
    }
    // Update is called once per frame
    void Update ()
    {
	    if (readyNum >= AirConsole.instance.GetActivePlayerDeviceIds.Count && playerSelectActive)
        {
            if (startOnce)
            {
                ClearControllers();
                menuControls.StartGame();
                startOnce = false;
            } 
        }
	}

    public void SetSpecificPlayerActive(int deviceID)
    {
        tokens[AirConsole.instance.ConvertDeviceIdToPlayerNumber(deviceID)].SetTokenCanMove();
        StartCoroutine(tokens[AirConsole.instance.ConvertDeviceIdToPlayerNumber(deviceID)].PushDelay(0.5f));
    }

    public void SetPlayerSelectActive()
    {
        playerSelectActive = true;
        playerSelectGroup.SetActive(true);
        Message controllerLayout = new Message();
        controllerLayout.menuNumber = 0;
        for (int i = 1; i <= AirConsole.instance.GetActivePlayerDeviceIds.Count; i++)
        {
            tokens[i - 1].SetTokenCanMove();
            StartCoroutine(tokens[i - 1].PushDelay(0.5f));
            AirConsole.instance.Message(AirConsole.instance.ConvertPlayerNumberToDeviceId(i), JToken.FromObject(controllerLayout));
        }
    }
}
