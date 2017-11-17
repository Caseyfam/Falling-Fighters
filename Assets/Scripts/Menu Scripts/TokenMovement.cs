using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;
using NDream.AirConsole;

public class TokenMovement : MonoBehaviour {

    public bool tokenCanMove = false;

    Vector2 startPos;
    RectTransform thisTransform;
    int selectionNumber = 1;
    public PlayerSelect playerSelect;
    public ModelMaterialIdentifier modelMat;
    public int playerNumber;

    public GameObject[] playerObjects;
    public GameObject[] playerStandingObjects;
    public Material[] standingColors;
    AudioSource source;
    public AudioClip soundClip;
    public AudioClip select;
    public AudioClip error;

    bool pushDelay = false;
	// Use this for initialization

    void Awake()
    {
        source = GetComponent<AudioSource>();
        modelMat = GameObject.Find("AirConsole").GetComponent<ModelMaterialIdentifier>();
    }

	void Start ()
    {
        thisTransform = GetComponent<RectTransform>();
        startPos = GetComponent<RectTransform>().anchoredPosition;
	}
	
    public IEnumerator PushDelay(float time)
    {
        yield return new WaitForSeconds(time);
        pushDelay = true;
    }
    public void SetTokenCanMove()
    {
        tokenCanMove = true;
        gameObject.SetActive(true);
    }

    public void SetTokenCannotMove()
    {
        tokenCanMove = false;
        gameObject.SetActive(false);
    }

    public void SendTokenData(JToken data)
    {
        try
        {
            if ((bool)data["A"]["pressed"] && tokenCanMove && pushDelay)
            {
                if (!playerSelect.PlayerIsSelected(selectionNumber)) // if Player is not grayed out
                {
                    playerSelect.GrayOutPlayer(selectionNumber);
                    tokenCanMove = false;
                    // Store what costume player selected
                    StoreCostume();
                    source.PlayOneShot(select);
                }
            }
        }
        catch
        {

        }
        try
        {
            if (tokenCanMove)
            {
                if (data["dpad-left"]["message"]["direction"].ToString().Equals("right") && (bool)data["dpad-left"]["pressed"])
                {
                    if (selectionNumber < 8)
                    {
                        selectionNumber++;
                        source.PlayOneShot(soundClip);
                    }
                }
                else if (data["dpad-left"]["message"]["direction"].ToString().Equals("left") && (bool)data["dpad-left"]["pressed"])
                {
                    if (selectionNumber > 1)
                    {
                        selectionNumber--;
                        source.PlayOneShot(soundClip);
                    }
                }
                else if (data["dpad-left"]["message"]["direction"].ToString().Equals("up") && (bool)data["dpad-left"]["pressed"])
                {
                    if (selectionNumber > 4)
                    {
                        selectionNumber -= 4;
                        source.PlayOneShot(soundClip);
                    }
                }
                else if (data["dpad-left"]["message"]["direction"].ToString().Equals("down") && (bool)data["dpad-left"]["pressed"])
                {
                    if (selectionNumber < 5)
                    {
                        selectionNumber += 4;
                        source.PlayOneShot(soundClip);
                    }
                }
            }
        }
        catch
        {

        }     
    }

    public void StoreCostume()
    {
        modelMat.playerObjects[playerNumber - 1] = playerObjects[selectionNumber - 1];
        modelMat.playerStandingObjects[playerNumber - 1] = playerStandingObjects[selectionNumber - 1];
        modelMat.scoreMaterials[playerNumber - 1] = standingColors[selectionNumber - 1];

        Message colorMessage = new Message();
        // Change controller color too!
        switch (selectionNumber)
        {
            case 1:
                colorMessage.color = "#ad1d1d";
                break;
            case 2:
                colorMessage.color = "#d3641f";
                break;
            case 3:
                colorMessage.color = "#d3cd1f";
                break;
            case 4:
                colorMessage.color = "#70d31f";
                break;
            case 5:
                colorMessage.color = "#1f6ad3";
                break;
            case 6:
                colorMessage.color = "#862d8e";
                break;
            case 7:
                colorMessage.color = "#000000";
                break;
            case 8:
                colorMessage.color = "#ffffff";
                break;
        }
        AirConsole.instance.Message(AirConsole.instance.ConvertPlayerNumberToDeviceId(playerNumber - 1), JToken.FromObject(colorMessage));
        modelMat.SetBackups();
    }

	// Update is called once per frame
	void Update ()
    {
        if (tokenCanMove)
        {
            switch (selectionNumber)
            {
                default:
                    selectionNumber = 1;
                    thisTransform.anchoredPosition = new Vector2(startPos.x, startPos.y);
                    break;
                case 1:
                    thisTransform.anchoredPosition = new Vector2(startPos.x, startPos.y);
                    break;
                case 2:
                    thisTransform.anchoredPosition = new Vector2(startPos.x + 100f, startPos.y);
                    break;
                case 3:
                    thisTransform.anchoredPosition = new Vector2(startPos.x + 200f, startPos.y);
                    break;
                case 4:
                    thisTransform.anchoredPosition = new Vector2(startPos.x + 300f, startPos.y);
                    break;
                case 5:
                    thisTransform.anchoredPosition = new Vector2(startPos.x, startPos.y - 100f);
                    break;
                case 6:
                    thisTransform.anchoredPosition = new Vector2(startPos.x + 100f, startPos.y - 100f);
                    break;
                case 7:
                    thisTransform.anchoredPosition = new Vector2(startPos.x + 200f, startPos.y - 100f);
                    break;
                case 8:
                    thisTransform.anchoredPosition = new Vector2(startPos.x + 300f, startPos.y - 100f);
                    break;
            }
        }
	    
	}
}
