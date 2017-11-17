using UnityEngine;
using System.Collections;

public class IceBandaid : MonoBehaviour {

    int fallStage = 0;
	// Update is called once per frame
	public void BandaidStagePlusOne ()
    {
        fallStage++;
        CheckForScale(fallStage);
	}

    void CheckForScale(int fallStage)
    {
        switch (fallStage)
        {
            default:
                break;
            case 1:
                transform.localScale = new Vector3(37.47676f, 1f, 37.47676f);
                break;
            case 5:
                transform.localScale = new Vector3(26f, 1f, 25f);
                break;
            case 9:
                transform.localScale = new Vector3(16.3292f, 1f, 16.22428f);
                break;
            case 13:
                Destroy(gameObject);
                break;
        }

    }
}
