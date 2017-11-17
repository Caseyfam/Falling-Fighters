using UnityEngine;
using System.Collections;

public class PlayerRigidBodyMovement : MonoBehaviour {

    float playerSpeed;
    float playerRotationSpeed;
    float gravity;

    private int playerID;

    GlobalVariables globalVars;
    Rigidbody playerRigidBody;
    PlayerAnimator playerAnim;
    Animator animator;

    private float joystickAngle;
    private float currentAngle;
    private float targetAngle;
    private float rotateTarget;

    private float rotationX, rotationY;
    private float controllerDistance;

    private bool playerCanMove;
    private bool playerHasMoved = false;

    private Vector3 moveDirection = Vector3.zero;


	// Use this for initialization
	void Start ()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        globalVars = GameObject.Find("GameLogic").GetComponent<GlobalVariables>();
        playerAnim = GetComponentInChildren<PlayerAnimator>();
        animator = GetComponentInChildren<Animator>();

        playerSpeed = globalVars.GetGlobalPlayerSpeed();
        playerRotationSpeed = globalVars.GetGlobalPlayerRotationSpeed();
        gravity = globalVars.GetGlobalGravity();

	}
	
	// Update is called once per frame
	void Update ()
    {
        playerAnim = GetComponentInChildren<PlayerAnimator>();
        animator = GetComponentInChildren<Animator>();
        playerCanMove = globalVars.GetPlayersCanMove();
        
        currentAngle = playerRigidBody.transform.eulerAngles.y;

        joystickAngle = Mathf.Atan2(rotationX, rotationY) * Mathf.Rad2Deg;

        targetAngle = Mathf.LerpAngle(currentAngle, joystickAngle, Time.deltaTime * playerRotationSpeed);
        //print("Swipe X: " + swipeRotationX);
        //print("Swipe Y: " + swipeRotationY);

        if (playerHasMoved)
        {
            transform.eulerAngles = new Vector3(playerRigidBody.transform.eulerAngles.x, targetAngle, playerRigidBody.transform.eulerAngles.z);
        }
        

        if (playerCanMove)
        {
            //moveDirection.y = -gravity * Time.deltaTime;
            //playerRigidBody.velocity = moveDirection;
            Vector3 thisVelocity = playerRigidBody.velocity;
            thisVelocity.x = moveDirection.x;
            thisVelocity.z = moveDirection.z;
            playerRigidBody.velocity = thisVelocity;
        }
        
     
        // ANIMATION TESTING CENTER. EXPECT THE UNEXPECTED. DANGER BELOW.
        if (playerRigidBody.velocity.x != 0 || playerRigidBody.velocity.z != 0)
        {
            playerAnim.SetWalkingForward();
        }
        if (playerRigidBody.velocity.x == 0 && playerRigidBody.velocity.z == 0)
        {
            playerAnim.SetIdle();
        }
        

        controllerDistance = Mathf.Sqrt(Mathf.Pow(rotationX, 2) + Mathf.Pow(rotationY, 2)); // Finding distance of joystick from offset
        // Statement below changes walk speed based on how far the joystick is offset from center
        if (playerAnim.IsWalking() && !playerAnim.IsAttacking())
        {
            animator.speed = (1 + (1 / 2)) * controllerDistance;
        }
        // 1.5 is the current animator speed. This can be changed / adjusted
        else
        {
            animator.speed = (1 + (1 / 2));
        }
    }

    public void SetPlayerID(int playerID)
    {
        this.playerID = playerID;
    }

    public void MovePlayer(float x, float y)
    {
        playerHasMoved = true;
        rotationX = x;
        rotationY = y;

        if (playerCanMove)
        {
            moveDirection = new Vector3(x, -gravity * Time.deltaTime, y);
            moveDirection *= playerSpeed;
        }

    }

    public void SetPlayerSpeed (int newSpeed)
    {
        playerSpeed = newSpeed;
    }

    public void SetPlayerRotationSpeed(int newSpeed)
    {
        playerRotationSpeed = newSpeed;
    }
}
