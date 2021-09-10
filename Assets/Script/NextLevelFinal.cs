using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelFinal : MonoBehaviour
{
    public string levelName;
    public GameObject fadeIn;
    public GameObject Canvas;

    public GameObject hint;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.gameObject.GetComponent<PlayerMove>().finger >= 5)
        {
            collision.gameObject.GetComponent<PlayerMove>().canMove = false;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            var fade = Instantiate(fadeIn, Canvas.transform.position, Canvas.transform.rotation);
            fade.transform.parent = Canvas.transform;
            StartCoroutine("wait1secpls");
        }
        else
        {
            StopCoroutine("showHint");
            StartCoroutine("showHint");
        }
    }


    IEnumerator wait1secpls()
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(levelName);
    }

    IEnumerator showHint()
    {
        hint.SetActive(true);
        yield return new WaitForSeconds(2f);
        hint.SetActive(false);
    }
}
