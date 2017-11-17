using UnityEngine;
using System.Collections;

public class TextFollowPlayer : MonoBehaviour {

    public GameObject targetObj;
    public Camera mainCamera;
    private float targetX;
    private float targetY;
    public Vector3 textOffset;

	void Update ()
    {
        try // Try to follow a gameobject
        {
            targetX = mainCamera.WorldToScreenPoint(targetObj.transform.position).x;
            targetY = mainCamera.WorldToScreenPoint(targetObj.transform.position).y;

            transform.position = new Vector2(targetX + textOffset.x, targetY + textOffset.y);
        }
        catch // If a gameObject does not exist... (we actually need this since gameObjects have the 
              // chance of being destroyed when the round starts
        {
            Destroy(this.gameObject);
        }
	}
}
