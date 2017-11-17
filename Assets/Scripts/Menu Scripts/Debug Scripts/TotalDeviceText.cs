using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class TotalDeviceText : MonoBehaviour
{

    UnityEngine.UI.Text thisText;
    // Use this for initialization
    void Start()
    {
        thisText = GetComponent<UnityEngine.UI.Text>();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            thisText.text = "Total Device Count: " + AirConsole.instance.GetControllerDeviceIds().Count;
        }
        catch
        {
            thisText.text = "Total Device Count: 0";
        }
        

    }
}
