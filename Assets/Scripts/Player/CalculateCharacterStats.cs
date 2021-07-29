using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateCharacterStats : MonoBehaviour
{
    //[SerializeField] private GameObject playerCharacter;
    [SerializeField] private GameObject projectile;

    private int baseDamage;
    private float baseMovementSpeed;

    private int currentDamage;
    private float currentMovementSpeed;

    void Awake()
    {
        baseDamage = projectile.GetComponent<Projectile>().getDamage();
        currentDamage = baseDamage;
        projectile.GetComponent<Projectile>().setDamage(currentDamage);
        baseMovementSpeed = gameObject.GetComponent<PlayerController>().getMovementSpeed();
        currentMovementSpeed = baseMovementSpeed;
        gameObject.GetComponent<PlayerController>().setMovementSpeed(currentMovementSpeed);
    }

    public void calcAll()
    {
        calcMovementSpeed();
        calcDamage();
    }

    public void calcDamage()
    {
        CharacterInventory inventory = gameObject.GetComponent<PlayerController>().getInventory();
        int invAmount = inventory.GetItemAmount("IncreaseDamage");
        if(invAmount != 0)
        {
            currentDamage = baseDamage + (int)inventory.GetItem("IncreaseDamage").item.getStats() * invAmount;
        }
        else
        {
            currentDamage = baseDamage;
        }
        projectile.GetComponent<Projectile>().setDamage(currentDamage);
    }

    public void calcMovementSpeed()
    {
        CharacterInventory inventory = gameObject.GetComponent<PlayerController>().getInventory();
        int invAmount = inventory.GetItemAmount("MovementUp");
        if(invAmount != 0)
        {
            currentMovementSpeed = baseMovementSpeed + inventory.GetItem("MovementUp").item.getStats() * invAmount;
        }
        else
        {
            currentMovementSpeed = baseMovementSpeed;
        }
        gameObject.GetComponent<PlayerController>().setMovementSpeed(currentMovementSpeed);
    }
}
