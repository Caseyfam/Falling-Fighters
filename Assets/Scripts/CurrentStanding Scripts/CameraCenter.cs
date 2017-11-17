using UnityEngine;
using System.Collections;

public class CameraCenter : MonoBehaviour {

    int playerCount;

    float avgX = 0;

	// Use this for initialization
	void Start ()
    {
        playerCount = GameObject.Find("PlayerCount").GetComponent<PlayerCount>().GetPlayerCount();
        if (playerCount == 1)
        {
            avgX = GameObject.Find("Score1").transform.position.x;
            transform.position = new Vector3(avgX, transform.position.y, transform.position.z);
        }
        else
        {
            for (int i = 1; i < playerCount + 1; i++)
            {
                avgX += GameObject.Find("Score" + i).transform.position.x;
            }
            avgX = avgX / playerCount;
            transform.position = new Vector3(avgX, transform.position.y, transform.position.z);
        }
        
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
