using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            //Destroy(transform.parent.gameObject);
            //player.GetComponentsInChildren<PlayerController>();
            player.transform.GetChild(0).gameObject.SetActive(true);
            player.transform.GetChild(2).gameObject.SetActive(false);
        }
    }
}
