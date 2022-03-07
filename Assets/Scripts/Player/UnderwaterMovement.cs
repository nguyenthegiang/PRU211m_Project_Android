using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterMovement : MonoBehaviour
{
    public float speed = 3f;
    [SerializeField]
    GameObject submarineBomb;

    [SerializeField]
    public HeartManager heartManager;

    [SerializeField]
    public Joystick joystick;

    public Vector3 checkPointPassed;

    float _horizontalMove;
    float _verticalMove;
    bool m_FacingRight = false;
    bool hasControl = true;
    Rigidbody2D body;
    Vector3 spawnPosition;
    float objectHeight;
    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        objectHeight = gameObject.GetComponent<Renderer>().bounds.size.y;
        spawnPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - objectHeight, gameObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasControl)
        {
            _horizontalMove = joystick.Horizontal;
            _verticalMove = joystick.Vertical;
            if (_horizontalMove > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (_horizontalMove < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            Vector3 moveVector = new Vector3(_horizontalMove, _verticalMove, 0);
            body.velocity = moveVector.normalized * speed;

            
            spawnPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - objectHeight, gameObject.transform.position.z);

        }

    }

    void FixedUpdate()
    {
       
    }

    public void DropBomb()
    {
        GameObject bomb = Instantiate(submarineBomb, spawnPosition, Quaternion.identity);
        bomb.GetComponent<Rigidbody2D>().AddForce(2f * Vector3.down);
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if touch a Hazard -> die
        if (collision.gameObject.tag == "Hazard")
        {

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
        if (heartManager.health > 0)
        {
            //respawn in checkpoint if still have HP
            CheckpointRespawn();
        }
        else
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
