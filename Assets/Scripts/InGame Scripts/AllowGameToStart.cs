using UnityEngine;
using System.Collections;

public class AllowGameToStart : MonoBehaviour {

    float timeLeft = 3f;
    bool allowClockToRun = true;

    public GlobalVariables globalVars;
    public UnityEngine.UI.Text goText;
    string goTextString;
    AudioSource source;
    public AudioClip earlyClash;
    public AudioClip goclash;
    bool play2Once = true, play1Once = true, playGoOnce = true;

	// Use this for initialization
	void Start ()
    {
        source = GetComponent<AudioSource>();
        goTextString = "3";
        source.PlayOneShot(earlyClash);
	}
	
	// Update is called once per frame
	void Update ()
    {
        goText.text = goTextString; // Prints the calculated string to the canvas
        
        if (allowClockToRun == true)
        {
            timeLeft -= Time.deltaTime;
        }
        if (timeLeft <= 2 && timeLeft > 1)
        {
            if (play2Once)
            {
                source.PlayOneShot(earlyClash);
                play2Once = false;
            }
            goTextString = "2";
        }
        else if (timeLeft <= 1 && timeLeft > 0)
        {
            if (play1Once)
            {
                source.PlayOneShot(earlyClash);
                play1Once = false;
            }
            goTextString = "1";
        }
        else if (timeLeft <= 0 && timeLeft > -1)
        {
            if (playGoOnce)
            {
                source.PlayOneShot(goclash);
                playGoOnce = false;
            }
            goTextString = "Fight!";
            globalVars.SetGameActive(true);
            globalVars.SetPlayersCanMove(true);
            // Still need to make it so players can only move once the game starts
        }
        else if(timeLeft < -1 && timeLeft > -10)
        {
            goText.transform.position = Vector3.Lerp(goText.transform.position, goText.transform.position - new Vector3(-15f, 0f, 0f), 1f);
        }
        else if (timeLeft < -10)
        {
            allowClockToRun = false;
            goText.gameObject.SetActive(false);
        }
    }
}
