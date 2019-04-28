using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages:
/// 1. the player movement and flipping
/// 2. the player animation
/// </summary>
public class PlayerCtrl : MonoBehaviour
{
    //1. declare public bool isGrounded, Transform feet, float feetRadius, layerMask whatIsGround
    //2. show Physics2D.OverlapCircle() method to check if player is grounded
    //3. then show preferred way for this cat by using Physics2D.OverlapBox

    [Tooltip("this is a positive integer which speed up the player movement")]
    public int speedBoost;  // set this to 5
    public float jumpSpeed; // set this to 600

    public Transform leftBulletSpawnPos, rightBulletSpawnPos;
    public GameObject leftBullet, rightBullet;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    int jumptimes;
    bool movePressed;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {

       
        float playerSpeed = Input.GetAxisRaw("Horizontal"); // value will be 1, -1 or 0
        playerSpeed *= speedBoost;
        if(playerSpeed!=0)
            MoveHorizontal(playerSpeed);
       else if(playerSpeed==0)
            StopMoving();


        if (jumptimes<2&&Input.GetButtonDown("Jump"))
            Jump();

        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }

        ShowFalling();
    }


	public void MoveHorizontal(float playerSpeed)
    {
     
        
        Debug.Log("call");
        rb.velocity = new Vector2(playerSpeed, rb.velocity.y);

        if (playerSpeed < 0)
            sr.flipX = true;
        else if(playerSpeed > 0)
            sr.flipX = false;

        if(jumptimes==0)
            anim.SetInteger("State", 1);
    }

   public void StopMoving()
    {
        movePressed = false;
        rb.velocity = new Vector2(0, rb.velocity.y);

        if(jumptimes==0)
            anim.SetInteger("State", 0);
    }

   public void ShowFalling()
    {
        if(rb.velocity.y < 0)
        {
            anim.SetInteger("State", 3);
        }
    }

    public void Jump()
    {

        jumptimes++;
        rb.AddForce(new Vector2(0, jumpSpeed)); // simply make the player jump in the y axis or upwards
        anim.SetInteger("State", 2);

    }

    public void Fire()
    {
        // makes the player fire bullets in the left direction
        if(sr.flipX)
        {
            Instantiate(leftBullet, leftBulletSpawnPos.position, Quaternion.identity);
        }

        // makes the player fire bullets in the right direction
        if(!sr.flipX)
        {
            Instantiate(rightBullet, rightBulletSpawnPos.position, Quaternion.identity);
        }
    }



	void OnCollisionEnter2D(Collision2D other)
	{
        if (other.gameObject.CompareTag("Ground"))
            jumptimes = 0;
	}
}
