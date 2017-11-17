using UnityEngine;
using System.Collections;

public class MenuText : MonoBehaviour {

    private UnityEngine.UI.Text thisText;

	// Use this for initialization
	void Start ()
    {
        thisText = GetComponent<UnityEngine.UI.Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void SetMenuText(string desiredText)
    {
        thisText.text = desiredText;
    }
}
