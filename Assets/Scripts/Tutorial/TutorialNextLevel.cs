using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNextLevel : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LevelLoader.instance.LoadNextLevel();
        }
    }
}
