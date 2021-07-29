using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public Item item;

    void Awake()
    {
        gameObject.tag = "Item";
    }
}
