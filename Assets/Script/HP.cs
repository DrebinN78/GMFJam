using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public float stunTime;
    public float stunTimeCounter;

    public bool isInvulnerable;

    public bool isPlayer;
    SpriteRenderer sr;


    void Start()
    {
        currentHealth = maxHealth;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (stunTimeCounter >= 0)
        {
            stunTimeCounter -= Time.deltaTime;
            sr.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0.2f), Mathf.PingPong(Time.time * 10, 1));
            isInvulnerable = true;
        }
        else
        {
            sr.color = Color.white;
            isInvulnerable = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerHitBox")
        {
            if (!isPlayer)
            {
                if (!isInvulnerable)
                {
                    currentHealth -= 1;

                    stunTimeCounter = stunTime;
                }
            }
        }else if (collision.tag == "Enemy")
        {
            if (isPlayer){
                currentHealth -= 1;

                stunTimeCounter = stunTime;
            }
        }
    }
}
