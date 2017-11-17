using UnityEngine;
using System.Collections;

public class PlayerStandingsAnimator : MonoBehaviour {

    Animator playerAnim;
    int winningNumber;
    int playerNum;
    void Awake()
    {
        playerAnim = GetComponentInChildren<Animator>();
        winningNumber = GameObject.Find("PointSystem").GetComponent<PointSystem>().winningNumber;
        playerNum = (int)name.ToString()[6] - 48;
    }

	// Use this for initialization
	void Start ()
    {
        //Debug.Log(playerNum);
	    if (winningNumber == playerNum) // This player did win
        {
            playerAnim.SetBool("DidWin", true);
            playerAnim.SetInteger("WinNumber", Random.Range(0, 6));
        }
        else // This player did not win
        {
            playerAnim.SetBool("DidWin", false);
            playerAnim.SetInteger("LoseNumber", Random.Range(0, 6));
        }
        // This isn't done yet. What if you win the round, but are still behind? Should probably make it so that the winner of the round celebrates and
        // the people who have the most points celebrate.
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}
}
