using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInStatus : MonoBehaviour {

    public List<Transform> playerTransforms;
    public bool[] playerIsIn;
    GlobalVariables globalVars;

    private float outOfBoundsHeight;
    private int thisPlayerNum;

    private int playerCount;
    private int playersLeft;
    bool playersLeftSingleCheck = true;

    void Start()
    {
        globalVars = GameObject.Find("GameLogic").GetComponent<GlobalVariables>();
        outOfBoundsHeight = globalVars.GetOutOfBoundsHeight();
    }

    void Update()
    {
        for (int i = 0; i < playerCount; i++)
        {
            try
            {
                if (playerTransforms[i].transform.position.y < outOfBoundsHeight)
                {
                    thisPlayerNum = (int)playerTransforms[i].gameObject.ToString()[6] - 48;
                    playerIsIn[thisPlayerNum - 1] = false;
                    playerTransforms[i].GetComponentInChildren<PlayerAnimator>().PlayFallingAudio(); // Experimental
                    playersLeft = playersLeft - 1;
                    globalVars.SetPlayersLeft(playersLeft);
                    playerTransforms.Remove(GameObject.Find("Player" + (thisPlayerNum)).transform);
                    //Destroy(GameObject.Find("Player" + (thisPlayerNum)));
                }
            }
            catch
            {
                // Object below level is destroyed
            }
        }
        if (playersLeft < 2 && playersLeftSingleCheck == true)
        {
            GameObject.Find("PointSystem").GetComponent<PointSystem>().AddPointForWinner(playerTransforms[0].gameObject); // Add a point for the winner to the global point system
            playersLeftSingleCheck = false; // So we don't add tons of points to the winner per frame
        }

    }

    public void SetPlayers(int playerCount) // Initialize objects to look at and sets bools for if they're in or not
    {
        this.playerCount = playerCount;
        playersLeft = playerCount;
        playerTransforms = new List<Transform>();
        playerIsIn = new bool[playerCount];

        for (int i = 0; i < playerCount; i++)
        {
            try
            {
                playerTransforms.Add(GameObject.Find("Player" + (i + 1)).transform);
                playerIsIn[i] = true;
            }
            catch
            {

            }
        }
    }

    public void PlayerDisconnect(int playerNum)
    {
        print("Shifting Player" + playerNum);
        for (int i = playerNum; i < playerCount; i++)
        {
            try
            {
                playerIsIn[i] = playerIsIn[i + 1];
            }
            catch
            {
                playerIsIn[i] = false;
            }
        }
        playersLeft--;
    }
}
