using UnityEngine;
using System.Collections;

public class PlayerCount : MonoBehaviour {

    private int playerCount;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void SetPlayerCount (int playerCount)
    {
        this.playerCount = playerCount;
    }

    public int GetPlayerCount()
    {
        return playerCount;
    }
}
