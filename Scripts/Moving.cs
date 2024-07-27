using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Moving : MonoBehaviour
{
    public static bool isGrounded;
    public float speed;
    public float jumpForce;
    public float dashForce;
    public float dashTime;
    private bool movement = true;
    private bool run;
    private bool isRight = true, isLeft;
    public Animator anim;
    public Rigidbody2D rb2b;
    bool onGroundCheck = true;

    void Start()
    {
        anim.GetComponent<Animator>();
        rb2b.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Movemnt
        if (movement)
        {
            var move = Input.GetAxis("Horizontal");
            transform.position += new Vector3(move, 0, 0) * speed * Time.deltaTime;
        }
        
        //jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb2b.velocity = Vector3.up * jumpForce;

            anim.SetBool("varJump", true);
            anim.SetBool("varRun", false);

            run = false;
        }

        //Fall
        if (!isGrounded && rb2b.velocity.y < -0.178)
        {
            anim.SetBool("varFall", true);
            anim.SetBool("varJump", false);
            anim.SetBool("varRun", false);

            run = false;
        }

        //Change side
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            isRight = true;
            isLeft = false;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            isLeft = true;
            isRight = false;
        }

        //Dash
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isRight)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
                rb2b.velocity = Vector3.right * dashForce;
                rb2b.constraints = RigidbodyConstraints2D.FreezePositionY;
                movement = false;
                Invoke("stopDash", dashTime);

                anim.SetBool("varDash", true);
            }
            else if (isLeft)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
                rb2b.velocity = Vector3.left * dashForce;
                rb2b.constraints = RigidbodyConstraints2D.FreezePositionY;
                movement = false;
                Invoke("stopDash", dashTime);

                anim.SetBool("varDash", true);
            }
        }

        //Functions
        RunAnimControl();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 9) 
        {
            onGroundCheck = false;
        }

        if (collision.gameObject.layer == 7 && onGroundCheck)
        {
            isGrounded = true;
            run = true;

            anim.SetBool("varJump", false);
            anim.SetBool("varFall", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;

        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 9)
        {
            onGroundCheck = true;
        }
    }

    private void RunAnimControl()
    {
        if (run)
        {
            if ((Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0) && isGrounded)
            {
                anim.SetBool("varRun", true);
            }
            else if (Input.GetAxis("Horizontal") == 0 && isGrounded)
            {
                anim.SetBool("varRun", false);
            }
        }
    }

    private void stopDash()
    {
        rb2b.constraints = RigidbodyConstraints2D.None;
        rb2b.constraints = RigidbodyConstraints2D.FreezeRotation;

        anim.SetBool("varDash", false);

        movement = true;
    }
}
