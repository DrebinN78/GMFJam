using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    //Position of the Game Object
    public Transform pos1;
    public Transform pos2;

    //Actual position (pos 1 or pos2)
    public Transform actualPos;

    //position of the enemy
    private Transform enemyPos;

    //speed of the enemy
    public float enemySpeed;

    public float deathRayRange;

    public GameObject bullet;

    public float timeBtwShot;
    float actualTimeBtwShot;

    // Start is called before the first frame update
    private void Start()
    {
        enemyPos = this.GetComponent<Transform>();
        Physics2D.queriesStartInColliders = false;
    }

    // Update is called once per frame
    private void Update()
    {
        //change the actual position if the enemy position and the pos1 or pos2 position is the same
        if(enemyPos.position == pos1.position)
        {
            actualPos = pos1;
        }
        else if(enemyPos.position == pos2.position)
        {
            actualPos = pos2;
        }

        //if the actual position is the same with pos1 or pos2, move towards the other pos
        if(actualPos == pos1)
        {
            enemyPos.position = Vector3.MoveTowards(enemyPos.position, pos2.position, enemySpeed * Time.deltaTime);
            enemyPos.localScale = new Vector3(1, 1, 1);
        }
        else if(actualPos == pos2)
        {
            enemyPos.position = Vector3.MoveTowards(enemyPos.position, pos1.position, enemySpeed * Time.deltaTime);
            enemyPos.localScale = new Vector3(1, -1, 1);
        }

        DeathRay();
    }

    private void DeathRay()
    {
        RaycastHit2D deathRay = Physics2D.Raycast(enemyPos.position, -transform.up, deathRayRange);
        if(deathRay.collider != null)
        {
            Debug.DrawLine(transform.position, deathRay.point);
            if(deathRay.collider.CompareTag("Player"))
            {
                if(actualTimeBtwShot <= 0)
                {
                Debug.Log("mort");
                var bul = Instantiate(bullet, transform.position, transform.rotation);
                bul.GetComponent<Bullet>().dir = -transform.up;
                    actualTimeBtwShot = timeBtwShot;

                }
            }
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + -transform.up * deathRayRange);
        }

        actualTimeBtwShot -= Time.deltaTime;

    }
}
