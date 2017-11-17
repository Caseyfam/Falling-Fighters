using UnityEngine;
using System.Collections;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class TitleScreenVisibles : MonoBehaviour {

    public UnityEngine.UI.Text deviceCount, connections, menuText;
    public TextMesh pressContinue;
    public UnityEngine.UI.Image menuBack;
    public Sprite logo;
    public TitleScreenVars titleScreenVars;
    public GameObject logoContainer;
    public Camera mainCam;
    public MenuControls menuControls;

    private bool logoShowing;
    private bool transitioningToMenu = false;

    public Vector3 camMenuPosition;
    public Vector3 camLogoPosition;

    AudioSource source;
    public AudioClip logoNoise;

    bool upOrDown = false;

    void Awake()
    {
        AirConsole.instance.onMessage += OnMessage;
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        SetMenuAlphas(0f);

        logoShowing = true;
        mainCam.transform.position = camLogoPosition;

        StartCoroutine(PressStartAppear(2.5f));
    }

    void OnMessage (int deviceID, JToken data)
    {
        try
        {
            int activePlayer = AirConsole.instance.ConvertDeviceIdToPlayerNumber(deviceID);

            if (activePlayer == 0)
            {
                if (logoShowing)
                {
                    if ((bool)data["A"]["pressed"])
                    {
                        titleScreenVars.SetLogoAsSeen();
                        logoShowing = false;
                        menuControls.PressDelay(1f);
                        transitioningToMenu = true;
                        AirConsole.instance.onMessage -= OnMessage;
                        upOrDown = true;
                        source.PlayOneShot(logoNoise);
                    }
                }
            }
        }
        catch
        {

        }
       
    }

    void SetMenuAlphas(float alpha)
    {
        deviceCount.color = new Color(deviceCount.color.r, deviceCount.color.g, deviceCount.color.b, alpha);
        connections.color = new Color(connections.color.r, connections.color.g, connections.color.b, alpha);
        menuText.color = new Color(menuText.color.r, menuText.color.g, menuText.color.b, alpha);
        menuBack.color = new Color(menuBack.color.r, menuBack.color.g, menuBack.color.b, alpha);
    }

	// Update is called once per frame
	void Update ()
    {
	    if (transitioningToMenu)
        {
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, camMenuPosition, 0.01f);
            try
            {
                deviceCount.color = new Color(deviceCount.color.r, deviceCount.color.g, deviceCount.color.b, deviceCount.color.a + 0.01f);
                connections.color = new Color(connections.color.r, connections.color.g, connections.color.b, deviceCount.color.a + 0.01f);
                menuText.color = new Color(menuText.color.r, menuText.color.g, menuText.color.b, deviceCount.color.a + 0.01f);
                menuBack.color = new Color(menuBack.color.r, menuBack.color.g, menuBack.color.b, deviceCount.color.a + 0.01f);
            }
            catch
            {
                
            }
        }
        try
        {
            if (!upOrDown)
            {
                logoContainer.transform.localPosition = Vector3.Lerp(logoContainer.transform.localPosition, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane)), 0.05f);
            }
            else
            {
                logoContainer.transform.localPosition = Vector3.Lerp(logoContainer.transform.localPosition, logoContainer.transform.localPosition + new Vector3(0f, 10f, 0f), 0.05f);
            }
            
        }
        catch
        {
            // Logo was destroyed
            
        }

	}

    IEnumerator PressStartAppear(float time)
    {
        yield return new WaitForSeconds(time);
        pressContinue.color = new Color(pressContinue.color.r, pressContinue.color.g, pressContinue.color.b, 1f);
    }
}
