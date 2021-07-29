using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHealthItem", menuName = "CollectableItem/Item/Health")]
public class HealthItem : Item
{
    public float health = 0.0f;

    public override float getStats()
    {
        return health;
    }
}
