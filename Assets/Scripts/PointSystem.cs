using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PointSystem : MonoBehaviour {

    int[] previousPlayerPoints;
    public int[] playerPoints;
    public int[] disconnectPoints;
    public int winningNumber;
    GlobalVariables globalVars;

    void Awake() // Don't destroy points between scenes
    {
        DontDestroyOnLoad(transform.gameObject);
    }

	// Use this for initialization
	void Start ()
    {
        playerPoints = new int[8];
        previousPlayerPoints = new int[8];
        disconnectPoints = new int[8];



        for (int i = 0; i < previousPlayerPoints.Length; i++)
        {
            previousPlayerPoints[i] = 0;
        }
	}

    void OnLevelWasLoaded()
    {
        globalVars = GameObject.Find("GameLogic").GetComponent<GlobalVariables>();
        // Check if we are on a game map here to avoid rewriting over previousPlayerPoints

        if (SceneManager.GetActiveScene().name != "Menu" && SceneManager.GetActiveScene().name != "Current Standings")
        {
            for (int i = 0; i < playerPoints.Length; i++)
            {
                previousPlayerPoints[i] = playerPoints[i];
            }
        }
        for (int i = 0; i < playerPoints.Length; i++)
        {
            disconnectPoints[i] = playerPoints[i];
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void AddPointForWinner(GameObject winningObject)
    {
        globalVars.SetPlayerHasWon(true);
        winningNumber =  (int)winningObject.ToString()[6] - 48; // Converting from string to ascii to int
        playerPoints[winningNumber - 1]++;
        //print("Player " + winningNumber + " now has " + playerPoints[winningNumber] + " points!");

        PointParticleForPlayer(winningNumber);
    }

    public void PointParticleForPlayer(int winningNumber)
    {
        GameObject particle = GameObject.Find("plusOneParticle");
        particle.transform.position = GameObject.Find("Player" + winningNumber).transform.position;
        particle.GetComponent<ParticleSystem>().Play();
    }

    public int[] GetPoints()
    {
        return playerPoints;
    }

    public int[] GetPreviousPoints()
    {
        return previousPlayerPoints;
    }

    public void SetPointsOnDisconnect(int playerNumber)
    {
        for (int i = playerNumber; i < previousPlayerPoints.Length; i++)
        {
            try
            {
                previousPlayerPoints[i] = previousPlayerPoints[i + 1];
            }
            catch
            {

            }
            try
            {
                playerPoints[i] = playerPoints[i + 1];
            }
            catch
            {

            }
        }
        PlayerCount playerCount = GameObject.Find("PlayerCount").GetComponent<PlayerCount>();
        if (!SceneManager.GetActiveScene().name.Equals("Menu"))
        {
            try
            {
                previousPlayerPoints[playerCount.GetPlayerCount() - 1] = 0; // Clear points as we move all players down
                playerPoints[playerCount.GetPlayerCount() - 1] = 0;
            }
            catch
            {

            }
        }
        
    }
}
