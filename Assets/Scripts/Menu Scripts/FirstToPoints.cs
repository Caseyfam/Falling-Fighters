using UnityEngine;
using System.Collections;

public class FirstToPoints : MonoBehaviour {

    private int firstToPoints = 5;

    public void SetFirstToPoints(int points)
    {
        if (points >= 1 && points <= 5)
        {
            firstToPoints = points;
        }
    }

    public int GetFirstToPoints()
    {
        return firstToPoints;
    }
}
