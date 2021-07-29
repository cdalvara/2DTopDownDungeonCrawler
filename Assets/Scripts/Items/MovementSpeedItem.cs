using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMovementSpeedItem", menuName = "CollectableItem/Item/MovementSpeed")]
public class MovementSpeedItem : Item
{
    public float speed = 0.0f;

    public override float getStats()
    {
        return speed;
    }
}
