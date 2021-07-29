using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInInventory
{
    public Item item;
    public int amount;
    public ItemInInventory(Item itemInput, int amountInput)
    {
        item = itemInput;
        amount = amountInput;
    }
}
