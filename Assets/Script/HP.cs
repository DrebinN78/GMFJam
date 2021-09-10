using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HP : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public float stunTime;
    float stunTimeCounter;

    public bool isInvulnerable;

    public bool isPlayer;
    SpriteRenderer sr;

    public GameObject fadeIn;
    public GameObject Canvas;
    public GameObject leTrucADestroy;


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

        if(currentHealth <= 0)
        {
            if (isPlayer)
            {
                GetComponent<PlayerMove>().canMove = false;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                var fade = Instantiate(fadeIn, Canvas.transform.position, Canvas.transform.rotation);
                fade.transform.parent = Canvas.transform;
                StartCoroutine("wait1secpls");
            }
            else
            Destroy(leTrucADestroy);
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

                AudioManger.Instance.Play("GetDamaged");

                
            }
        }
    }

    IEnumerator wait1secpls()
    {
        yield return new WaitForSeconds(1.2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
