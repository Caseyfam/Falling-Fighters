using UnityEngine;
using System.Collections;
using NDream.AirConsole;

public class SetPlayerCount : MonoBehaviour {

    // This script exists since, in a real game, once a round starts we won't allow any more players to enter the game.
    // Now, when we enter the game, we are locking in the player count, which is neccessary since the camera relies on this number.
    
    void Start()
    {
        SetThePlayerCount();
    }

    public void SetThePlayerCount()
    {
        GameObject.Find("GameLogic").GetComponent<GlobalVariables>().SetPlayerCount(AirConsole.instance.GetActivePlayerDeviceIds.Count);
        GameObject.Find("PlayerCount").GetComponent<PlayerCount>().SetPlayerCount(AirConsole.instance.GetActivePlayerDeviceIds.Count);
        // Now call the camera to tell it what players to track
        GameObject.Find("GameLogic").GetComponent<PlayerInStatus>().SetPlayers(AirConsole.instance.GetActivePlayerDeviceIds.Count);
    }
}
