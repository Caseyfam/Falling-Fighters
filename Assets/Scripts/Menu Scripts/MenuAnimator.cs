using UnityEngine;
using System.Collections;

public class MenuAnimator : MonoBehaviour {

    Animator playerAnim;
    public int poseNum;

    void Awake()
    {
        playerAnim = GetComponent<Animator>();
        playerAnim.SetInteger("Pose", poseNum);
    }

}
