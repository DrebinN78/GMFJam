using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrusher : MonoBehaviour
{
    private Animator crusherAnim;

    // Start is called before the first frame update
    private void Start()
    {
        crusherAnim = this.GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        //When player trigger the zone of the crusher,
        //Activate Attack Animation
        if(collider.CompareTag("Player"))
        {
            crusherAnim.SetTrigger("Attack");
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        crusherAnim.ResetTrigger("Attack");
    }
}
