using UnityEngine;
using System.Collections;

public class StoredFallSpeed : MonoBehaviour {

    public string fallSpeed;

    void Start()
    {
        fallSpeed = "Average";
    }

	public void SetStoredFallSpeed (string fallSpeed)
    {
        this.fallSpeed = fallSpeed;
    }
}
