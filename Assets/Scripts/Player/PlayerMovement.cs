using System.Collections;
using UnityEngine;

//Movement & actions of MainCharacter
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public CharacterController2D controller;
    [SerializeField]
    public float runSpeed = 30f;
    [SerializeField]
    public Animator animator;
    [SerializeField]
    public HeartManager heartManager;
    float horizontalMove = 0f;
    bool isJumping = false;

    // check if player have control of main character
    bool hasControl = true;
    Timer timer;

    //the checkpoint at which the Character will respawn
    public Vector3 checkPointPassed;

    //for androidMovement
    private bool moveLeft;
    private bool moveRight;
    private bool moveJump;

    // Start is called before the first frame update
    void Start()
    {
        //init heart manager
        heartManager = gameObject.GetComponent<HeartManager>();
        timer = gameObject.AddComponent<Timer>();
    }

    //For Android
    public void MoveMeLeft()
    {
        moveLeft = true;
    }

    public void StopMeLeft()
    {
        moveLeft = false;
    }

    public void MoveMeRight()
    {
        moveRight = true;
    }

    public void StopMeRight()
    {
        moveRight = false;
    }

    public void JumpMe()
    {
        moveJump = true;
    }

    public void StopJumpMe()
    {
        moveJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasControl)
        {
            //For android
            if (moveLeft && !moveRight)
            {
                horizontalMove = -1 * runSpeed;
            } else if (moveRight && !moveLeft)
            {
                horizontalMove = 1 * runSpeed;
            } else
            {
                horizontalMove = 0;
            }

            animator.SetFloat("speed", Mathf.Abs(horizontalMove));

            if (moveJump)
            {
                isJumping = true;
                animator.SetBool("isJumping", true);
            }
            else
            {
                isJumping = false;
                animator.SetBool("isJumping", false);
            }
        }        
    }

    void FixedUpdate()
    {
        if (hasControl)
        {
            controller.Move(horizontalMove * Time.deltaTime, isJumping);
            isJumping = false;
        }
        
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if touch a Hazard -> die
        if (collision.gameObject.tag == "Hazard")
        {
            
            animator.SetBool("dead", true);
            
            StartCoroutine(waiter());
        }
    }

    //Use for Delay in Death animation
    IEnumerator waiter()
    {
        // disable player control
        hasControl = false;

        //stop all movement on main character
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        yield return new WaitForSeconds(0.75f);
        animator.SetBool("dead", false);
        if (heartManager.health > 0)
        {
            //respawn in checkpoint if still have HP
            CheckpointRespawn();
        } else
        {
            //go to Game Over Scene
            SceneSwitcher.goToGameOverScene();
        }

        hasControl = true;
    }

    //respawn mainCharacter at checkPoint (when still have hearts left)
    void CheckpointRespawn()
    {
        //respawn
        transform.position = new Vector3(checkPointPassed.x, checkPointPassed.y, 0);
        //minus HP
        heartManager.MinusHeart();
    }
}
