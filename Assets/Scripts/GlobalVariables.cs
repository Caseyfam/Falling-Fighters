using UnityEngine;
using System.Collections;

public class GlobalVariables : MonoBehaviour
{
    // Place more global variables up here
    private bool gameActive;
    private bool playersCanMove = false;
    private int globalPlayerSpeed = 10;
    private int globalGravity = 300;
    private int globalPlayerRotationSpeed = 10;
    private float outOfBoundsHeight = -2.5f;
    private int playersLeft;
    private int playerCount;
    private float globalPlatformFallSpeed = 0.1f;
    private bool playerHasWon;

    public bool GetGameActive()
    {
        return gameActive;
    }
    public void SetGameActive(bool gameReady)
    {
        this.gameActive = gameReady;
    }
	
    public bool GetPlayersCanMove()
    {
        return playersCanMove;
    }
    public void SetPlayersCanMove(bool canMove)
    {
        playersCanMove = canMove;
    }

    public int GetGlobalPlayerSpeed()
    {
        return globalPlayerSpeed;
    }
    public void SetGlobalPlayerSpeed(int newSpeed)
    {
        globalPlayerSpeed = newSpeed;
    }

    public int GetGlobalPlayerRotationSpeed()
    {
        return globalPlayerRotationSpeed;
    }
    public void SetGlobalPlayerRotationSpeed(int newSpeed)
    {
        globalPlayerRotationSpeed = newSpeed;
    }

    public int GetGlobalGravity()
    {
        return globalGravity;
    }
    public void SetGlobalGravity(int newGravity)
    {
        globalGravity = newGravity;
    }

    public int GetPlayerCount()
    {
        return playerCount;
    }
    public void SetPlayerCount(int playerCount)
    {
        if (playerCount <= 8)
        {
            this.playerCount = playerCount;
        }
        else
        {
            this.playerCount = 8;
        }
        
    }

    public float GetOutOfBoundsHeight()
    {
        return outOfBoundsHeight;
    }
    public void SetOutOfBoundsHeight(int outOfBoundsHeight)
    {
        this.outOfBoundsHeight = outOfBoundsHeight;
    }

    public int GetPlayersLeft() // Isn't working
    {
        return playersLeft;
    }
    public void SetPlayersLeft(int playersLeft)
    {
        this.playersLeft = playersLeft;
    }

    public float GetGlobalPlatformFallSpeed()
    {
        return globalPlatformFallSpeed;
    }
    public void SetGlobalPlatformFallSpeed(float newFallSpeed)
    {
        globalPlatformFallSpeed = newFallSpeed;
    }

    public bool GetPlayerHasWon()
    {
        return playerHasWon;
    }
    public void SetPlayerHasWon(bool flag)
    {
        playerHasWon = flag;
    }
}
