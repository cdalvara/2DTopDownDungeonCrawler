using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private CharacterInventory characterInventory;
    private Transform ItemSlots;
    private Transform ItemSlot;

    void Awake()
    {
        ItemSlots = transform.Find("ItemSlots");
        ItemSlot = ItemSlots.Find("ItemSlot");
    }

    public void setInventory(CharacterInventory charInventory)
    {
        characterInventory = charInventory;
        refreshInventory();
    }

    public void refreshInventory()
    {
        foreach(Transform child in ItemSlots)
        {
            if(child == ItemSlot)continue;
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;
        float ItemSlotCellSize = 50.0f;
        foreach(ItemInInventory itemInInventory in characterInventory.getList())
        {
            for(int i = 0; i < itemInInventory.amount; i++)
            {
                RectTransform itemSlotRectTransform = Instantiate(ItemSlot, ItemSlots).GetComponent<RectTransform>();
                itemSlotRectTransform.gameObject.SetActive(true);
                itemSlotRectTransform.GetComponent<InventoryDrop>().charInv = characterInventory;
                itemSlotRectTransform.GetComponent<InventoryDrop>().itemToDrop = itemInInventory;
                itemSlotRectTransform.GetComponent<InventoryDrop>().playerChar = player;
                itemSlotRectTransform.anchoredPosition = new Vector2(x*ItemSlotCellSize, y*ItemSlotCellSize);
                Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
                image.sprite = itemInInventory.item.itemSprite;
                x++;
                if(x>4)
                {
                    x = 0;
                    y--;
                }
            }
        }
    }
}
