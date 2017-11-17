using UnityEngine;
using System.Collections;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class DeclareWinner : MonoBehaviour {

    int pointsToWin;
    PointSystem pointSystem;
    public GameObject confettiObject;
    public GameObject[] players;
    public Camera sceneCam;
    public UnityEngine.UI.Text winnerText;
	public FadeForSkybox fade;
    public AudioClip clip;

    int winningPlayerNum;
    bool playerHasWon = false;
    bool camZooming = false;
    bool audioPlaying = false;
    bool fadeText = false;

    void Awake()
    {
        pointsToWin = GameObject.Find("AirConsole").GetComponent<FirstToPoints>().GetFirstToPoints();
        pointSystem = GameObject.Find("PointSystem").GetComponent<PointSystem>();
    }

    void Start()
    {
        for (int i = 0; i < pointSystem.playerPoints.Length; i++)
        {
            if (pointSystem.playerPoints[i] == pointsToWin)
            {
                winningPlayerNum = i;
                playerHasWon = true;
            }
            else if (AirConsole.instance.GetActivePlayerDeviceIds.Count == 1)
            {
                winningPlayerNum = 0;
                playerHasWon = true;
            }
            else if (AirConsole.instance.GetActivePlayerDeviceIds.Count <= 0)
            {
                playerHasWon = true;
            }
        }
        winnerText.color = new Color(winnerText.color.r, winnerText.color.g, winnerText.color.b, 0f);
    }

	// Update is called once per frame
	void Update ()
    {
	    if (playerHasWon)
        {
            StartCoroutine(WaitForZoom(1.4f));
        }
        if (camZooming)
        {
            sceneCam.transform.position = Vector3.MoveTowards(sceneCam.transform.position, new Vector3(players[winningPlayerNum].transform.position.x, players[winningPlayerNum].transform.position.y + 2.5f, players[winningPlayerNum].transform.position.z - 60f), 0.1f);
        }
        if (fadeText)
        {
            winnerText.color = new Color(winnerText.color.r, winnerText.color.b, winnerText.color.b, winnerText.color.a + 0.01f);
        }
	}

    IEnumerator WaitForZoom(float time)
    {
        yield return new WaitForSeconds(time);
        camZooming = true;
        confettiObject.transform.position = players[winningPlayerNum].transform.position;
        confettiObject.SetActive(true);
        if (!audioPlaying)
        {
            GetComponent<AudioSource>().PlayOneShot(clip);
            audioPlaying = true;
        }
        winnerText.text = AirConsole.instance.GetNickname(AirConsole.instance.ConvertPlayerNumberToDeviceId(winningPlayerNum)) + " has won the game!";
        fadeText = true;
		StartCoroutine(fade.Fade(0f, 0f));
        
    }

    public bool GetPlayerHasWon()
    {
        return playerHasWon;
    }
}
