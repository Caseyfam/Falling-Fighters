using UnityEngine;
using System.Collections;

public class FadeForSkybox : MonoBehaviour {

	public IEnumerator Fade(float aValue, float aTime)
	{
        /*
		float alpha = thisColor.a;
		for (float t = 0.0f; t < 1.0f; t+= Time.deltaTime / aTime)
		{
			Color newColor = new Color(thisColor.r, thisColor.g, thisColor.b, Mathf.Lerp(alpha, aValue, t));
			transform.GetComponent<Renderer>().material.color = newColor;
			yield return null;
		}
        */

        yield return new WaitForSeconds(aTime);
        GetComponent<Rigidbody>().isKinematic = false;
	}
}
