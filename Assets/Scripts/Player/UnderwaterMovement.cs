using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterMovement : PlayerMovement
{
    public float speed = 3f;
    [SerializeField]
    GameObject submarineBomb;

    float _horizontalMove;
    float _verticalMove;
    bool m_FacingRight = false;
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
        _horizontalMove = Input.GetAxis("Horizontal");
        _verticalMove = Input.GetAxis("Vertical");
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bomb = Instantiate(submarineBomb, spawnPosition, Quaternion.identity);
            bomb.GetComponent<Rigidbody2D>().AddForce(2f * Vector3.down);
        }
        spawnPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - objectHeight, gameObject.transform.position.z);

    }

    void FixedUpdate()
    {
        
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
}
