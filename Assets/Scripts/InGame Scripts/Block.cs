using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    bool isBlocking = false;

    public bool getIsBlocking()
    {
        return isBlocking;
    }

    public void setIsBlocking(bool status)
    {
        isBlocking = status;
    }
}
