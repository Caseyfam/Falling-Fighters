using UnityEngine;
using System.Collections;

public class CreatePlayers : MonoBehaviour {

    public GameObject[] playerList;
    public GameObject[] playerNamesList;

    public GlobalVariables globalVariables;
    private int storedPlayerCount;

    public Vector3[] playerPositions1;
    public Vector3[] playerPositions2;
    public Vector3[] playerPositions3;
    public Vector3[] playerPositions4;
    public Vector3[] playerPositions5;
    public Vector3[] playerPositions6;
    public Vector3[] playerPositions7;
    public Vector3[] playerPositions8;


    // Use this for initialization
    void Start ()
    {
        storedPlayerCount = globalVariables.GetPlayerCount();
        CreatePlayersOnGameStart(storedPlayerCount);
	}
	
    public void CreatePlayersOnGameStart(int numPlayers)
    {
        for (int i = numPlayers + 1; i < 9; i++) // Destroy GameObjects that aren't used
        {
            playerList[i-1].SetActive(false);
            playerNamesList[i-1] .SetActive(false);
        }

       switch(numPlayers) // Helps assign the proper Vector3[] to get the proper player positions
        {
            case 1:
                PositionPlayers(numPlayers, playerPositions1);
                break;
            case 2:
                PositionPlayers(numPlayers, playerPositions2);
                break;
            case 3:
                PositionPlayers(numPlayers, playerPositions3);
                break;
            case 4:
                PositionPlayers(numPlayers, playerPositions4);
                break;
            case 5:
                PositionPlayers(numPlayers, playerPositions5);
                break;
            case 6:
                PositionPlayers(numPlayers, playerPositions6);
                break;
            case 7:
                PositionPlayers(numPlayers, playerPositions7);
                break;
            case 8:
                PositionPlayers(numPlayers, playerPositions8);
                break;
        }
    }

    private void PositionPlayers(int numPlayers, Vector3[] playerPositions)
    {
       for (int i = 0; i < numPlayers; i++)
        {
            playerList[i].transform.position = playerPositions[i];
        }
    }
}
