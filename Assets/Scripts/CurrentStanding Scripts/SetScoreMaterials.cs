using UnityEngine;
using System.Collections;

public class SetScoreMaterials : MonoBehaviour {

    ModelMaterialIdentifier modelMat;
    public int playerNum;
    // Use this for initialization
    void Awake()
    {
        modelMat = GameObject.Find("AirConsole").GetComponent<ModelMaterialIdentifier>();
    }

    void Start ()
    {
        GetComponent<Renderer>().material = modelMat.scoreMaterials[playerNum - 1];
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
