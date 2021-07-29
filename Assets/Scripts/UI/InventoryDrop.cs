using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryDrop : MonoBehaviour, IPointerClickHandler
{
    public CharacterInventory charInv;
    public ItemInInventory itemToDrop;
    public GameObject playerChar;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            //Debug.Log("RightWorks " + itemToDrop.item.itemName);
            charInv.removeItem(itemToDrop.item.itemName, playerChar.gameObject);
        }
    }
    
}
