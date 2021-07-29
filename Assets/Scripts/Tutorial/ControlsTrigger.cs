using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsTrigger : MonoBehaviour
{

    GameObject player;
    GameObject Canvas;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Canvas = GameObject.FindGameObjectWithTag("UI");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Canvas.transform.GetChild(2).gameObject.SetActive(false);
            gameObject.SetActive(false);
            //StartCoroutine(Fade());
        }
    }

    public IEnumerator Fade()
    {
        yield return new WaitForSeconds(3f);
        Canvas.transform.GetChild(5).gameObject.SetActive(false);
    }
}



