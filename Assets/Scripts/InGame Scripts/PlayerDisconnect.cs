using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using NDream.AirConsole;

public class PlayerDisconnect : MonoBehaviour {

    // Basically, when a player disconnects, all player numbers shift down. Player 3 becomes Player 2. However, we need
    // to make it seem like nothing really changed, so we need to shift player points down, materials, and the fighter
    // model that the player is used to.

    PointSystem pointSystem;
    ModelMaterialIdentifier modelMatInfo;
    PlayerCount playerCount;
    PlayerInStatus playerIn;

    int totalPlayers;

    void Awake()
    {
        modelMatInfo = GetComponent<ModelMaterialIdentifier>();
        
    }

    void FindTotalPlayers()
    {
        try
        {
            totalPlayers = playerCount.GetPlayerCount();
        }
        catch
        {
            Debug.Log("Waiting for players to connect...");
        }
    }

	// Use this for initialization
	void Start ()
    {
        FindTotalPlayers();
    }
	
	// Update is called once per frame
	void Update ()
    {
        try
        {
            playerCount = GameObject.Find("PlayerCount").GetComponent<PlayerCount>();
        }
        catch
        {

        }
        
        FindTotalPlayers();

        if (!SceneManager.GetActiveScene().name.Equals("Menu") && !SceneManager.GetActiveScene().name.Equals("Current Standings"))
        {
            playerIn = GameObject.Find("GameLogic").GetComponent<PlayerInStatus>();
        }
        else
        {
            playerIn = null;
        }
    }

    public void DisconnectPlayer(int playerNum)
    {
        // Remember, we're working with the notion that Player1 is 0, Player2 is 1, etc.
        pointSystem = GameObject.Find("PointSystem").GetComponent<PointSystem>();
        pointSystem.SetPointsOnDisconnect(playerNum);

        if (SceneManager.GetActiveScene().name != "Menu")
        {
            //modelMatInfo.SetMaterialsOnDisconnect(playerNum);
            //modelMatInfo.SetAvatarsOnDisconnect(playerNum);
            modelMatInfo.SetObjectsOnDisconnect(playerNum);
            modelMatInfo.SetStandingsObjectsOnDisconnect(playerNum);
            modelMatInfo.SetControllerColorsOnDisconnect(playerNum);
            modelMatInfo.SetScoreColorsOnDisconnect(playerNum);
        }
        
        try
        {
            playerIn.PlayerDisconnect(playerNum);
            Destroy(GameObject.Find("Player" + (totalPlayers)));
        }
        catch
        {

        }

        playerCount.SetPlayerCount(totalPlayers - 1);

        for (int i = playerNum + 1; i < totalPlayers; i++)
        {
            if (SceneManager.GetActiveScene().name != "Current Standings")
            {
                try
                {
                    GameObject.Find("Player" + i).transform.position = GameObject.Find("Player" + (i + 1)).transform.position;
                    GameObject.Find("Player" + i).transform.rotation = GameObject.Find("Player" + (i + 1)).transform.rotation;
                }
                catch
                {

                }
            }
        }

        FindTotalPlayers();
    }
}
