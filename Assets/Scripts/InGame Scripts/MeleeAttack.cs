using UnityEngine;
using System.Collections;

//The best way to test this script is to place the character's cirlce thing (I don't know what to call it) on the enemy player 
//and then press the F key. They will then be knocked back. Also, I added a another Box Collider to Player2 so the distance between the circle 
//and enemy player doesn't have to be too precise. If this is something that works, we can add more Box Colliders to all the players so they 
//all have the same hit box. 

public class MeleeAttack : MonoBehaviour
{
    public int force = 3000;
    GameObject storedOther;
    bool isHit = false;
    bool isInRange = false;
    bool applyAnimOnce = false;
    PlayerAnimator playerAnim;

    public AudioClip swipe1Audio, swipe2Audio, swipe3Audio, punch1, punch2, punch3;
    private AudioSource source;
    private bool audioCanPlay = true;

    bool isBlocking = false;
    bool startOnce = true;
    bool punchOnce = true;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        playerAnim = transform.parent.GetComponentInParent<PlayerAnimator>();
        if (isHit == true)
        {
			try
			{
                //Gets blocking status from storedOther
                isBlocking = storedOther.GetComponentInChildren<Block>().getIsBlocking();

                if (isBlocking == false)
                {
                    if (punchOnce)
                    {
                        StartCoroutine(PlayOnceAudio(0.5f));
                    }
                    storedOther.GetComponent<Rigidbody>().AddForce(transform.forward * force);
                }
                //(1/6) of the "hit" force when opponent is blocking
                else if (isBlocking)
                {
                    storedOther.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
                    if (punchOnce)
                    {
                        StartCoroutine(PlayOnceAudio(0.5f));
                    }
                }
            }
			catch
			{
				// This is try caught since the "other" player could die
				// after being pushed off, resulting in an error from Unity later
			}
            if (applyAnimOnce == false)
            {
                if (storedOther != null)
                {
                    storedOther.GetComponentInChildren<PlayerAnimator>().SetIsHit();
                }
                applyAnimOnce = true;
            }
            if (startOnce)
            {
                StartCoroutine(StopCharacter(0.07f)); // Here is where you enter how long the
                                                      // attack should last.
                startOnce = false;
            }
            
        }
    }
    IEnumerator PlayOnceAudio(float time)
    {
        source.PlayOneShot(punch1);
        punchOnce = false;
        yield return new WaitForSeconds(time);
        punchOnce = true;
    }

    public void Attack()
    {
        if (audioCanPlay && !playerAnim.IsAttacking())
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    source.PlayOneShot(swipe1Audio);
                    break;
                case 1:
                    source.PlayOneShot(swipe2Audio);
                    break;
                case 2:
                    source.PlayOneShot(swipe3Audio);
                    break;
                default:
                    source.PlayOneShot(swipe1Audio);
                    break;
            }
            StartCoroutine(AudioPause(0.5f));
        }
        
        playerAnim.SetAttacking(true);
        if (isInRange)
        {
            isHit = true;
        }

    }
   

    void OnTriggerEnter (Collider other)
    {
        isInRange = true;
        if (other.gameObject.tag == "Player")
        {
            storedOther = other.gameObject;
        }
    }

    void OnTriggerStay (Collider other) // EXPERIMENTAL, IF ATTACKING IS BROKEN THIS MIGHT HAVE DONE IT
    {
        isInRange = true;
        if (other.gameObject.tag == "Player")
        {
            storedOther = other.gameObject;
        }
    }

    // On Trigger Stay

    // Hash tables to remove duplicates

    void OnTriggerExit(Collider other)
    {
        isInRange = false;
        try
        {
            playerAnim.SetAttacking(false);
        }
        catch
        {

        }
        
    }


    IEnumerator StopCharacter(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
		isHit = false;
        storedOther = null;
        applyAnimOnce = false;
        startOnce = true;
        //storedOther.GetComponent<Rigidbody>().velocity = Vector3.zero;

        //forceApplied = false;
    }

    IEnumerator AudioPause(float waitTime)
    {
        audioCanPlay = false;
        yield return new WaitForSeconds(waitTime);
        audioCanPlay = true;
    }

}
