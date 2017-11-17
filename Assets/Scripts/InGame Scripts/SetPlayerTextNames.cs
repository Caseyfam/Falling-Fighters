using UnityEngine;
using System.Collections;
using NDream.AirConsole;

public class SetPlayerTextNames : MonoBehaviour {

    UnityEngine.UI.Text playerText;
    private int playerNumber;
    GlobalVariables globalVars;


	// Use this for initialization
	void Start ()
    {
        playerText = GetComponent<UnityEngine.UI.Text>();
        playerNumber = name[6] - 48; // Converting from ASCII back to a recognizable int
        SetPlayerTextName(playerNumber);
        globalVars = GameObject.Find("GameLogic").GetComponent<GlobalVariables>();
	}

    public void SetPlayerTextName (int activePlayer)
    {
        playerText.text = AirConsole.instance.GetNickname(AirConsole.instance.ConvertPlayerNumberToDeviceId(activePlayer - 1));
    }
}
