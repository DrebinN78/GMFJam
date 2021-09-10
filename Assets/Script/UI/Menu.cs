using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public GameObject fadeIn;
    public GameObject Canvas;

    public bool ending;


    private void Start()
    {
        if (ending)
        {
            StartCoroutine("clafin");
        }    
    }

    public void StartGame()
    {
        var fade = Instantiate(fadeIn, Canvas.transform.position, Canvas.transform.rotation);
        fade.transform.parent = Canvas.transform;
        StartCoroutine("wait1secpls");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    


    IEnumerator wait1secpls()
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadSceneAsync(1);
    }

    IEnumerator clafin()
    {

            yield return new WaitForSeconds(4.5f);
            var fade = Instantiate(fadeIn, Canvas.transform.position, Canvas.transform.rotation);
            fade.transform.parent = Canvas.transform;
            yield return new WaitForSeconds(1.2f);
            SceneManager.LoadSceneAsync(0);
    }
}
