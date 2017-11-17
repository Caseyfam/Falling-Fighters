using UnityEngine;
using System.Collections;

public class FallOnTimer : MonoBehaviour
{
    bool platformFalling = false;

    public int secondsUntilFall; // Int used to see how long a platform should wait to fall after it has been stepped on
    public float rumbleWarning = 2f;

    float platformFallSpeed; //this will determine how fast the platform falls
    public GlobalVariables globalVariables;
    public CameraZoomInStages camZoom;
    PlatformRumble platRumble;
    StoredFallSpeed fallSpeedStorer;
    float zoomTime = 1f;

    private AudioSource audioSource;
    public float volLowRange = .5f;
    public float volHighRange = 1.0f;
    public AudioClip fallSound;

    public IceBandaid iceBandaid;

    bool gameActive;
    bool platformFell = false;

    private float randomTime;
    private float randomBeforeTime = 4f;
    void Start()
    {
        try
        {
            fallSpeedStorer = GameObject.Find("AirConsole").GetComponent<StoredFallSpeed>();
            if (fallSpeedStorer.fallSpeed.Equals("Slow"))
            {
                switch (secondsUntilFall)
                {
                    case 15:
                        secondsUntilFall += 10;
                        break;
                    case 35:
                        secondsUntilFall += 15;
                        break;
                    case 60:
                        secondsUntilFall += 20;
                        break;
                    case 90:
                        secondsUntilFall += 25;
                        break;
                    case 120:
                        secondsUntilFall += 30;
                        break;
                }
            }
            else if (fallSpeedStorer.fallSpeed.Equals("Fast"))
            {
                switch (secondsUntilFall)
                {
                    case 15:
                        secondsUntilFall -= 5;
                        break;
                    case 35:
                        secondsUntilFall -= 10;
                        break;
                    case 60:
                        secondsUntilFall -= 15;
                        break;
                    case 90:
                        secondsUntilFall -= 20;
                        break;
                    case 120:
                        secondsUntilFall -= 25;
                        break;
                }
            }
        }
        catch
        {

        }
        
        randomTime = Random.Range((float)secondsUntilFall - randomBeforeTime, (float)secondsUntilFall);
        platRumble = GetComponent<PlatformRumble>();
    }

    void Awake()
    {
        platformFallSpeed = globalVariables.GetGlobalPlatformFallSpeed();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        gameActive = globalVariables.GetGameActive();
        if (gameActive)
        {
            if (platformFell == false)
            {
                StartCoroutine(CountdownToRumble(randomTime - rumbleWarning));
                StartCoroutine(CountdownToFall(randomTime)); // Literally starts a timer to fall once the round begins
                StartCoroutine(CameraZoom(secondsUntilFall));
                platformFell = true;
            }
        }

        if (platformFalling)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - platformFallSpeed, transform.position.z); // Makes the platform fall
        }
    }

    IEnumerator CountdownToRumble(float time)
    {
        yield return new WaitForSeconds(time);
        platRumble.SetRumbling();
        audioSource.PlayOneShot(fallSound, volHighRange);
    }

    IEnumerator CountdownToFall(float time)
    {
        yield return new WaitForSeconds(time);
        platformFalling = true;
        try
        {
            iceBandaid.BandaidStagePlusOne();
        }
        catch
        {
            // Not in ice stage
        }
        StartCoroutine(SetDisabled(20f)); //Once the platform has been stepped on by a player, it will get disabled in 20 seconds
                                          //so it doesn't keep falling forever
    }

    IEnumerator CameraZoom(float time)
    {
        yield return new WaitForSeconds(time);
        camZoom.ZoomIn(zoomTime);
    }

    IEnumerator SetDisabled(float time)
    {
        yield return new WaitForSeconds(time);
        //this.gameObject.SetActive(false);
        // Temporarily not disabling these because it's not like it's a huge performance loss and because
        // it's messing with other stuff and seriously if anyone reads this it's nearly midnight and I'm tired.

        // Screw it. If you're actually reading this then I congratulate you for looking at this code for whatever reason.
        // Have a Kappa.
        /*
                        
                            ░░░░░░░░░   
                ░░░░▄▀▀▀▀▀█▀▄▄▄▄░░░░
                ░░▄▀▒▓▒▓▓▒▓▒▒▓▒▓▀▄░░
                ▄▀▒▒▓▒▓▒▒▓▒▓▒▓▓▒▒▓█░
                █▓▒▓▒▓▒▓▓▓░░░░░░▓▓█░
                █▓▓▓▓▓▒▓▒░░░░░░░░▓█░
                ▓▓▓▓▓▒░░░░░░░░░░░░█░
                ▓▓▓▓░░░░▄▄▄▄░░░▄█▄▀░
                ░▀▄▓░░▒▀▓▓▒▒░░█▓▒▒░░
                ▀▄░░░░░░░░░░░░▀▄▒▒█░
                ░▀░▀░░░░░▒▒▀▄▄▒▀▒▒█░
                ░░▀░░░░░░▒▄▄▒▄▄▄▒▒█░
                 ░░░▀▄▄▒▒░░░░▀▀▒▒▄▀░░
                ░░░░░▀█▄▒▒░░░░▒▄▀░░░
                ░░░░░░░░▀▀█▄▄▄▄▀
        */
    }
}
