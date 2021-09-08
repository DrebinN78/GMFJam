using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTacking : MonoBehaviour
{
    private Transform enemyTransform;
    private Transform player;
    public float trackSpeed;

    public int maxLife;
    public int life; 
    // Start is called before the first frame update
    private void Start()
    {
        enemyTransform = this.GetComponent<Transform>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        life = maxLife;
    }

    private void Update()
    {
        if(life <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("PlayerHitBox"))
        {
            life -= 1;
        }
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        //when hitbox trigger with player, disable enemy patrol and rush against player
        if(collider.CompareTag("Player"))
        {
            this.GetComponent<EnemyPatrol>().enabled = false;
            enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, player.position, trackSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        //when player escape trigger, re-enable enemy patrol
        if(collider.CompareTag("Player"))
        {
            this.GetComponent<EnemyPatrol>().enabled = true;
        }
    }
}
