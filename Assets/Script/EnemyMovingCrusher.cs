using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingCrusher : MonoBehaviour
{
    public bool isFalling;
    public bool goUp;
    public bool needToSwitch;
    public GameObject enemy;
    public Transform actualPos;
    public float downSpeed;
    public float upSpeed;
    public float posSpeed;
    public float gravity;

    public BoxCollider2D bc;
    public float extraHeightBelow;
    public LayerMask ground;

    public float waitBeforeSwitch;
    public float maxWaitBeforeSwitch;
    public Transform pos1;
    public Transform pos2;
    
    // Start is called before the first frame update
    void Start()
    {
        actualPos = pos1;
        waitBeforeSwitch = maxWaitBeforeSwitch;
    }

    // Update is called once per frame
    private void Update()
    {
        //When crusher didn't already crush
        if(!needToSwitch)
        {
            if(!isFalling && !goUp)
            {
                enemy.GetComponent<Rigidbody2D>().gravityScale = gravity;
                isFalling = true;
                enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, downSpeed);
            }

            if(IsGrounded())
            {
                isFalling = false;
                goUp = true;
            }

            if(goUp)
            {
                enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, actualPos.position, upSpeed * Time.deltaTime);
                enemy.GetComponent<Rigidbody2D>().gravityScale = 0f;
            }

        }
        //when crusher already crush
        else
        {
            waitBeforeSwitch -= Time.deltaTime;
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, actualPos.position, posSpeed * Time.deltaTime);
        }

        //when crusher has crushed and need to be reset
        if(Vector2.Distance(enemy.transform.position, actualPos.position) < 0.001f && goUp)
        {
            isFalling = false;
            goUp = false;
            needToSwitch = true;
            if(actualPos == pos1)
            {
                actualPos = pos2;
            }
            else if(actualPos == pos2)
            {
                actualPos = pos1;
            }

        }
        //when crusher reach the other position
        else if(enemy.transform.position == actualPos.position && waitBeforeSwitch <= 0f)
        {
            waitBeforeSwitch = maxWaitBeforeSwitch;
            needToSwitch = false;
        }
        
    }

    public bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, extraHeightBelow, ground);

        if (raycastHit.collider != null)
        {
            

            return raycastHit.collider != null;
        }

        // Debug.DrawRay(bc.bounds.center + new Vector3(bc.bounds.extents.x, 0), Vector2.down * (bc.bounds.extents.y + extraHeightBelow));
        // Debug.DrawRay(bc.bounds.center - new Vector3(bc.bounds.extents.x, 0), Vector2.down * (bc.bounds.extents.y + extraHeightBelow));
        // Debug.DrawRay(bc.bounds.center - new Vector3(bc.bounds.extents.x, bc.bounds.extents.y + extraHeightBelow), Vector2.right * (bc.bounds.extents.x));


        return false;

    }
}
