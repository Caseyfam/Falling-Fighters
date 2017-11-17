using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {

    Camera thisCam;
    public float speed;
    public bool zOrX = true;

    void Awake()
    {
        thisCam = GetComponent<Camera>();
    }

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (zOrX)
        {
            thisCam.transform.Rotate(new Vector3(0, 1, 0), speed * Time.deltaTime);
        }
        else
        {
            thisCam.transform.Rotate(new Vector3(0, 0, 1), speed * Time.deltaTime);
        }
	}
}
