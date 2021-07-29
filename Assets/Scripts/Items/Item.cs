using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public int amount;

    public GameObject itemPrefab;
    public Sprite itemSprite;

    public abstract float getStats();
}
