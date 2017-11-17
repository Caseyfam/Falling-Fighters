using UnityEngine;
using System.Collections;

public class TextFadeOut : MonoBehaviour
{

    public GlobalVariables globalVars;
    bool canFade = false;

    // Update is called once per frame
    void Update()
    {
        if (globalVars.GetGameActive())
        {
            StartCoroutine(WaitToFade(2f));
            foreach (Transform child in transform)
            {
                if (canFade)
                {
                    child.GetComponent<CanvasRenderer>().SetAlpha(child.GetComponent<CanvasRenderer>().GetAlpha() - 0.01f);
                }
            }
        }
    }

    IEnumerator WaitToFade(float time)
    {
        yield return new WaitForSeconds(time);
        canFade = true;
    }
}
