using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDamageItem", menuName = "CollectableItem/Item/Damage")]
public class DamageItem : Item
{
    public float damage = 0.0f;

    public override float getStats()
    {
        return damage;
    }
}
