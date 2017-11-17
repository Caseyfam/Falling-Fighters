using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class MenuControls : MonoBehaviour {

    private int menuSelectionNumber = 0; // What option are we selecting?
    private int optionsMenuSelectionNum = 0;

    private float controllerX, controllerY;

    public string selection1;
    public string selection2;
    public string selection3;
    public string selection4;

    private bool pointerMove = true;
    private int firstTo = 5;
    private bool pressDelay = false;
    private string fallSpeed = "Average";

    MenuText menuText;
    FirstToPoints firstToPointsStorer;
    StoredFallSpeed fallSpeedStorer;

    bool tutVisible = false;
    private AudioSource source;
    public AudioClip selectionAudio, startAudio;
    public TitleScreenVars titleScreen;
    public ControllerConnect controllerConnect;

    public UnityEngine.UI.Image menuBack, credits, playerError, tutorial;

    private int subMenus = 0;

    void Awake()
    {
        AirConsole.instance.onMessage += OnMessage;
        firstToPointsStorer = GameObject.Find("AirConsole").GetComponent<FirstToPoints>();
        fallSpeedStorer = GameObject.Find("AirConsole").GetComponent<StoredFallSpeed>();
        source = GetComponent<AudioSource>();
    }

    public void ClearControllers()
    {
        AirConsole.instance.onMessage -= OnMessage;
    }

    // Use this for initialization
    void Start ()
    {
        menuText = GameObject.Find("MenuSelections").GetComponent<MenuText>();
        firstTo = firstToPointsStorer.GetFirstToPoints();
        fallSpeed = fallSpeedStorer.fallSpeed;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (tutVisible)
        {
            tutorial.color = new Color(tutorial.color.r, tutorial.color.g, tutorial.color.b, tutorial.color.a + 0.01f);
        }
        if (titleScreen.haveSeenLogo)
        {
            switch (subMenus)
            {
                case 0:
                    switch (menuSelectionNumber) // Obviously, these are all just debug menu options
                    {
                        default:
                            menuText.SetMenuText("> " + selection1 + "\n" + selection2 + "\n" + selection3 + "\n" + selection4);
                            break;
                        case 0:
                            menuText.SetMenuText("> " + selection1 + "\n" + selection2 + "\n" + selection3 + "\n" + selection4);
                            break;
                        case 1:
                            menuText.SetMenuText(selection1 + "\n> " + selection2 + "\n" + selection3 + "\n" + selection4);
                            break;
                        case 2:
                            menuText.SetMenuText(selection1 + "\n" + selection2 + "\n> " + selection3 + "\n" + selection4);
                            break;
                        case 3:
                            menuText.SetMenuText(selection1 + "\n" + selection2 + "\n" + selection3 + "\n> " + selection4);
                            break;
                    }
                    break;
                case 1:
                    switch (optionsMenuSelectionNum) // Game Options Menu
                    {
                        default:
                            menuText.SetMenuText("> First To: " + firstTo + "\n " + "Crumble Speed: " + fallSpeed + "\n " + "Return");
                            break;
                        case 0:
                            menuText.SetMenuText("> First To: " + firstTo + "\n " + "Crumble Speed: " + fallSpeed + "\n " + "Return");
                            break;
                        case 1:
                            menuText.SetMenuText("First To: " + firstTo + "\n " + "> Crumble Speed: " + fallSpeed + "\n " + "Return");
                            break;
                        case 2:
                            menuText.SetMenuText("First To: " + firstTo + "\n " + "Crumble Speed: " + fallSpeed + "\n " + "> Return");
                            break;
                    }
                    break;
                case 2:
                    // Credits go here
                    credits.color = new Color(credits.color.r, credits.color.g, credits.color.b, 1f);
                    break;
                case 3:
                        playerError.color = new Color(playerError.color.r, playerError.color.g, playerError.color.b, 1f);
                    break;
                default:
                    break;
            }
            
        } 
        else
        {

        }
	    
	}

    void OnMessage (int deviceID, JToken data)
    {
        int activePlayer = AirConsole.instance.ConvertDeviceIdToPlayerNumber(deviceID);

        if (activePlayer == 0 && titleScreen.haveSeenLogo) 
        {
            //print("Controller Y: " + controllerY);

            try
            {

                if ((bool)data["dpad-left"]["pressed"])
                {
                    if (data["dpad-left"]["message"]["direction"].ToString().Equals("down"))
                    {
                        controllerY = -1;
                        //Debug.Log("Stored down");
                    }
                    else if (data["dpad-left"]["message"]["direction"].ToString().Equals("up"))
                    {
                        controllerY = 1;
                        //Debug.Log("Stored up");
                    }
                    else if (data["dpad-left"]["message"]["direction"].ToString().Equals("right"))
                    {
                        controllerX = 1;
                        //Debug.Log("Stored right");
                    }
                    else if (data["dpad-left"]["message"]["direction"].ToString().Equals("left"))
                    {
                        controllerX = -1;
                        //Debug.Log("Stored left");
                    }
                    else
                    {
                        //Debug.Log("Did not parse JToken");
                    }
                }
                else
                {
                    pointerMove = true;
                    controllerX = 0;
                    controllerY = 0;
                }
            }
            catch
            {
                // Couldn't parse controller
            }
           

            if (SceneManager.GetActiveScene().name == "Menu" && subMenus == 0)
            {
                if (controllerY == -1 && menuSelectionNumber < 2 && pointerMove == true)
                {
                    menuSelectionNumber++;
                    source.PlayOneShot(selectionAudio);
                    pointerMove = false;
                }
                else if (controllerY == 1 && menuSelectionNumber > 0 && pointerMove == true)
                {
                    menuSelectionNumber--;
                    source.PlayOneShot(selectionAudio);
                    pointerMove = false;
                } 
                try
                {
                    PerformMenuAction(data);
                }
                catch
                {
                    // If you don't swipe or move the joystick, AirConsole sends nulls. It's awful.
                }
            }
            if (SceneManager.GetActiveScene().name == "Menu" && subMenus == 1)
            {
                if (controllerY == -1 && optionsMenuSelectionNum < 2 && pointerMove == true)
                {
                    optionsMenuSelectionNum++;
                    source.PlayOneShot(selectionAudio);
                    pointerMove = false;
                }
                else if (controllerY == 1 && optionsMenuSelectionNum > 0 && pointerMove == true)
                {
                    optionsMenuSelectionNum--;
                    source.PlayOneShot(selectionAudio);
                    pointerMove = false;
                }
                
            }
        }

        if (subMenus == 1)
        {
            switch (optionsMenuSelectionNum)
            {
                default:
                    optionsMenuSelectionNum = 0;
                    break;
                case 0:
                    if (controllerX == 1)
                    {
                        if (firstTo < 5)
                        {
                            firstTo++;
                            source.PlayOneShot(selectionAudio);
                            firstToPointsStorer.SetFirstToPoints(firstTo);
                        }
                    }
                    else if (controllerX == -1)
                    {
                        if (firstTo > 1)
                        {
                            firstTo--;
                            source.PlayOneShot(selectionAudio);
                            firstToPointsStorer.SetFirstToPoints(firstTo);
                        }
                    }
                    break;
                case 1:
                    if (controllerX == 1)
                    {
                        if (fallSpeed.Equals("Average"))
                        {
                            fallSpeed = "Fast";
                            source.PlayOneShot(selectionAudio);
                        }
                        else if (fallSpeed.Equals("Slow"))
                        {
                            fallSpeed = "Average";
                            source.PlayOneShot(selectionAudio);
                        }
                    }
                    else if (controllerX == -1)
                    {
                        if (fallSpeed.Equals("Fast"))
                        {
                            fallSpeed = "Average";
                            source.PlayOneShot(selectionAudio);
                        }
                        else if (fallSpeed.Equals("Average"))
                        {
                            fallSpeed = "Slow";
                            source.PlayOneShot(selectionAudio);
                        }
                    }
                    fallSpeedStorer.SetStoredFallSpeed(fallSpeed);
                    break;
                case 2:
                    try
                    {
                        if ((bool)data["A"]["pressed"] && pressDelay)
                        {
                            menuSelectionNumber = 0;
                            optionsMenuSelectionNum = 0;
                            subMenus = 0;
                            source.PlayOneShot(selectionAudio);
                        }
                    }
                    catch
                    {
                        // No Button in JToken
                    }
                    break;
            }
            
        }
        if (subMenus == 2)
        {
            if ((bool)data["A"]["pressed"] && pressDelay)
            {
                menuSelectionNumber = 0;
                optionsMenuSelectionNum = 0;
                credits.color = new Color(credits.color.r, credits.color.g, credits.color.b, 0f);
                subMenus = 0;
                source.PlayOneShot(selectionAudio);
            }
        }

        if (subMenus == 3)
        {
            if ((bool)data["A"]["pressed"] && pressDelay)
            {
                playerError.color = new Color(playerError.color.r, playerError.color.g, playerError.color.b, 0f);
                menuSelectionNumber = 0;
                subMenus = 0;
                source.PlayOneShot(selectionAudio);
            }
        }
    }

    public void PressDelay (float time)
    {
        StartCoroutine(PressButtonDelay(time));
    }

    public IEnumerator PressButtonDelay (float time)
    {
        pressDelay = false;
        yield return new WaitForSeconds(time);
        pressDelay = true;
    }

    bool startOnce = false;
    public void StartGame()
    {
        if (!startOnce)
        {
            ClearControllers();
            if (GameObject.Find("AirConsole").GetComponent<TitleScreenVars>().haveSeenTutorial)
            {
                StartCoroutine(WaitForStartSound(0.2f));
                StartCoroutine(DelayedLoadScene("Map1"));
                startOnce = true;              
            }
            else
            {
                StartCoroutine(WaitForTutorial(7f));
                GameObject.Find("AirConsole").GetComponent<TitleScreenVars>().haveSeenTutorial = true;
            }
        }
        
    }
    public IEnumerator WaitForTutorial(float time)
    {
        tutVisible = true;
        yield return new WaitForSeconds(time);
        StartCoroutine(WaitForStartSound(0.2f));
        StartCoroutine(DelayedLoadScene("Map1"));
        startOnce = true;
    }

    public IEnumerator WaitForStartSound(float time)
    {
        yield return new WaitForSeconds(time);
        source.PlayOneShot(startAudio);
    }

    void PerformMenuAction (JToken data)
    {
        switch (menuSelectionNumber)
        {
            default:
                break;
            case 0:
                if (AirConsole.instance.GetActivePlayerDeviceIds.Count > 1)
                {
                    if ((bool)data["A"]["pressed"] && pressDelay)
                    {
                        GetComponent<PlayerSelect>().SetPlayerSelectActive();
                        subMenus = 5; // Menu doesn't recognize inputs anymore
                    }
                }
                else // This game requires at least two players
                {
                    if ((bool)data["A"]["pressed"] && pressDelay)
                    {
                        subMenus = 3;
                        source.PlayOneShot(selectionAudio);
                        PressDelay(1f);
                    }
                }
                break;
            case 1:
                if ((bool)data["A"]["pressed"] && pressDelay)
                {
                    subMenus = 1;
                    optionsMenuSelectionNum = 0;
                    source.PlayOneShot(selectionAudio);
                }

                break;
            case 2:
                if ((bool)data["A"]["pressed"] && pressDelay)
                {
                    subMenus = 2;
                    optionsMenuSelectionNum = 0;
                    source.PlayOneShot(selectionAudio);
                    PressDelay(1f);
                }
                //Debug.Log("Miguel Barboza\nKarl Ferraren\nKevin Gannon\nMylene Haus\nJohn Laschober");

                // Make this actually appear somewhere for the player to see
                break;
        }
    }

    IEnumerator DelayedLoadScene(string sceneName)
    {
        float fadeTime = GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(sceneName);
    }
}
