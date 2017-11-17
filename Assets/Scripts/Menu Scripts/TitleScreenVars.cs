using UnityEngine;
using System.Collections;

public class TitleScreenVars : MonoBehaviour {

    public bool haveSeenLogo;
    public bool haveSeenTutorial = false;

    public void SetLogoAsSeen()
    {
        haveSeenLogo = true;
    }

}
