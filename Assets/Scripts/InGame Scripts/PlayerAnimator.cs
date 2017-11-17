using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour {

    Animator playerAnim;
    float distanceToGround;

    private AudioSource source;
    public AudioClip fallingAudio;
    public AudioClip step1, step2, step3;

    void Awake()
    {
        playerAnim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        playerAnim = GetComponent<Animator>();
        if (IsGrounded())
        {
            playerAnim.SetBool("Falling", false);

        }
        else
        {
            playerAnim.SetBool("Falling", true);
        }
        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("standing_melee_attack_horizontal"))
        {
            playerAnim.SetBool("Attacking", false);
        }
	}

    public bool IsGrounded()
    {
        //return Physics.Raycast(transform.position, -Vector2.up, 0.1f);
        if ((Physics.Raycast(transform.position, -Vector2.up, 0.3f)) || (Physics.Raycast(transform.position + new Vector3(0f, 0f, 1f), -Vector2.up, 0.3f)) 
            || (Physics.Raycast(transform.position - new Vector3(0f, 0f, 1f), -Vector2.up, 0.3f)) || (Physics.Raycast(transform.position - new Vector3(1f, 0f, 0f), -Vector2.up, 0.3f))
            || (Physics.Raycast(transform.position + new Vector3(1f, 0f, 0f), -Vector2.up, 0.3f))) 
        {
            return true;
        }
        else
        {
            return false;
        }
        // Does a raycast go down and hit the floor?
    }

    public void PlayFallingAudio()
    {
        source.PlayOneShot(fallingAudio);
    }

    public void SetIdle()
    {
        playerAnim.SetInteger("Condition", 0);
    }

    public void SetWalkingForward()
    {
        playerAnim.SetInteger("Condition", 1);
        if(source.isPlaying == false)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    source.PlayOneShot(step1);
                    break;
                case 1:
                    source.PlayOneShot(step2);
                    break;
                case 2:
                    source.PlayOneShot(step3);
                    break;
                default:
                    source.PlayOneShot(step1);
                    break;
            }
        }
    }

 
    public void SetAttacking(bool attack)
    {
        playerAnim.SetBool("Attacking", attack);
    }

    public void SetBlocking(bool block)
    {
        playerAnim.SetBool("IsBlocking", block);
    }

    public bool IsAttacking()
    {
        return (playerAnim.GetBool("Attacking"));
    }
    
    public bool IsWalking()
    {
        if (playerAnim.GetInteger("Condition") == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsIdle()
    {
        if (playerAnim.GetInteger("Condition") == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetIsHit()
    {
        playerAnim.SetBool("IsHit", true);
        StartCoroutine(WaitForHit(0.07f)); // Current knockback length
    }

    IEnumerator WaitForHit(float time)
    {
        yield return new WaitForSeconds(time);
        playerAnim.SetBool("IsHit", false);
    }
}
