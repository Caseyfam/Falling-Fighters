using UnityEngine;
using System.Collections;

public class SkyboxRotate : MonoBehaviour
{
    public float speed;
    public bool zOrX = true;

    // Update is called once per frame
    void Update()
    {
        if (zOrX)
        {
            transform.Rotate(new Vector3(0, 1, 0), speed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, 1), speed * Time.deltaTime);
        }
    }
}
