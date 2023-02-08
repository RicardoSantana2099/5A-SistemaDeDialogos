using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Movimiento
    public float moveSpeed;

    //Salto
    private bool canDoubleJump;
    public float jumpForce;

    //Componentes
    public Rigidbody2D rgb;

    //Animaciones+
    private Animator anim;
    private SpriteRenderer theSR;

    //Piso
    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    void Start()
    {
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        rgb.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), rgb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);
       
        if(isGrounded)
        {
            canDoubleJump = true;
        }

        if(Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rgb.velocity = new Vector2(rgb.velocity.x, jumpForce);

            }
            else
            {
                if(canDoubleJump)
                {
                    rgb.velocity = new Vector2(rgb.velocity.x, jumpForce);
                    canDoubleJump = false;
                }
            }

        }
        if(rgb.velocity.x < 0)
        {
            theSR.flipX = true;
        }
        else 
            if(rgb.velocity.x > 0)
        {
            theSR.flipX = false;
        }
        anim.SetFloat("moveSpeed", Mathf.Abs(rgb.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
    }
}
