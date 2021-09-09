using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherDetection : MonoBehaviour
{
    [Header("Components")] [SerializeField] bool detected;
    public bool isFalling;
    public bool goUp;
    public GameObject enemy;
    public Transform startPos;
    public float downSpeed;
    public float upSpeed;
    public float gravity;

    public BoxCollider2D bc;
    public float extraHeightBelow;
    public LayerMask ground;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos.gameObject.transform.parent = null;
    }

    // Update is called once per frame
    private void Update()
    {
        if(detected)
        {
            if(!isFalling && !goUp)
            {
                enemy.GetComponent<Rigidbody2D>().gravityScale = gravity;
                isFalling = true;
                enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, downSpeed);
            }

            
        }

        if(IsGrounded())
        {
            isFalling = false;
            goUp = true;
        }

        if(goUp)
        {
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, startPos.position, upSpeed * Time.deltaTime);
            enemy.GetComponent<Rigidbody2D>().gravityScale = 0f;
        }

        if(Vector2.Distance(enemy.transform.position, startPos.position) < 0.001f)
        {
            isFalling = false;
            goUp = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            detected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            detected = false;
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
