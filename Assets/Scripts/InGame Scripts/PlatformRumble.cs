using UnityEngine;
using System.Collections;

public class PlatformRumble : MonoBehaviour {

    bool isRumbling = false;
    Vector3 originalPos;

	// Use this for initialization
	void Start ()
    {
        originalPos = transform.position;    
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (isRumbling)
        {
            int i = Random.Range(0, 3);
            switch (i)
            {
                case 0:
                    transform.position = Vector3.Lerp(transform.position, new Vector3(originalPos.x + 1, transform.position.y, originalPos.z), 0.08f);
                    break;
                case 1:
                    transform.position = Vector3.Lerp(transform.position, new Vector3(originalPos.x, transform.position.y, originalPos.z + 1), 0.08f);
                    break;
                case 2:
                    transform.position = Vector3.Lerp(transform.position, new Vector3(originalPos.x - 1, transform.position.y, originalPos.z), 0.08f);
                    break;
                case 3:
                    transform.position = Vector3.Lerp(transform.position, new Vector3(originalPos.x, transform.position.y, originalPos.z - 1), 0.08f);
                    break;
            }
        }
	}
    
    public void SetRumbling()
    {
        isRumbling = true;
    }
}
