using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] itemSlots;
    public int gold; // jumlah gold di inventory
    public TMP_Text goldText;
    public GameObject lootPrefab; // prefab loot untuk drop kalo inventory penuh
    public Transform player;

    // reference ke script UseItem (aku ganti nama biar ga bentrok)
    public UseItem useItemScript;

    private void Start()
    {
        foreach (var slot in itemSlots)
        {
            slot.UpdateUi(); // update semua UI slot di awal
        }
        goldText.text = gold.ToString(); // inisialisasi tampilan gold
    }

    private void OnEnable()
    {
        Loot.OnItemLooted += AddItem; // subscribe ke event loot
    }

    private void OnDisable()
    {
        Loot.OnItemLooted -= AddItem; // unsubscribe
    }

    public void AddItem(ItemSO itemSO, int quantity)
    {
        if (itemSO.isGold)
        {
            gold += quantity;
            goldText.text = gold.ToString(); // update UI gold
            return;
        }
        foreach (var slot in itemSlots)
        {
            if (slot.itemSO == itemSO && slot.quantity < itemSO.stackSize)
            { 
            int availableSpace = itemSO.stackSize - slot.quantity;
            int amountToAdd = Mathf.Min(availableSpace, quantity);

                slot.quantity += amountToAdd;
                quantity -= amountToAdd;

                slot.UpdateUi();
                if (quantity <= 0)
                    return; // kalo udah keadd semua, keluar

            }
        }
       
            foreach (var slot in itemSlots)
            {
                if (slot.itemSO == null)
                {
                    int amountToAdd = Mathf.Min(itemSO.stackSize -  quantity);
                slot.itemSO = itemSO;
                    slot.quantity = quantity;
                    slot.UpdateUi();
                    return;
                }
            }
            if(quantity > 0)
                DropLoot(itemSO, quantity);

    }

    private void DropLoot(ItemSO itemSO, int quantity)
    {
        Loot loot = Instantiate(lootPrefab, player.position,Quaternion.identity).GetComponent<Loot>();
        loot.Initialize(itemSO, quantity);
    }

    public void DropItem(InventorySlot slot) {

        DropLoot(slot.itemSO, 1);
        slot.quantity--;
        if (slot.quantity <= 0)
        { 
            slot.itemSO = null; // kosongin slot kalo habis
        }
        slot.UpdateUi();
    }

    public void UseItem(InventorySlot slot)
    {
        if (slot.itemSO != null && slot.quantity > 0)
        {
            // pake script UseItem untuk efek item
            useItemScript.ApplyItemEffects(slot.itemSO);

            slot.quantity--;
            if (slot.quantity <= 0)
            {
                slot.itemSO = null; // kosongin slot kalo habis
            }
            slot.UpdateUi();
        }
    }
}
