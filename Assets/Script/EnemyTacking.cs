using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTacking : MonoBehaviour
{
    private Transform enemyTransform;
    private Transform player;
    public float trackSpeed;
    // Start is called before the first frame update
    private void Start()
    {
        enemyTransform = this.GetComponent<Transform>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
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
