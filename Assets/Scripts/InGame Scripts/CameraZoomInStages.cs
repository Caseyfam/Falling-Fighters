using UnityEngine;
using System.Collections;

public class CameraZoomInStages : MonoBehaviour {

    Camera thisCamera;
    bool shouldZoom = false;

	// Update is called once per frame
	void Update ()
    {
        if (shouldZoom)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), 0.1f);
        }
    }

    public void ZoomIn(float zoomTime)
    {
        shouldZoom = true;
        StartCoroutine(ZoomTimer(zoomTime));
    }

    IEnumerator ZoomTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        shouldZoom = false;
    }
}
