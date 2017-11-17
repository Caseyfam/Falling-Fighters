using UnityEngine;
using System.Collections;

public class CopyAlphaOfParent : MonoBehaviour {

    public UnityEngine.UI.Image parentImage;
    UnityEngine.UI.Image thisImage;

    void Awake()
    {
        thisImage = GetComponent<UnityEngine.UI.Image>();
    }
	// Update is called once per frame
	void Update ()
    {
        thisImage.color = parentImage.color;
	}
}
