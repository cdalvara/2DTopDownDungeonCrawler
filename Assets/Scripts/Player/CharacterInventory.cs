using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory
{
    private List<ItemInInventory> itemList;
    private Inventory inventory;

    private int maxInventorySpace = 10;
    private int remainingInventorySpace;

    public CharacterInventory()
    {
        itemList = new List<ItemInInventory>();
        remainingInventorySpace = maxInventorySpace;
        Debug.Log("Inv works");
    }

    public void setInv(Inventory inventory)
    {
        this.inventory = inventory;
    }

    public bool isFull()
    {
        if(remainingInventorySpace > 0)
        {
            return false;
        }
        else
        {
            Debug.Log("Inventory is full");
            return true;
        }
        //return remainingInventorySpace <= 0;
    }

    public ItemInInventory GetItem(string name)
    {
        foreach(ItemInInventory itemInInventory in itemList)
        {
            if(itemInInventory.item.itemName == name)
                return itemInInventory;
        }
        return null;
    }

    public int GetItemAmount(string name)
    {
        foreach(ItemInInventory itemInInventory in itemList)
        {
            if(itemInInventory.item.itemName == name)
                return itemInInventory.amount;
        }
        return 0;
    }

    public void dropItem(Item item, GameObject characterObj)
    {
        if(item != null)
        {
            GameObject gO = GameObject.Instantiate(item.itemPrefab, characterObj.transform.position, item.itemPrefab.transform.rotation);
            characterObj.GetComponent<PlayerController>().StartCoroutine(characterObj.GetComponent<PlayerController>().waiting(gO));
            characterObj.GetComponent<CalculateCharacterStats>().calcAll();
        }
    }

    public void dropItem(string itemSearchName, GameObject characterObj)
    {
        ItemInInventory itemInInventory = GetItem(itemSearchName);
        if(itemInInventory != null)
        {
            Item item = itemInInventory.item;
            GameObject gO = GameObject.Instantiate(item.itemPrefab, characterObj.transform.position, item.itemPrefab.transform.rotation);
            characterObj.GetComponent<PlayerController>().StartCoroutine(characterObj.GetComponent<PlayerController>().waiting(gO));
            characterObj.GetComponent<CalculateCharacterStats>().calcAll();
        }
        
    }

    public bool addItem(Item item)
    {
        ItemInInventory temp = GetItem(item.itemName);
        if(temp != null)
        {
            temp.amount += item.amount;
            Debug.Log(item.amount + " more " + item.itemName + " was added to the players inventory.");
            remainingInventorySpace -= 1;
            Debug.Log("Inventory space remaining: " + (remainingInventorySpace));
            return true;
        }
        else
        {
            itemList.Add(new ItemInInventory(item, item.amount));
            Debug.Log("New item " + item.itemName + " was added to player inventory.");
            remainingInventorySpace -= 1;
            Debug.Log("Inventory space remaining: " + (remainingInventorySpace));
            return true;
        }
        return false;
    }

    public bool removeItem(string itemSearchName, GameObject characterObj)
    {
        ItemInInventory temp = GetItem(itemSearchName);
        if(temp != null)
        {
            if(temp.amount > 1)
            {
                temp.amount -= 1;
                Debug.Log(temp.amount + " " + temp.item.itemName + " was removed from the players inventory.");
            }
            else
            {
                temp.amount = 0;
                itemList.Remove(temp);
                Debug.Log(temp.item.itemName + " was removed from player inventory.");
            }
            remainingInventorySpace += 1;
            Debug.Log("Inventory space remaining: " + (remainingInventorySpace));
            dropItem(temp.item, characterObj);
            inventory.refreshInventory();
            return true;
        }
        else
        {
            Debug.Log("No item called " + itemSearchName + " found.");
        }
        return false;
    }

    public bool removeItem(string itemSearchName)
    {
        ItemInInventory temp = GetItem(itemSearchName);
        if(temp != null)
        {
            if(temp.amount > 1)
            {
                temp.amount -= 1;
                Debug.Log(temp.amount + " " + temp.item.itemName + " was removed from the players inventory.");
            }
            else
            {
                temp.amount = 0;
                itemList.Remove(temp);
                Debug.Log(temp.item.itemName + " was removed from player inventory.");
            }
            return true;
        }
        else
        {
            Debug.Log("No item called " + itemSearchName + " found.");
        }
        return false;
    }

    public bool removeItem(ItemInInventory itemInInventory)
    {
        ItemInInventory temp = itemInInventory;
        if(temp != null)
        {
            if(temp.amount > 1)
            {
                temp.amount -= 1;
                Debug.Log(itemInInventory.amount + " " + itemInInventory.item.itemName + " was removed from the players inventory.");
            }
            else
            {
                temp.amount = 0;
                itemList.Remove(itemInInventory);
                Debug.Log(itemInInventory.item.itemName + " was removed from player inventory.");
            }
            return true;
        }
        else
        {
            Debug.Log("No item called " + temp.item.itemName + " found.");
        }
        return false;
    }

    public void listOfItems()
    {
        int totalItems = 0;
        if(itemList.Count == 0)
        {
            Debug.Log("Player inventory is empty.");
        }
        else
        {
            Debug.Log("Start of inventory");
            foreach(ItemInInventory itemInInventory in itemList)
            {
                totalItems += itemInInventory.amount;
                Debug.Log(itemInInventory.item.itemName + " Item Desc: " + itemInInventory.item.itemDescription + " Item Stats: " + itemInInventory.item.getStats() + " Item amount: " + itemInInventory.item.amount);
            }
            Debug.Log("End of inventory");
        }
        Debug.Log("Amount of different items in inventory is " + itemList.Count + " Total amount of items is " + totalItems);
    }

    public List<ItemInInventory> getList()
    {
        return itemList;
    }
}
