using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ModelMaterialIdentifier : MonoBehaviour {

    // Will store the model and the material of a fighter for disconnects.

    // Basically, when a player disconnects, all player numbers shift down. Player 3 becomes Player 2. However, we need
    // to make it seem like nothing really changed, so we need to shift player points down, materials, and the fighter
    // model that the player is used to.

    public GameObject[] playerObjects;
    public GameObject[] objectBackups;
    public GameObject[] playerStandingObjects;
    public GameObject[] standingBackups;
    public Material[] scoreMaterials;
    public Material[] scoreBackups;
    public string[] controllerColors;
    public string[] colorsBackups;


    public void SetBackups()
    {
        for (int i = 0; i < playerObjects.Length; i++)
        {
            objectBackups[i] = playerObjects[i];
            standingBackups[i] = playerStandingObjects[i];
            colorsBackups[i] = controllerColors[i];
            scoreBackups[i] = scoreMaterials[i];
        }
    }

    void Awake()
    {
        SetBackups();
    }

    public GameObject GetObject(int playerNum)
    {
        return playerObjects[playerNum];
    }

    public GameObject GetStandingObject(int playerNum)
    {
        return playerStandingObjects[playerNum];
    }

    public void SetObjectsOnDisconnect(int playerNum)
    {
        SetBackups();
        for (int i = playerNum; i < playerObjects.Length; i++)
        {
            try
            {
                playerObjects[i] = playerObjects[i + 1];
            }
            catch
            {
                playerObjects[i] = objectBackups[playerNum];
            }
            try
            {
                //GameObject.Find("Player" + (i + 1) + "Animator");
            }
            catch
            {
                // Index OOB
            }
        }
        for (int i = 0; i < playerObjects.Length; i++)
        {
            if (!SceneManager.GetActiveScene().name.Equals("Current Standings"))
            {
                try
                {
                    ObjectSetter objSet = GameObject.Find("Player" + (i + 1)).GetComponent<ObjectSetter>();
                    Transform parent = GameObject.Find("Player" + (i + 1)).transform;
                    foreach (Transform child in parent)
                    {
                        Destroy(child.gameObject);
                    }
                    objSet.SetObject();
                }
                catch
                {

                }
            }
        }
        SetBackups();
    }

    public void SetStandingsObjectsOnDisconnect(int playerNum)
    {
        SetBackups();
        for (int i = playerNum; i < playerStandingObjects.Length; i++)
        {
            try
            {
                playerStandingObjects[i] = playerStandingObjects[i + 1];
            }
            catch
            {
                playerStandingObjects[i] = standingBackups[playerNum];
            }
            try
            {
                //GameObject.Find("Player" + (i + 1) + "Animator");
            }
            catch
            {
                // Index OOB
            }
        }
        
        SetBackups();
    }

    public void SetControllerColorsOnDisconnect(int playerNum)
    {
        SetBackups();
        for (int i = playerNum; i < controllerColors.Length; i++)
        {
            try
            {
                controllerColors[i] = controllerColors[i + 1];
            }
            catch
            {
                controllerColors[i] = colorsBackups[playerNum];
            }
        }
        SetBackups();
    }

    public void SetScoreColorsOnDisconnect(int playerNum)
    {
        SetBackups();
        for (int i = playerNum; i < scoreMaterials.Length; i++)
        {
            try
            {
                scoreMaterials[i] = scoreMaterials[i + 1];
            }
            catch
            {
                scoreMaterials[i] = scoreBackups[playerNum];
            }
        }
        SetBackups();
    }

}
