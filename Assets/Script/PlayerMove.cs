using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    //public Player playerControl;


    [Header("Movements")]
    public bool canMove;
    public float speed;
    public bool isLeft;


    [Header("Jump")]
    public float jumpForce;
    public float extraHeightBelow;
    public LayerMask ground;


    [Header("WallJump")]
    public LayerMask wall;
    public bool fromWallJump;
    public float extraHeightFace;
    float actualTimeRecov;
    public float timeRecov;
    public float speedMulti;


    [Header("Attack")]
    public float attackRecov;
    float actualAttackRecov;

    public GameObject attack;
    public Transform attackPoint;



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        canMove = true;
        //playerControl = new Player();
    }

    /*private void OnEnable()
    {
        playerControl.Enable();
    }

    private void OnDisable()
    {
        playerControl.Disable();
    }*/




    void Update()
    {
        Move();
        Jump();
        IsGrounded();
        WallJumpRecov();
        Attack();

    }

    // Movement ------------------------------------------------------------------------------------------------------------------------------------

    public void Move()
    {
        if (canMove)
        {
            if (fromWallJump)
            {
                if (Input.GetKey("q"))
                {
                    rb.velocity = new Vector2(-speed * speedMulti, rb.velocity.y);
                    isLeft = true;
                    transform.eulerAngles = new Vector3(0, 180, 0);

                }
                else if (Input.GetKey("d"))
                {
                    rb.velocity = new Vector2(speed * speedMulti, rb.velocity.y);
                    isLeft = false;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                
            }
            else
            {
                if (Input.GetKey("q"))
                {
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                    isLeft = true;
                    transform.eulerAngles = new Vector3(0, 180, 0);

                }
                else if (Input.GetKey("d"))
                {
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                    isLeft = false;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
            }
            
            
        }
    }



    public void Jump()
    {
        if (Input.GetKeyDown("space"))
        {
            if (IsGrounded() == true)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            else if (IsWalled() == true)
            {

                if (isLeft)
                {
                    rb.velocity = new Vector2(speed * speedMulti, jumpForce);
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    isLeft = false;
                }
                else
                {
                    rb.velocity = new Vector2(-speed * speedMulti, jumpForce);
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    isLeft = true;
                }
                
                canMove = false;
                actualTimeRecov = timeRecov;
                fromWallJump = true;
            }
        }
    }

    public void WallJumpRecov()
    {
        if(actualTimeRecov <= 0)
        {
            canMove = true;
        }
        else
        {
            actualTimeRecov -= Time.deltaTime;
        }
    }


    public void Attack()
    {
        if (Input.GetKeyDown("return"))
        {
            if (actualAttackRecov <= 0)
            {
                var att = Instantiate(attack, attackPoint.position, attackPoint.rotation);
                att.transform.parent = gameObject.transform;
                actualAttackRecov = attackRecov;
            }

        }
                actualAttackRecov -= Time.deltaTime;
    }



    // Collisions ----------------------------------------------------------------------------------------------------------------------------------

    public bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, extraHeightBelow, ground);

        if (raycastHit.collider != null)
        {
            fromWallJump = false;
            canMove = true;
            
            return raycastHit.collider != null;
        }

        /*Debug.DrawRay(bc.bounds.center + new Vector3(bc.bounds.extents.x, 0), Vector2.down * (bc.bounds.extents.y + extraHeightBelow));
        Debug.DrawRay(bc.bounds.center - new Vector3(bc.bounds.extents.x, 0), Vector2.down * (bc.bounds.extents.y + extraHeightBelow));
        Debug.DrawRay(bc.bounds.center - new Vector3(bc.bounds.extents.x, bc.bounds.extents.y + extraHeightBelow), Vector2.right * (bc.bounds.extents.x));*/


        return false;

    }


    public bool IsWalled()
    {
        if (isLeft)
        {
            RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, -Vector2.right, extraHeightFace, wall);

            if (raycastHit.collider != null)
            {
                return raycastHit.collider != null;
            }


            return false;

            /*Debug.DrawRay(pS.boxCollider2D.bounds.center + new Vector3(0, pS.boxCollider2D.bounds.extents.y), -Vector2.right * (pS.boxCollider2D.bounds.extents.x + extraHeightFace));
            Debug.DrawRay(pS.boxCollider2D.bounds.center - new Vector3(0, pS.boxCollider2D.bounds.extents.y), -Vector2.right * (pS.boxCollider2D.bounds.extents.x + extraHeightFace));
            Debug.DrawRay(pS.boxCollider2D.bounds.center + new Vector3(-pS.boxCollider2D.bounds.extents.x - extraHeightFace, pS.boxCollider2D.bounds.extents.y), Vector2.down * (pS.boxCollider2D.bounds.extents.y));
            Debug.Log(raycastHit.collider);*/

        }
        else
        {
            RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.right, extraHeightFace, wall);

            if (raycastHit.collider != null)
            {
                return raycastHit.collider != null;
            }


            return false;

            /*Debug.DrawRay(pS.boxCollider2D.bounds.center + new Vector3(0, pS.boxCollider2D.bounds.extents.y), Vector2.right * (pS.boxCollider2D.bounds.extents.x + extraHeightFace));
            Debug.DrawRay(pS.boxCollider2D.bounds.center - new Vector3(0, pS.boxCollider2D.bounds.extents.y), Vector2.right * (pS.boxCollider2D.bounds.extents.x + extraHeightFace));
            Debug.DrawRay(pS.boxCollider2D.bounds.center + new Vector3(pS.boxCollider2D.bounds.extents.x + extraHeightFace, pS.boxCollider2D.bounds.extents.y), Vector2.down * (pS.boxCollider2D.bounds.extents.y));
            Debug.Log(raycastHit.collider);*/

        }
    }
}
