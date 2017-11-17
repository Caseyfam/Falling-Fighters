using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class PlayerControllers : MonoBehaviour
{
    public PlayerRigidBodyMovement rigid1, rigid2, rigid3, rigid4, rigid5, rigid6, rigid7, rigid8;
    GlobalVariables globalVariables;
    JToken controllerData;
    bool playerIsBlocking = false;

    int activePlayer;
    JToken data1, data2, data3, data4, data5, data6, data7, data8;
    private float controllerX1, controllerX2, controllerX3, controllerX4, controllerX5, controllerX6, controllerX7, controllerX8;
    private float controllerY1, controllerY2, controllerY3, controllerY4, controllerY5, controllerY6, controllerY7, controllerY8;
    private bool block1, block2, block3, block4, block5, block6, block7, block8;
    JObject mergedData;



    void Awake()
    {
        AirConsole.instance.onMessage += OnMessage;
    }

    public void ClearControllers()
    {
        AirConsole.instance.onMessage -= OnMessage;
    }

    // Use this for initialization
    void Start()
    {
        globalVariables = GameObject.Find("GameLogic").GetComponent<GlobalVariables>();

    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            block1 = (bool)data1["message"]["holding"];
        }
        catch
        {

        }
        try
        {
            block2 = (bool)data2["message"]["holding"];
        }
        catch
        {

        }
        try
        {
            block3 = (bool)data3["message"]["holding"];
        }
        catch
        {

        }
        try
        {
            block4 = (bool)data4["message"]["holding"];
        }
        catch
        {

        }
        try
        {
            block5 = (bool)data5["message"]["holding"];
        }
        catch
        {

        }
        try
        {
            block6 = (bool)data6["message"]["holding"];
        }
        catch
        {

        }
        try
        {
            block7 = (bool)data7["message"]["holding"];
        }
        catch
        {

        }
        try
        {
            block8 = (bool)data8["message"]["holding"];
        }
        catch
        {

        }
    }

    void OnMessage(int deviceID, JToken data)
    {
        activePlayer = AirConsole.instance.ConvertDeviceIdToPlayerNumber(deviceID);
        // Insert frame delay and find parse
        // Store JTokens in appropriately named JToken variables
        switch (activePlayer + 1)
        {
            case 1:
                data1 = data;
                break;
            case 2:
                data2 = data;
                break;
            case 3:
                data3 = data;
                break;
            case 4:
                data4 = data;
                break;
            case 5:
                data5 = data;
                break;
            case 6:
                data6 = data;
                break;
            case 7:
                data7 = data;
                break;
            case 8:
                data8 = data;
                break;
        }
        if (activePlayer != -1)
        {
            try
            {
                controllerX1 = (float)data1["joystick-left"]["message"]["x"];
            }
            catch
            {
                controllerX1 = 0;
            }
            try
            {
                controllerY1 = -(float)data1["joystick-left"]["message"]["y"];
            }
            catch
            {
                controllerY1 = 0;
            }
            try
            {
                controllerX2 = (float)data2["joystick-left"]["message"]["x"];
            }
            catch
            {
                controllerX2 = 0;
            }
            try
            {
                controllerY2 = -(float)data2["joystick-left"]["message"]["y"];
            }
            catch
            {
                controllerY2 = 0;
            }
            try
            {
                controllerX3 = (float)data3["joystick-left"]["message"]["x"];
            }
            catch
            {
                controllerX3 = 0;
            }
            try
            {
                controllerY3 = -(float)data3["joystick-left"]["message"]["y"];
            }
            catch
            {
                controllerY3 = 0;
            }
            try
            {
                controllerX4 = (float)data4["joystick-left"]["message"]["x"];
            }
            catch
            {
                controllerX4 = 0;
            }
            try
            {
                controllerY4 = -(float)data4["joystick-left"]["message"]["y"];
            }
            catch
            {
                controllerY4 = 0;
            }
            try
            {
                controllerX5 = (float)data5["joystick-left"]["message"]["x"];
            }
            catch
            {
                controllerX5 = 0;
            }
            try
            {
                controllerY5 = -(float)data5["joystick-left"]["message"]["y"];
            }
            catch
            {
                controllerY5 = 0;
            }
            try
            {
                controllerX6 = (float)data6["joystick-left"]["message"]["x"];
            }
            catch
            {
                controllerX6 = 0;
            }
            try
            {
                controllerY6 = -(float)data6["joystick-left"]["message"]["y"];
            }
            catch
            {
                controllerY6 = 0;
            }
            try
            {
                controllerX7 = (float)data7["joystick-left"]["message"]["x"];
            }
            catch
            {
                controllerX7 = 0;
            }
            try
            {
                controllerY7 = -(float)data7["joystick-left"]["message"]["y"];
            }
            catch
            {
                controllerY7 = 0;
            }
            try
            {
                controllerX8 = (float)data8["joystick-left"]["message"]["x"];
            }
            catch
            {
                controllerX8 = 0;
            }
            try
            {
                controllerY8 = -(float)data8["joystick-left"]["message"]["y"];
            }
            catch
            {
                controllerY8 = 0;
            }

            try
            {
                block1 = (bool)data1["swipeanalog-right"]["message"]["holding"];
            }
            catch
            {

            }
            try
            {
                block2 = (bool)data2["swipeanalog-right"]["message"]["holding"];
            }
            catch
            {

            }
            try
            {
                block3 = (bool)data3["swipeanalog-right"]["message"]["holding"];
            }
            catch
            {

            }
            try
            {
                block4 = (bool)data4["swipeanalog-right"]["message"]["holding"];
            }
            catch
            {

            }
            try
            {
                block5 = (bool)data5["swipeanalog-right"]["message"]["holding"];
            }
            catch
            {

            }
            try
            {
                block6 = (bool)data6["swipeanalog-right"]["message"]["holding"];
            }
            catch
            {

            }
            try
            {
                block7 = (bool)data7["swipeanalog-right"]["message"]["holding"];
            }
            catch
            {

            }
            try
            {
                block8 = (bool)data8["swipeanalog-right"]["message"]["holding"];
            }
            catch
            {

            }

            switch (activePlayer)
            {
                default:
                    break;
                case 0: // Player 1
                    try
                    {
                        ControllerMovePlayer(rigid1, 1, data1, controllerX1, controllerY1, block1);
                    }
                    catch
                    {
                        print("Shits on fire");
                    }
                    break;
                case 1: // Player 2
                    ControllerMovePlayer(rigid2, 2, data2, controllerX2, controllerY2, block2);
                    break;
                case 2: // Player 3
                    ControllerMovePlayer(rigid3, 3, data3, controllerX3, controllerY3, block3);
                    break;
                case 3: // Player 4
                    ControllerMovePlayer(rigid4, 4, data4, controllerX4, controllerY4, block4);
                    break;
                case 4: // Player 5
                    ControllerMovePlayer(rigid5, 5, data5, controllerX5, controllerY5, block5);
                    break;
                case 5: // Player 6
                    ControllerMovePlayer(rigid6, 6, data6, controllerX6, controllerY6, block6);
                    break;
                case 6: // Player 7
                    ControllerMovePlayer(rigid7, 7, data7, controllerX7, controllerY7, block7);
                    break;
                case 7: // Player 8
                    ControllerMovePlayer(rigid8, 8, data8, controllerX8, controllerY8, block8);
                    break;
            }
            try
            {
                if (data["swipeanalog-right"] != null)
                {
                    switch (activePlayer)
                    {
                        default:
                            break;
                        case 0: // Player 1
                            ControllerSwipePlayer(rigid1, data1);
                            break;
                        case 1: // Player 2
                            ControllerSwipePlayer(rigid2, data2);
                            break;
                        case 2: // Player 3
                            ControllerSwipePlayer(rigid3, data3);
                            break;
                        case 3: // Player 4
                            ControllerSwipePlayer(rigid4, data4);
                            break;
                        case 4: // Player 5
                            ControllerSwipePlayer(rigid5, data5);
                            break;
                        case 5: // Player 6
                            ControllerSwipePlayer(rigid6, data6);
                            break;
                        case 6: // Player 7
                            ControllerSwipePlayer(rigid7, data7);
                            break;
                        case 7: // Player 8
                            ControllerSwipePlayer(rigid8, data8);
                            break;
                    }
                }
            }
            catch
            {

            }
        }
    }


    void MessageCall()
    {


    }

    private void ControllerMovePlayer(PlayerRigidBodyMovement playerMovement, int playerID, JToken controllerData, float controllerX, float controllerY, bool playerBlock)
    {
        bool receivingInputs; // Is the player sending any inputs?

        try
        {
            if (controllerData["joystick-left"].HasValues)
            {
                if ((bool)controllerData["joystick-left"]["pressed"])
                {
                    receivingInputs = true;
                }
                else
                {
                    receivingInputs = false;
                }
                if (receivingInputs && !playerBlock)
                {
                    playerMovement.SetPlayerSpeed(globalVariables.GetGlobalPlayerSpeed());
                    playerMovement.SetPlayerRotationSpeed(globalVariables.GetGlobalPlayerRotationSpeed());
                    playerMovement.MovePlayer(controllerX, controllerY); // Sends inputs to the MovePlayer function, causing them to move

                    playerMovement.SetPlayerID(playerID); // Tells the animator which player to animate
                }
                else
                {
                    playerMovement.SetPlayerSpeed(0);
                    playerMovement.SetPlayerRotationSpeed(0);
                    playerMovement.MovePlayer(0, 0);
                }
            }
        }
        catch
        {
           
        }         
    }

    private void ControllerSwipePlayer(PlayerRigidBodyMovement playerRigid, JToken controllerData)
    {
        try
        {
            if ((bool)controllerData["swipeanalog-right"]["pressed"] == true)
            {
                if (controllerData["swipeanalog-right"]["message"]["x"] != null)
                {
                    playerRigid.gameObject.GetComponentInChildren<MeleeAttack>().Attack();
                }
                else if (controllerData["swipeanalog-right"]["message"]["holding"] != null)
                {
                    playerRigid.gameObject.GetComponentInChildren<Block>().setIsBlocking(true);
                    //Debug.Log(playerRigid + " started blocking");
                    playerIsBlocking = true;
                    playerRigid.GetComponentInChildren<PlayerAnimator>().SetBlocking(true);
                }
            }
            else if ((bool)controllerData["swipeanalog-right"]["message"]["holding"] == false)
            {
                playerRigid.gameObject.GetComponentInChildren<Block>().setIsBlocking(false);
                //Debug.Log(playerRigid + " stopped blocking");
                playerIsBlocking = false;
                playerRigid.GetComponentInChildren<PlayerAnimator>().SetBlocking(false);
            }
        }
        catch
        {
        }
    }
}
