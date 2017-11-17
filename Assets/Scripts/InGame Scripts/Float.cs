using UnityEngine;
using System.Collections;

public class Float : MonoBehaviour {

    Vector3 offscreenTarget;
    public float offscreenFloatTarget = -100f;
    public float repositionPoint = 100f;
    public float maxOffset = 1f;

	// Use this for initialization
	void Start ()
    {
        offscreenTarget = new Vector3(offscreenFloatTarget, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.MoveTowards(transform.position, offscreenTarget, maxOffset);
        if (transform.position == offscreenTarget)
        {
            transform.position = new Vector3(repositionPoint, transform.position.y, transform.position.z);
        }
	}
}
