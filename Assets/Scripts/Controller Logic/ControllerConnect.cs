using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;


public class ControllerConnect : MonoBehaviour {

    string sceneName = null;
    public List<int> correlations; // Ties playerNumber to deviceID
    ModelMaterialIdentifier modelMat;
    PlayerSelect playerSelectActive;

    int currentSceneController;

    void Awake()
    {
        modelMat = GetComponent<ModelMaterialIdentifier>();
        AirConsole.instance.onConnect += OnConnect;
        AirConsole.instance.onDisconnect += OnDisconnect;
        // Need to come back and possibly clear these or something
    }

    void OnConnect(int deviceID)
    {
        //Debug.Log(deviceID + " connected");
        if (AirConsole.instance.GetControllerDeviceIds().Count <= 8)
        {
            StartGame(AirConsole.instance.GetControllerDeviceIds().Count);
        }
        correlations.Add(deviceID);

        SendMessageToController(deviceID);

        Message menuMessage = new Message();
        menuMessage.menuNumber = currentSceneController;

        SendMenuToController(menuMessage);
    }

    void OnDisconnect(int deviceID)
    {
        //Debug.Log(deviceID + " disconnected");
        GetComponent<PlayerDisconnect>().DisconnectPlayer(AirConsole.instance.ConvertDeviceIdToPlayerNumber(deviceID));
        StartCoroutine(Pause(0.01f, deviceID));
    }

    IEnumerator Pause (float time, int deviceID)
    {
        yield return new WaitForSeconds(time);
        StartGame(AirConsole.instance.GetControllerDeviceIds().Count);
        GameObject.Find("PlayerCount").GetComponent<PlayerCount>().SetPlayerCount(AirConsole.instance.GetActivePlayerDeviceIds.Count);
        correlations.Remove(deviceID);

        Message menuMessage = new Message();
        menuMessage.menuNumber = currentSceneController;
        SendMenuToController(menuMessage);
    }

    void StartGame (int numPlayers) // StartGame doesn't actually start the game, it just initializes player inputs
    {
        AirConsole.instance.SetActivePlayers(numPlayers);
    }

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (SceneManager.GetActiveScene().name != sceneName)
        { 
            // Detecting a scene change since Unity took out OnLevelWasLoaded which was extremely useful
            //Debug.Log("Scene has changed");
            sceneName = SceneManager.GetActiveScene().name;

            Message menuMessage = new Message();

            if (sceneName == "Menu")
            {
                playerSelectActive = GameObject.Find("GameLogic").GetComponent<PlayerSelect>();
                currentSceneController = 0;
                menuMessage.menuNumber = 0;
                SendMenuToController(menuMessage);
                for (int i = 0; i < 8; i++)
                {
                    try
                    {
                        SendMessageToController(AirConsole.instance.ConvertPlayerNumberToDeviceId(i));
                    }
                    catch
                    {

                    }
                }
            }
            else if (sceneName == "Current Standings")
            {
                currentSceneController = 1;
                menuMessage.menuNumber = 1;
                SendMenuToController(menuMessage);
            }
            else if (sceneName == "Map1" || sceneName == "Map2" || sceneName == "Map3")
            {
                currentSceneController = 2;
                menuMessage.menuNumber = 2;
                SendMenuToController(menuMessage);
            }        
        }
	}

    void SendMenuToController(Message menuMessage)
    {
        for (int i = 0; i < 8; i++)
        {
            try
            {
                if (SceneManager.GetActiveScene().name.Equals("Menu"))
                {
                    if (i == 0)
                    {
                        menuMessage.menuNumber = 0;
                        AirConsole.instance.Message(correlations[i], JToken.FromObject(menuMessage));
                    }
                    else // Send "Player select active" and deal with that somehow
                    {
                        try
                        {
                            if (playerSelectActive.playerSelectActive == true)
                            {
                                menuMessage.menuNumber = 0;
                                AirConsole.instance.Message(correlations[i], JToken.FromObject(menuMessage));
                            }
                            else
                            {
                                menuMessage.menuNumber = 3;
                                AirConsole.instance.Message(correlations[i], JToken.FromObject(menuMessage));
                            }
                        }
                        catch
                        {

                        } 
                    }
                }
                else if (SceneManager.GetActiveScene().name.Equals("Current Standings"))
                {
                    if (i == 0)
                    {
                        AirConsole.instance.Message(correlations[i], JToken.FromObject(menuMessage));
                    }
                    else
                    {
                        menuMessage.menuNumber = 3;
                        AirConsole.instance.Message(correlations[i], JToken.FromObject(menuMessage));
                    }
                }
                else
                {
                    AirConsole.instance.Message(correlations[i], JToken.FromObject(menuMessage));
                }
                
            }
            catch
            {
                // Controllers probably aren't connected yet
            }

        }
    }

    

    public void SendMessageToController(int deviceID)
    {
        Message colorMessage = new Message();

        switch (AirConsole.instance.ConvertDeviceIdToPlayerNumber(deviceID))
        {
            case 0:
                colorMessage.color = modelMat.controllerColors[0];
                AirConsole.instance.Message(deviceID, JToken.FromObject(colorMessage)); // Red
                break;
            case 1:
                colorMessage.color = modelMat.controllerColors[1];
                AirConsole.instance.Message(deviceID, JToken.FromObject(colorMessage)); // Orange
                break;
            case 2:
                colorMessage.color = modelMat.controllerColors[2];
                AirConsole.instance.Message(deviceID, JToken.FromObject(colorMessage)); // Yellow
                break;
            case 3:
                colorMessage.color = modelMat.controllerColors[3];
                AirConsole.instance.Message(deviceID, JToken.FromObject(colorMessage)); // Green
                break;
            case 4:
                colorMessage.color = modelMat.controllerColors[4];
                AirConsole.instance.Message(deviceID, JToken.FromObject(colorMessage)); // Blue
                break;
            case 5:
                colorMessage.color = modelMat.controllerColors[5];
                AirConsole.instance.Message(deviceID, JToken.FromObject(colorMessage)); // Purple
                break;
            case 6:
                colorMessage.color = modelMat.controllerColors[6];
                AirConsole.instance.Message(deviceID, JToken.FromObject(colorMessage)); // Black
                break;
            case 7:
                colorMessage.color = modelMat.controllerColors[7];
                AirConsole.instance.Message(deviceID, JToken.FromObject(colorMessage)); // White
                break;
        }
        //print("Message sent to " + deviceID);
    }
}
