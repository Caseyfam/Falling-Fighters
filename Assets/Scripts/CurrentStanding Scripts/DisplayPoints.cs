using UnityEngine;
using System.Collections;
using NDream.AirConsole;

public class DisplayPoints : MonoBehaviour {

    public float pointRaiseSpeed = 0.1f;
    PointSystem pointSystem;
    int playerCount;
    int[] previousPlayerPoints;
    public GameObject[] blocks;

    void Awake()
    {
        pointSystem = GameObject.Find("PointSystem").GetComponent<PointSystem>();
        playerCount = GameObject.Find("PlayerCount").GetComponent<PlayerCount>().GetPlayerCount();
        previousPlayerPoints = pointSystem.GetPreviousPoints();
    }

	void Start ()
    {
        for (int i = 0; i < playerCount; i++)
        {
            SetPlayerNames(i);
        }
        for (int i = playerCount; i < 8; i++)
        {
            GameObject.Find("Player" + (i + 1) + "Name").SetActive(false);
            GameObject.Find("Player" + (i + 1)).SetActive(false);
            GameObject.Find("Score" + (i + 1)).SetActive(false);
        }
        for (int i = 0; i < 8; i++)
        {
            SetPreviousBlockHeight(i);
        }

    }
	
	void Update ()
    {
        for (int i = 0; i < playerCount; i++)
        {
            SetBlockHeight(i);
        }

    }

    void SetPlayerNames(int activePlayer)
    {
        string playerName;
        playerName = AirConsole.instance.GetNickname(AirConsole.instance.ConvertPlayerNumberToDeviceId(activePlayer));
        GameObject.Find("Player" + (activePlayer + 1) + "Name").GetComponent<TextMesh>().text = playerName;
    }

    void SetBlockHeight(int activePlayer) // Scales the block height to visually represent points won
    {
        int[] playerPoints = pointSystem.disconnectPoints;

        //thisBlock.transform.localScale = new Vector3(5, 5 * playerPoints[activePlayer], 5);
        if (playerPoints[activePlayer] != 0)
        {
           // Debug.Log("Should be scaling");
            blocks[activePlayer].transform.localScale = Vector3.MoveTowards(blocks[activePlayer].transform.localScale, new Vector3(5, 5 * playerPoints[activePlayer], 5), pointRaiseSpeed);
        }
        
    }

    void SetPreviousBlockHeight(int activePlayer)
    {
        // Will make it look like the block heights stayed the same between rounds
        try
        {
            if (previousPlayerPoints[activePlayer] != 0)
            {
                GameObject score = GameObject.Find("Score" + (activePlayer + 1));
                GameObject player = GameObject.Find("Player" + (activePlayer + 1));
                float scoreHeight;

                score.transform.localScale = new Vector3(5, 5 * previousPlayerPoints[activePlayer], 5);
                scoreHeight = score.GetComponent<Collider>().bounds.extents.y;

                player.transform.position = new Vector3(player.transform.position.x, score.transform.position.y + scoreHeight, player.transform.position.z);

            }
        }
        catch
        {
            print("Could not set the score box for player " + (activePlayer + 1));
        }
    }
}
