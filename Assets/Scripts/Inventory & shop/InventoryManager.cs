using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] itemSlots;
    //  public UseItem useItem;
    public int gold; // Variable to hold the amount of gold in the inventory
    public TMP_Text goldText;



    private void Start()
    {
        foreach (var slot in itemSlots)
        {
            slot.UpdateUi(); // Update the UI for all slots at the start
        }
        goldText.text = gold.ToString(); // Initialize the gold text UI with the starting amount of gold
    }
    private void OnEnable()
    {
        Loot.OnItemLooted += AddItem; // Subscribe to the OnItemLooted event
    }

    private void OnDisable()
    {
        Loot.OnItemLooted -= AddItem;
    }

    public void AddItem(ItemSO itemSO, int quantity)
    {
        if (itemSO.isGold)
        {
            gold += quantity; // Add the quantity of gold to the inventory
            goldText.text = gold.ToString();// Update the UI text to reflect the new amount of gold
            return;
        }
        else
        {
            foreach (var slot in itemSlots)
            {
                if (slot.itemSO == null)
                {
                    slot.itemSO = itemSO; // Assign the item to the slot
                    slot.quantity = quantity;
                    slot.UpdateUi(); // Update the UI for all slots before adding the item
                    
                    return;
                }




                }

            }
        }
    public void UseItem(InventorySlot slot)
    {
        if (slot.itemSO != null && slot.quantity >= 0)
        {
            Debug.Log("trying to úsing item:" + slot.itemSO.itemName);
        
        }

    }
    }


